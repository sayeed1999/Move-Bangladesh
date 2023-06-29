using Moq;
using RideSharing.Entity;
using RideSharing.Repository;
using RideSharing.Common.Enums;

namespace RideSharing.Tests.MockRepositories
{
    public class MockCustomerRepository : Mock<IBaseRepository<Customer>>
    {
        private IEnumerable<Customer> customers = new List<Customer>
        {
            new Customer
            {
                Id = 1,
                Address = "abc", 
                Email = "cust1@gmail.com", 
                Gender = Gender.Male, 
                Name = "Cust 1", 
                Phone = "880***",
                CreatedBy = 1,
                CreatedDateUtc = DateTime.UtcNow,
                UserId = 1
            },
            new Customer
            {
                Id = 2,
                Address = "abc",
                Email = "cust2@gmail.com",
                Gender = Gender.Female,
                Name = "Cust 2",
                Phone = "880***",
                CreatedBy = 2,
                CreatedDateUtc = DateTime.UtcNow,
                UserId = 2
            },
        };

        public MockCustomerRepository SetupAllMethodsUsingBuilderPattern()
        {
            Setup(x => x.GetAllAsync()).Returns(Task.FromResult(customers));

            Setup(x => x.FindByIdAsync(It.IsAny<long>()))
                .Returns<long>(id => Task.FromResult(customers.FirstOrDefault(c => c.Id == id)));

            return this;
        }

    }
}
