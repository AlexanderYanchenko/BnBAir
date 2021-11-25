using System;

namespace BnBAir.WEB.Models
{
    public class CategoryDateModel
    {
        public Guid CategoryDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public virtual CategoryModel Category { get; set; }
    }
}