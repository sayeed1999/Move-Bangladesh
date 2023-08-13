using RideSharing.Service;
using RideSharing.Entity;
using RideSharing.Service;
using RideSharing.Tests.MockRepositories;
using Moq;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Tests.Tests
{
    public class CustomerServiceTest
    {
        private readonly Mock<IBaseRepository<Customer>> _customerRepository;
        private readonly IServiceProvider _serviceProvider; // how to use it ??

        public CustomerServiceTest()
        {
            _customerRepository = new MockCustomerRepository().SetupAllMethodsUsingBuilderPattern();
        }

        private ICustomerService GetCustomerService()
        {
            var service = new CustomerService(_customerRepository.Object);
            return service;
        }

        [Fact]
        public async Task GetCustomers_Return_Array_Of_Customers()
        {
            // Arrange
            var customerService = GetCustomerService();

            // Act
            var customers = await customerService.GetAllAsync();

            // Assert
            Assert.Equal(2, customers.Count());
        }

        [Fact]
        public async Task GetCustomerById_Return_Single_Customer()
        {
            // Arrange
            var customerService = GetCustomerService();

            // Act
            var customer = await customerService.FindByIdAsync(1);

            // Assert
            Assert.IsType<Customer>(customer);
            Assert.Equal(1, customer.Id);
        }
    }
}