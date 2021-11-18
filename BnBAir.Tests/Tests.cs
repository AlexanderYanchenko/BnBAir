
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BnBAir.API;
using BnBAir.API.Controllers;
using BnBAir.API.Models;
using BnBAir.BLL.DTO;
using BnBAir.BLL.Interfaces;
using BnBAir.BLL.Services;
using BnBAir.DAL.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.TestHost;
using Moq;
using NUnit.Framework;

namespace BnBAir.Tests
{
    public class Tests
    {
        [Test]
        [TestCase("", 1, "3328D90F-7DFD-48E8-9040-755348ABA964", 400)]
        [TestCase("test1", 0, "3328D90F-7DFD-48E8-9040-755348ABA964", 400)]
        [TestCase("   ", 0, "3328D90F-7DFD-48E8-9040-755348ABA964", 400)]
        [TestCase("test1", 1, "3328D90F-7DFD-48E8-9040-755348ABA964", 200)]
        
        public void AddCategory_ResponseTest(string name, int countOfBed, Guid categoryDatesId, int expected)
        {
            var mock = new Mock<IServiceUW>();
            mock.Setup(x => x.CategoriesDTO.Get()).ReturnsAsync(GetTestCategories());
            var controller = new CategoryController(mock.Object);
            var target = controller.AddCategory(name, countOfBed, categoryDatesId);
            var statusCodeResult = (IStatusCodeActionResult) target;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(expected,statusCodeResult.StatusCode);
        }

        [Test]
        [TestCase("TestName1", 200)]
        public async Task DeleteCategory_ResponseTest(string name, int expected)
        {
            var categories = GetTestCategories();
            var testCategory = categories.FirstOrDefault(x => x.Name == name);
            var mock = new Mock<IServiceUW>();
            mock.Setup(x => x.CategoriesDTO.Get()).ReturnsAsync(categories);
            var controller = new CategoryController(mock.Object);
            var target = await controller.DeleteCategory(testCategory.CategoryId);
            var statusCodeResult = (IStatusCodeActionResult) target;
            Assert.IsNotNull(statusCodeResult);
            Assert.AreEqual(expected,statusCodeResult.StatusCode);
        }
        
        
        private List<CategoryDTO> GetTestCategories()
        {
            var categories = new List<CategoryDTO>()
            {
                new CategoryDTO()
                {
                    Bed = 1,
                    Name = "TestName1",
                    CategoryDates = new List<CategoryDateDTO> {new CategoryDateDTO {Price = 200}}
                },
                new CategoryDTO()
                {
                    Bed = 2,
                    Name = "TestName2",
                    CategoryDates = new List<CategoryDateDTO> {new CategoryDateDTO {Price = 400}}
                }
            };
            return categories;
        }
    }
}