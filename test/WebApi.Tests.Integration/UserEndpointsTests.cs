using Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System.Net;
using System.Text.Json;
using Application.Common.Interfaces;

namespace WebApi.Tests.Integration
{
    public class UserEndpointsTests
    {
        private readonly IUserService _userService =
            Substitute.For<IUserService>();

        [Fact]
        public async Task GetUserById_ReturnUser_WhenUserExists()
        {
            //Arrange
            var id = Guid.NewGuid();
            var user = new UserExample { Id = id, FullName = "Nick Chapsas" };
            _userService.GetById(Arg.Is(id)).Returns(user);

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_userService);
            });

            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/user/{id}");
            var responseText = await response.Content.ReadAsStringAsync();
            var customerResult = JsonSerializer.Deserialize<UserExample>(responseText);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            customerResult.Should().BeEquivalentTo(user);
        }

        [Fact]
        public async Task GetUserById_ReturnNotFound_WhenUserDoesNotExists()
        {
            //Arrange
            _userService.GetById(Arg.Any<Guid>()).Returns((UserExample?)null);

            using var app = new TestApplicationFactory(x =>
            {
                x.AddSingleton(_userService);
            });

            var id = Guid.NewGuid();
            var httpClient = app.CreateClient();

            //Act
            var response = await httpClient.GetAsync($"/user/{id}");

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
