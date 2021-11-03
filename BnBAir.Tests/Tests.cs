using System;
using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;
using BnBAir.Tests.TestRepositories;
using NUnit.Framework;

namespace BnBAir.Tests
{
    public class Tests
    {
        [Test]
        public void GetAllCategoryDates_ShouldReturnAllCategoryDates()
        {
            var testCategoryDates = GetCategoryDates();
            var testCategoryDateRepository = new TestCategoryDatesRepository(testCategoryDates);
            var result = testCategoryDateRepository.GetAll();
            Assert.AreEqual(testCategoryDates.Count, result.Count);
        }

        [Test]
        public void CreateCategoryDate_ShouldCreateAndAddCategoryDate()
        {
            var testCategoryDates = GetCategoryDates();
            var testCategoryDateRepository = new TestCategoryDatesRepository(testCategoryDates);
            var testCategoryDate = new CategoryDate()
            {
                StartDate = new DateTime(2021,10,25),
                EndDate = new DateTime(2022,05,24),
                Price = 800
            };
            var exceptedCategoryDates = testCategoryDates.ToList();
            exceptedCategoryDates.Add(testCategoryDate);
            testCategoryDateRepository.Create(testCategoryDate);
            Assert.AreEqual(exceptedCategoryDates.Count,testCategoryDates.Count);
        }

        [Test]
        public void GetByIdCategoryDae_ShouldFindCategoryDate()
        {
            var testCategoryDates = GetCategoryDates();
            var testGuid = testCategoryDates[0].CategoryDateId;
            var testCategoryDateRepository = new TestCategoryDatesRepository(testCategoryDates);
            var exceptedCategoryName = testCategoryDates
                .Where(id=>id.CategoryDateId == testGuid).ToList();
            var result = testCategoryDateRepository.GetById(testGuid);
            Assert.AreEqual(exceptedCategoryName[0], result);
        }
        
        [Test]
        public void RemoveCategoryDate_ShouldDeleteCategoryDate()
        {
            var testCategoryDates = GetCategoryDates();
            var testGuid = testCategoryDates[0].CategoryDateId;
            var testCategoryDateRepository = new TestCategoryDatesRepository(testCategoryDates);
            var exceptedCategoryDates = testCategoryDates.ToList();
            
            exceptedCategoryDates.RemoveAt(0);
            testCategoryDateRepository.Delete(testGuid);
            
            Assert.AreEqual(exceptedCategoryDates.Count,testCategoryDates.Count);
        }
        
        
        public List<CategoryDate> GetCategoryDates()
        {
            var categoryDates = new List<CategoryDate>();
            categoryDates.Add(new CategoryDate()
            {
                StartDate = new DateTime(2021,10,25),
                EndDate = new DateTime(2022,05,24),
                Price = 300
            });
                
            categoryDates.Add(new CategoryDate()
            {
                StartDate = new DateTime(2021,07,15),
                EndDate = new DateTime(2022,06,10),
                Price = 500
            });

            return categoryDates;
        }
    }
}