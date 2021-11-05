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

        public virtual Category Category { get; set; }

        
        public CategoryDate()
        {
            CategoryDateId = Guid.NewGuid();
        }
    }
}