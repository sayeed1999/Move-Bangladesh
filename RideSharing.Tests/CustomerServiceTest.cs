using RideSharing.Service;
using RideSharing.Repository;
using RideSharing.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace RideSharing.Tests
{
    public class CustomerServiceTest
    {
        private readonly Mock<IBaseRepository<Customer>> _mockRepository;
        private readonly IServiceProvider _serviceProvider;

        public CustomerServiceTest()
        {
            _mockRepository = new Mock<IBaseRepository<Customer>>();

            var services = new ServiceCollection();
            services.AddTransient<IBaseRepository<Customer>>(sp => _mockRepository.Object);

            _serviceProvider = services.BuildServiceProvider();
        }

        private IBaseRepository<Customer> GetCustomerService()
        {
            var service = _serviceProvider.GetRequiredService<IBaseRepository<Customer>>();
            return service;
        }

        [Fact]
        public async Task GetCustomers()
        {
            // Arrange
            var customerService = GetCustomerService();
            // Add any necessary setup for your mock repository

            // Act
            var customers = await customerService.GetAllAsync();
        
            // Assert
            Assert.Equal(0, customers.Count());
        }

    }
}
