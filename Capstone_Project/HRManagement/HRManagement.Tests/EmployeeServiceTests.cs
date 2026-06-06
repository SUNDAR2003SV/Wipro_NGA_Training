using FluentAssertions;
using HRManagement.Application.Services;
using HRManagement.Core.Entities;
using HRManagement.Core.Interfaces;
using Moq;
using Xunit;

namespace HRManagement.Tests
{
    public class EmployeeServiceTests
    {
        // ==================================================
        // Test 1: Verify employee is added successfully
        // ==================================================
        [Fact]
        public async Task AddEmployeeAsync_Should_Add_Employee()
        {
            // Arrange
            // Create a fake repository using Moq
            var repositoryMock = new Mock<IEmployeeRepository>();

            // Create service object
            var service = new EmployeeService(repositoryMock.Object);

            // Create test employee
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@test.com",
                Department = "IT"
            };

            // Act
            await service.AddEmployeeAsync(employee);

            // Assert
            // Verify AddAsync was called exactly once
            repositoryMock.Verify(
                x => x.AddAsync(employee),
                Times.Once);
        }

        // ==================================================
        // Test 2: Verify employee is returned by Id
        // ==================================================
        [Fact]
        public async Task GetEmployeeAsync_Should_Return_Employee()
        {
            // Arrange
            var repositoryMock = new Mock<IEmployeeRepository>();

            // Create test employee
            var employee = new Employee
            {
                EmployeeId = 1,
                FirstName = "John"
            };

            // Configure mock repository
            repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(employee);

            var service = new EmployeeService(repositoryMock.Object);

            // Act
            var result = await service.GetEmployeeAsync(1);

            // Assert
            result.Should().NotBeNull();
            result.EmployeeId.Should().Be(1);
            result.FirstName.Should().Be("John");
        }

        // ==================================================
        // Test 3: Verify employee is deleted
        // ==================================================
        [Fact]
        public async Task DeleteEmployeeAsync_Should_Delete_Employee()
        {
            // Arrange
            var repositoryMock = new Mock<IEmployeeRepository>();

            var service = new EmployeeService(repositoryMock.Object);

            // Act
            await service.DeleteEmployeeAsync(1);

            // Assert
            repositoryMock.Verify(
                x => x.DeleteAsync(1),
                Times.Once);
        }
    }
}