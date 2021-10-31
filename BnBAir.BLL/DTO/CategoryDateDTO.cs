using System;
using System.Collections.Generic;
using BnBAir.DAL.Enitities;

namespace BnBAir.BLL.DTO
{
    public class CategoryDateDTO
    {
        public Guid CategoryDateId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }

        public Guid CategoryId { get; set; }
        public ICollection<CategoryDTO> Categories { get; set; }
    }
}