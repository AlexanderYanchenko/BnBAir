
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BnBAir.API.Controllers;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.BLL.Services;
using BnBAir.DAL.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;

namespace BnBAir.Tests
{
    public class Tests
    {
        [Test]
        public async Task MonitorBooking_ShouldReturnStatusCodeOk()
        {
            var mock = new Mock<IServiceUW>();
            mock.Setup(x=>x.ReservationsDTO.Get()).ReturnsAsync(new List<ReservationDTO>());
            var controller = new AdminController(mock.Object);

            var target = await controller.MonitorBooking();
          //  var result = 
            var okResult = target as OkObjectResult;
            
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [Test]
        [TestCase("2490BEAE-8295-4DF4-BB6E-08D9A4C7C7C6", 200)]
        public async Task GuestMonitor_ShouldReturnStatusCodeOk(Guid id, int statusCode)
        {
            var mock = new Mock<IServiceUW>();
            mock.Setup(x => x.GuestsDTO.GetById(id)).ReturnsAsync(new GuestDTO());
            var controller = new AdminController(mock.Object);
            var target = await controller.GuestMonitor(id);
            var statusCodeResult = (IStatusCodeActionResult) target;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(statusCode, statusCodeResult.StatusCode);

        }
       
        
        [Test]
        public static async Task AddRoom_ShouldReturnStatusCodeOk()
        {
            var mock = new Mock<IServiceUW>();
            mock.Setup(x=>x.RoomsDTO.Get()).ReturnsAsync(new List<RoomDTO>());
            var controller = new AdminController(mock.Object);
            
            CategoryDateDTO testDateDto = new CategoryDateDTO
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Price = 500,
            };
            CategoryDTO testCategory = new CategoryDTO
            {
                CategoryId = Guid.NewGuid(),
                Name = "test",
                Bed = 3,
                CategoryDates = new List <CategoryDateDTO> {testDateDto}
            };
            
            var result = await controller.AddRoom(5, testCategory.CategoryId);
            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }
    }
}