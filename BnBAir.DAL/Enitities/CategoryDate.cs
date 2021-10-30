using System;
using System.Collections.Generic;

namespace BnBAir.DAL.Enitities
{
    public class CategoryDate
    {
        public Guid CategoryDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public ICollection<Category> Categories { get; set; }
        
        public CategoryDate()
        {
            CategoryDateId = Guid.NewGuid();
        }
    }
}