using FluentAssertions;
using HRManagement.Application.Services;
using HRManagement.Core.Entities;
using HRManagement.Core.Exceptions;
using HRManagement.Core.Interfaces;
using Moq;
using Xunit;

namespace HRManagement.Tests
{
    public class LeaveServiceTests
    {
        // ==================================================
        // Test 1: Verify leave is applied successfully
        // ==================================================
        [Fact]
        public async Task ApplyLeaveAsync_Should_Apply_Leave()
        {
            // Arrange
            // Create fake repository
            var repositoryMock = new Mock<ILeaveRepository>();

            // Create service
            var service = new LeaveService(repositoryMock.Object);

            // Create valid leave request
            var leave = new Leave
            {
                EmployeeId = 1,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today.AddDays(3),
                Reason = "Vacation"
            };

            // Act
            await service.ApplyLeaveAsync(leave);

            // Assert
            repositoryMock.Verify(
                x => x.AddAsync(leave),
                Times.Once);
        }

        // ==================================================
        // Test 2: Invalid dates should throw exception
        // ==================================================
        [Fact]
        public async Task ApplyLeaveAsync_Should_Throw_InvalidLeaveException()
        {
            // Arrange
            var repositoryMock = new Mock<ILeaveRepository>();

            var service = new LeaveService(repositoryMock.Object);

            // Invalid leave request
            // Start Date > End Date
            var leave = new Leave
            {
                EmployeeId = 1,
                StartDate = DateTime.Today.AddDays(5),
                EndDate = DateTime.Today.AddDays(1),
                Reason = "Vacation"
            };

            // Act
            Func<Task> action =
                async () => await service.ApplyLeaveAsync(leave);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidLeaveException>();
        }

        // ==================================================
        // Test 3: Past date should throw exception
        // ==================================================
        [Fact]
        public async Task ApplyLeaveAsync_Should_Throw_Exception_For_Past_Date()
        {
            // Arrange
            var repositoryMock = new Mock<ILeaveRepository>();

            var service = new LeaveService(repositoryMock.Object);

            var leave = new Leave
            {
                EmployeeId = 1,
                StartDate = DateTime.Today.AddDays(-2),
                EndDate = DateTime.Today.AddDays(2),
                Reason = "Medical"
            };

            // Act
            Func<Task> action =
                async () => await service.ApplyLeaveAsync(leave);

            // Assert
            await action.Should()
                .ThrowAsync<InvalidLeaveException>();
        }
    }
}