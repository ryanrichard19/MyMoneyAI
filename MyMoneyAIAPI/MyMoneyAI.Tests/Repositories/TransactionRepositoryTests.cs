using Microsoft.EntityFrameworkCore;
using Moq;
using MyMoneyAI.Domain.Entities;
using MyMoneyAI.Infrastructure.Data;
using MyMoneyAI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMoneyAI.Tests.Repositories
{
    public class TransactionRepositoryTests
    {
        private readonly Mock<DbSet<Transaction>> _mockTransactionSet;
        private readonly Mock<ApplicationDbContext> _mockContext;
        private readonly TransactionRepository _transactionRepository;


        public TransactionRepositoryTests()
        {

            _mockTransactionSet = new Mock<DbSet<Transaction>>();
            _mockContext = new Mock<ApplicationDbContext>();
            _transactionRepository = new TransactionRepository(_mockContext.Object);

            _mockContext.Setup(m => m.Transactions).Returns(_mockTransactionSet.Object);


        }


        [Fact]
        public async Task ListAsync_ReturnsAllTransactions()
        {

            // Act
            var result = await _transactionRepository.ListAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task AddAsync_AddsNewTransaction()
        {
            // Arrange
            var newTransaction = new Transaction { Id = 3, Amount = 300m };

            // Act
            await _transactionRepository.AddAsync(newTransaction);

            // Assert
            _mockTransactionSet.Verify(m => m.Add(It.IsAny<Transaction>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task FindByIdAsync_ReturnsTransactionById()
        {
            // Arrange
            int id = 1;

            // Act
            var result = await _transactionRepository.FindByIdAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task Update_UpdatesExistingTransaction()
        {
            // Arrange
            var updatedTransaction = new Transaction { Id = 1, Amount = 150m };

            // Act
            _transactionRepository.Update(updatedTransaction);

            // Assert
            _mockTransactionSet.Verify(m => m.Update(It.IsAny<Transaction>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Remove_RemovesTransaction()
        {
            // Arrange
            var transactionToRemove = new Transaction { Id = 1, Amount = 100m };

            // Act
            _transactionRepository.Remove(transactionToRemove);

            // Assert
            _mockTransactionSet.Verify(m => m.Remove(It.IsAny<Transaction>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once);
        }

    }
}
