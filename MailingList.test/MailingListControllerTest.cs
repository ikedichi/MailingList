using System;
using System.Collections.Generic;
using System.Linq;
using CrudWeb.Controllers;
using CrudWeb.Models;
using CrudWeb.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MailingList.Test 
{
    public class MailingListControllerTest
    {
        private MyDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            var context = new MyDbContext(options);
            context.MailingList.Add(new Person { ID = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com" });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public void GetAllPeople_ShouldReturnList()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new MailingListController(context);

            // Act
            var result = controller.GetMailingList(string.Empty);

            // Assert
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Person>>>(result);
            var model = Assert.IsType<List<Person>>(actionResult.Value);
            Assert.Single(model);
            Assert.Equal("John", model.First().FirstName);
        }

        [Fact]
        public void OnPostUpdate_ShouldUpdateExistingPerson()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var pageModel = new MailingListModel(context);
            var initialPerson = new Person { ID = 2, FirstName = "Jane", LastName = "Doe", Email = "jane@example.com" };
            context.MailingList.Add(initialPerson);
            context.SaveChanges();

            var updatedPersonFromDb = context.MailingList.Find(2);

            // Check for null before accessing properties.
            if (updatedPersonFromDb != null)
            {
                Assert.Equal("UpdatedJane", updatedPersonFromDb.FirstName);
                Assert.Equal("UpdatedDoe", updatedPersonFromDb.LastName);
                Assert.Equal("updatedjane@example.com", updatedPersonFromDb.Email);
            }
            else
            {
                Assert.True(false, "Updated person could not be found in the database.");
            }
        }

    }
    }
