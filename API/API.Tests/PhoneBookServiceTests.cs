using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Data;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace API.API.Tests
{
    public class PhoneBookServiceTests
    {
      private DbContextOptions<PhoneBookContext> CreateNewContextOptions()
        {
            // In-memory database for testing
            return new DbContextOptionsBuilder<PhoneBookContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetEntriesAlphabetically_ShouldReturnEntriesInAlphabeticalOrder()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.AddRange(
                    new PhoneBookEntry { FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" },
                    new PhoneBookEntry { FirstName = "Alice", LastName = "Smith", Type = PhoneType.Work.ToString(), Number = "9876543210" },
                    new PhoneBookEntry { FirstName = "Bob", LastName = "Johnson", Type = PhoneType.Home.ToString(), Number = "5678901234" }
                );
                await context.SaveChangesAsync();
            }

            // Act
            List<PhoneBookEntry> result;
            using (var context = new PhoneBookContext(options))
            {
                var service = new PhoneBookService(context);
                result = await service.GetPhoneBooksAlphabetically();
            }

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Alice", result[0].FirstName);
            Assert.Equal("Bob", result[1].FirstName);
            Assert.Equal("John", result[2].FirstName);
        }


        [Fact]
        public async Task GetEntriesByFirstName_ShouldReturnEntriesSortedByFirstName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.AddRange(
                    new PhoneBookEntry { FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" },
                    new PhoneBookEntry { FirstName = "Alice", LastName = "Smith", Type = PhoneType.Work.ToString(), Number = "9876543210" },
                    new PhoneBookEntry { FirstName = "Bob", LastName = "Johnson", Type = PhoneType.Home.ToString(), Number = "5678901234" }
                );
                await context.SaveChangesAsync();
            }

            // Act
            List<PhoneBookEntry> result;
            using (var context = new PhoneBookContext(options))
            {
                var service = new PhoneBookService(context);
                result = await service.GetPhoneBooksByFirstName();
            }

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Alice", result[0].FirstName);
            Assert.Equal("Bob", result[1].FirstName);
            Assert.Equal("John", result[2].FirstName);
        }

        [Fact]
        public async Task GetEntriesByLastName_ShouldReturnEntriesSortedByLastName()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.AddRange(
                    new PhoneBookEntry { FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" },
                    new PhoneBookEntry { FirstName = "Alice", LastName = "Smith", Type = PhoneType.Work.ToString(), Number = "9876543210" },
                    new PhoneBookEntry { FirstName = "Bob", LastName = "Johnson", Type = PhoneType.Home.ToString(), Number = "5678901234" }
                );
                await context.SaveChangesAsync();
            }

            // Act
            List<PhoneBookEntry> result;
            using (var context = new PhoneBookContext(options))
            {
                var service = new PhoneBookService(context);
                result = await service.GetPhoneBooksByLastName();
            }

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal("Doe", result[0].LastName);
            Assert.Equal("Johnson", result[1].LastName);
            Assert.Equal("Smith", result[2].LastName);
        }
         [Fact]
        public async Task GetPhoneBooks_ReturnsListOfPhoneBooks()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.AddRange(
                    new PhoneBookEntry { FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" },
                    new PhoneBookEntry { FirstName = "Alice", LastName = "Smith", Type = PhoneType.Work.ToString(), Number = "9876543210" },
                    new PhoneBookEntry { FirstName = "Bob", LastName = "Johnson", Type = PhoneType.Home.ToString(), Number = "5678901234" }
                );
                await context.SaveChangesAsync();
            }

            var controller = new PhoneBooksController(new PhoneBookService(new PhoneBookContext(options)));

            // Act
            var result = await controller.GetPhoneBooks();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsAssignableFrom<List<PhoneBookEntry>>(okResult.Value);
            Assert.Equal(3, model.Count);
        }

        [Fact]
        public async Task GetPhoneBook_ReturnsPhoneBookById()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.Add(new PhoneBookEntry { Id = 1, FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" });
                await context.SaveChangesAsync();
            }

            var controller = new PhoneBooksController(new PhoneBookService(new PhoneBookContext(options)));

            // Act
            var result = await controller.GetPhoneBook(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var model = Assert.IsType<PhoneBookEntry>(okResult.Value);
            Assert.Equal(1, model.Id);
        }

        [Fact]
        public async Task AddPhoneBook_ReturnsCreatedAtAction()
        {
            // Arrange
            var options = CreateNewContextOptions();
            var newEntry = new PhoneBookEntry { FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" };
            var controller = new PhoneBooksController(new PhoneBookService(new PhoneBookContext(options)));

            // Act
            var result = await controller.AddPhoneBook(newEntry);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(controller.AddPhoneBook), createdAtActionResult.ActionName);
        }

        [Fact]
        public async Task EditPhoneBook_ReturnsOkResult()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.Add(new PhoneBookEntry { Id = 1, FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" });
                await context.SaveChangesAsync();
            }

            var controller = new PhoneBooksController(new PhoneBookService(new PhoneBookContext(options)));
            var entryToUpdate = new PhoneBookEntry { Id = 1, FirstName = "Jane", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "9876543210" };

            // Act
            var result = await controller.EditPhoneBook(1, entryToUpdate);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task DeletePhoneBook_ReturnsOkResult()
        {
            // Arrange
            var options = CreateNewContextOptions();
            using (var context = new PhoneBookContext(options))
            {
                context.PhoneBooks.Add(new PhoneBookEntry { Id = 1, FirstName = "John", LastName = "Doe", Type = PhoneType.Cellphone.ToString(), Number = "1234567890" });
                await context.SaveChangesAsync();
            }

            var controller = new PhoneBooksController(new PhoneBookService(new PhoneBookContext(options)));

            // Act
            var result = await controller.DeletePhoneBook(1);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
        }
    }
}