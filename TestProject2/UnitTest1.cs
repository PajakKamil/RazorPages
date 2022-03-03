using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.Controllers;
using FakeItEasy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;

//Nuget package FakeItEasy
namespace TestProject2
{
	public class UnitTest1
	{
		[Fact]
		public void Test1()
		{
			var unitOfWork = A.Fake<IUnitOfWork>();
			var IWebHost = A.Fake<IWebHostEnvironment>();
			//Arrange
			var controller = new MenuItemController(unitOfWork, IWebHost);


			//Act
			var actionResult = controller.Get();

			//Assert
			var result = actionResult as JsonResult;
			var returnJson = result.Value;
			Assert.NotNull(returnJson);
			Assert.Equal("{ data = Faked System.Collections.Generic.IEnumerable`1[Abby.Models.MenuItem] }", returnJson.ToString());
		}
	}
}