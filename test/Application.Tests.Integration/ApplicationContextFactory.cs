using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Application.Tests.Integration;

public class ApplicationContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new ApplicationDbContext(options, Substitute.For<IMediator>());

        context.Database.EnsureCreated();
        context.Books.AddRange(new[] {
                new Book { Id = 1, Title = "The Raven", Description = "A narrative poem by American writer Edgar Allan Poe.", AuthorId = 1 },
                new Book { Id = 2, Title = "A Farewell to Arms", Description = "A Farewell to Arms is a novel by American writer Ernest Hemingway.", AuthorId = 2 }
            });

        context.Authors.AddRange(new[] {
                new Author { Id = 1, FirstName = "Edgar", MiddleName = "Allan", LastName = "Poe" },
                new Author { Id = 2, FirstName = "Ernest", MiddleName = "Miller", LastName = "Hemingway" }
            });

        context.BooksTakens.AddRange(new[] {
                new BooksTaken { BookId = 1, UserId = 1, DateTaken = DateTime.Parse("2023-05-01 00:00:00.000") },
                new BooksTaken { BookId = 2, UserId = 1, DateTaken = DateTime.Parse("2022-11-11 00:00:00.000") },
                 new BooksTaken { BookId = 2, UserId = 2, DateTaken = DateTime.Parse("2023-01-12 00:00:00.000") },
            });

        context.Users.AddRange(new[] {
                new User { Id = 1, FirstName = "James", LastName = "Hetfield", Email = "coolguy@metal.com" },
                new User { Id = 2, FirstName = "Lars", LastName = "Ulrich", Email = "drummer001@gmail.com" }
            });

        context.SaveChanges();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}
