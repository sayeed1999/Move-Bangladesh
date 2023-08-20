using AuthService.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Entity;
using RideSharing.Infrastructure;
using RideSharing.Service;

namespace RideSharing.API.MessageQueues.Actions
{
    public class Actions
    {
        private readonly ICustomerService customerService;
        private readonly IDriverService driverService;
        private readonly ApplicationDbContext context;

        public Actions(ICustomerService customerService, IDriverService driverService, ApplicationDbContext context)
        {
            this.customerService = customerService;
            this.driverService = driverService;
            this.context = context;
        }
        
        public async Task OnUserRegistered(UserRegistered message)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    // if customer
                    var customer = Customer.Create(message.Id,
                                                   message.FirstName,
                                                   message.LastName,
                                                   message.Gender,
                                                   message.Email,
                                                   message.UserName,
                                                   message.PhoneNumber);

                    await customerService.AddAsync(customer.Value);

                    // if driver
                    var driver = Driver.Create(message.Id,
                                               message.FirstName,
                                               message.LastName,
                                               message.Gender,
                                               message.Email,
                                               message.UserName,
                                               message.PhoneNumber);

                    await driverService.AddAsync(driver.Value);
                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task OnUserModified(UserModified message)
        {
            try
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    // if customer
                    var customer = Customer.Create(message.Id,
                                                   message.FirstName,
                                                   message.LastName,
                                                   message.Gender,
                                                   message.Email,
                                                   message.UserName,
                                                   message.PhoneNumber);

                    await customerService.AddAsync(customer.Value);

                    // if driver
                    var driver = Driver.Create(message.Id,
                                               message.FirstName,
                                               message.LastName,
                                               message.Gender,
                                               message.Email,
                                               message.UserName,
                                               message.PhoneNumber);

                    await driverService.UpdateAsync(driver.Value);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
