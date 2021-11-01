using System;
using System.Collections.Generic;
using System.Linq;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using BnBAir.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.Tests.TestRepositories
{
    public class TestCategoryDatesRepository
    {
        private List<CategoryDate> _categoryDates;

        public TestCategoryDatesRepository(List<CategoryDate> categoryDates)
        {
            _categoryDates = categoryDates;
        }

        public List<CategoryDate> GetAll()
        {
            return _categoryDates;
        }

        public CategoryDate GetById(Guid id)
        {
            return _categoryDates.FirstOrDefault(c=>c.CategoryDateId == id);
        }

        public void Create(CategoryDate item)
        {
            _categoryDates.Add(item);
        }

        /*public void Update(CategoryDate item)
        {
            _categoryDates.Entry(item).State = EntityState.Modified;
        }*/

        public void Delete(Guid id)
        {
            var item = GetById(id);
            if (item != null)
            {
                _categoryDates.Remove(item);
            }
        }
    }
}