using System;

namespace BnBAir.API.Models
{
    public class CategoryDatesViewModel
    {
        public Guid CategoryDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public virtual CategoryViewModel Category { get; set; }
    }
}