﻿using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Common.ValueObjects;
using RideSharing.Entity;
using System.Data;

namespace RideSharing.Entity
{
    public class Customer : Human
    {
        private Customer()
        {

        }
        private Customer(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber) : base()
        {
            Id = id;
            Name = firstName + " " + lastName;
            Gender = gender;
            Email = email;
            Phone = phoneNumber;
            UserName = userName;
            DriverRatings = new();
            Trips = new();
        }
        

        public virtual List<DriverRating> DriverRatings { get; protected set; }
        public virtual List<Trip> Trips { get; protected set; }

        public static Result<Customer> Create(long id, string firstName, string lastName, Gender gender, Email email, string userName, string phoneNumber)
        {
            var customer = new Customer(id, firstName, lastName, gender, email.Value, userName, phoneNumber);

            var validator = new CustomerValidator();
            var validationResult = validator.Validate(customer);

            if (validationResult.IsValid) return Result.Success(customer);
            return Result.Failure<Customer>("not valid");
        }



    }
}
public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Id).GreaterThanOrEqualTo(0);
        RuleFor(customer => customer.Email).EmailAddress();
    }
}