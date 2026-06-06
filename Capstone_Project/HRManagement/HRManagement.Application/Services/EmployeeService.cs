using HRManagement.Core.Entities;
using HRManagement.Core.Exceptions;
using HRManagement.Core.Interfaces;

namespace HRManagement.Application.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // Get employee by Id
        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                throw new EmployeeNotFoundException(id);
            }

            return employee;
        }

        // Get all employees
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Add employee
        public async Task AddEmployeeAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
        }

        // Update employee
        public async Task UpdateEmployeeAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
        }

        // Delete employee
        public async Task DeleteEmployeeAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}