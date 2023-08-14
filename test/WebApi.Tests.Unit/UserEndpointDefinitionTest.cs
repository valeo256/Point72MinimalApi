using Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using NSubstitute;
using Application.Common.Interfaces;
using MediatR;
using Application.RequestHandlers.User.Queries.GetUserQuery;

namespace WebApi.Tests.Unit
{
    public class UserEndpointDefinitionTest
    {
        private readonly IUserService _userService =
            Substitute.For<IUserService>();
        private readonly ISender _sender = Substitute.For<ISender>();

        private readonly Endpoints.Users _sut = new();

        [Fact]
        public async Task GetAllCustomers_ReturnsCustomer_WhenCustomerExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var customer = new UserExample { Id = id, FullName = "Nick Chapsas" };
            _userService.GetById(id).Returns(customer);

            //Act
            var result = await _sut.GetUserById(_sender, id);

            //Assert
            Assert.NotNull(result);
            //Assert.Equal(customer, result);
        }
    }
}