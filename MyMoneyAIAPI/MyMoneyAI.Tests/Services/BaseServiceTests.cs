using AutoMapper;
using Moq;
using MyMoneyAI.Application.DTOs;
using MyMoneyAI.Application.Services;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;
using static MyMoneyAI.Tests.Services.BaseServiceTests;

namespace MyMoneyAI.Tests.Services
{

    public class BaseServiceTests
    {
        public class TestEntity : BaseEntity { }
        public record TestDto : BaseDto { }

        private class TestBaseService : BaseService<TestEntity, TestDto>
        {
            public TestBaseService(IGenericRepository<TestEntity> repository, IUserContext userContext, IMapper mapper)
                : base(repository, userContext, mapper) { }
        }

        private readonly Mock<IGenericRepository<TestEntity>> _mockRepository;
        private readonly Mock<IUserContext> _mockUserContext;
        private readonly Mock<IMapper> _mockMapper;
        private readonly TestBaseService _baseService;

        public BaseServiceTests()
        {
            _mockRepository = new Mock<IGenericRepository<TestEntity>>();
            _mockUserContext = new Mock<IUserContext>();
            _mockMapper = new Mock<IMapper>();
            _baseService = new TestBaseService(_mockRepository.Object, _mockUserContext.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task AddAsync_SetsOwnerIdAndCallsAddAsyncOnRepository()
        {
            // Arrange
            var testDto = new TestDto { Id = 1 };
            var testEntity = new TestEntity { Id = 1, OwnerId = "5" };

            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockMapper.Setup(m => m.Map<TestEntity>(It.Is<TestDto>(d => d.Id == testDto.Id)))
               .Returns(testEntity);
            _mockMapper.Setup(m => m.Map<TestDto>(It.Is<TestEntity>(e => e.Id == testEntity.Id && e.OwnerId == testEntity.OwnerId)))
                .Returns(testDto);
            _mockRepository.Setup(r => r.AddAsync(It.Is<TestEntity>(e => e.Id == testEntity.Id && e.OwnerId == testEntity.OwnerId)))
                           .Returns(Task.FromResult(testEntity));

            // Act
            var result = await _baseService.AddAsync(testDto);

            // Assert
            _mockMapper.Verify(m => m.Map<TestEntity>(testDto), Times.Once);
            _mockRepository.Verify(x => x.AddAsync(testEntity), Times.Once);
            _mockMapper.Verify(m => m.Map<TestDto>(testEntity), Times.Once);
            Assert.Equal(testDto.Id, result.Id);
        }

        [Fact]
        public async Task FindByIdAsync_CallsFindByIdAsyncOnRepository()
        {
            // Arrange
            int id = 1;
            var testDto = new TestDto { Id = 1 };
            var testEntity = new TestEntity { Id = 1, OwnerId = "5" };

            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockMapper.Setup(m => m.Map<TestDto>(It.Is<TestEntity>(e => e.Id == testEntity.Id && e.OwnerId == testEntity.OwnerId)))
               .Returns(testDto);
            _mockRepository.Setup(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>())).ReturnsAsync(testEntity);

            // Act
            var result = await _baseService.FindByIdAsync(id);

            // Assert
            Assert.Equal(id, result.Id);
            _mockMapper.Verify(m => m.Map<TestDto>(testEntity), Times.Once);
            _mockRepository.Verify(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ThrowsInvalidOperationExceptionWhenEntityNotFound()
        {
            // Arrange
            var testDto = new TestDto { Id = 1 };
            var testEntity = new TestEntity { Id = 1, OwnerId = "5" };
            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockRepository.Setup(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>())).ReturnsAsync((TestEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _baseService.UpdateAsync(testDto));
        }

        [Fact]
        public async Task UpdateAsync_CallsUpdateAsyncOnRepository()
        {
            // Arrange
            var testDto = new TestDto { Id = 1 };
            var testEntity = new TestEntity { Id = 1, OwnerId = "5" };
            var updatedEntity = new TestEntity { Id = 1, OwnerId = "5" };

            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockMapper.Setup(m => m.Map<TestEntity>(It.Is<TestDto>(d => d.Id == testDto.Id)))
             .Returns(testEntity);
            _mockMapper.Setup(m => m.Map<TestDto>(It.Is<TestEntity>(e => e.Id == testEntity.Id && e.OwnerId == testEntity.OwnerId)))
                .Returns(testDto);
            _mockRepository.Setup(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>())).ReturnsAsync(testEntity);
            _mockRepository.Setup(x => x.UpdateAsync(testEntity)).ReturnsAsync(updatedEntity);

            // Act
            await _baseService.UpdateAsync(testDto);

            // Assert
            _mockRepository.Verify(x => x.UpdateAsync(It.IsAny<TestEntity>()), Times.Once);
            _mockMapper.Verify(m => m.Map<TestEntity>(testDto), Times.Once);
            _mockMapper.Verify(m => m.Map<TestDto>(testEntity), Times.Once);
        }

        [Fact]
        public async Task RemoveAsync_ThrowsInvalidOperationExceptionWhenEntityNotFound()
        {
            // Arrange
            int id = 1;
            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockRepository.Setup(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>())).ReturnsAsync((TestEntity)null);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _baseService.RemoveAsync(id));
        }

        [Fact]
        public async Task RemoveAsync_CallsRemoveAsyncOnRepository()
        {
            // Arrange
            int id = 1;
            var testEntity = new TestEntity { Id = id, OwnerId = "5" };
            var testDto = new TestDto { Id = id };

            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockMapper.Setup(m => m.Map<TestDto>(It.Is<TestEntity>(e => e.Id == testEntity.Id && e.OwnerId == testEntity.OwnerId)))
                .Returns(testDto);
            _mockMapper.Setup(m => m.Map<TestEntity>(It.Is<TestDto>(d => d.Id == testDto.Id)))
            .Returns(testEntity);
            _mockRepository.Setup(x => x.FindByIdAsync(It.IsAny<Expression<Func<TestEntity, bool>>>())).ReturnsAsync(testEntity);
            _mockRepository.Setup(x => x.RemoveAsync(testEntity));


            // Act
            await _baseService.RemoveAsync(id);

            // Assert
            _mockRepository.Verify(x => x.RemoveAsync(testEntity), Times.Once);
        }

        [Fact]
        public async Task ListAsync_CallsListAsyncOnRepository()
        {
            // Arrange
            int pageNumber = 1;
            int pageSize = 10;
            _mockUserContext.Setup(x => x.UserId).Returns("5");
            _mockRepository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<TestEntity, bool>>>(), pageNumber, pageSize)).ReturnsAsync(new PagedList<TestEntity>(new List<TestEntity>(), pageNumber, pageSize));
            // Act
            var result = await _baseService.ListAsync(null, pageNumber, pageSize);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pageNumber, result.PageNumber);
            Assert.Equal(pageSize, result.PageSize);
            _mockRepository.Verify(x => x.ListAsync(It.IsAny<Expression<Func<TestEntity, bool>>>(), pageNumber, pageSize), Times.Once);
        }
    }
}
