using System;
using System.Collections.Generic;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using BnBAir.DAL.Repositories;
using BnBAir.Tests.TestRepositories;
using NUnit.Framework;


namespace BnBAir.Tests
{
    public class CreateCategoryDate
    {
       
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