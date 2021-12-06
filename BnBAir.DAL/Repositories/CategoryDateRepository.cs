using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class CategoryDateRepository : GenericRepository<CategoryDate>
    {
        public readonly ReservationContext _db;
        public CategoryDateRepository(ReservationContext db)
            : base(db, db.CategoryDates)
        {
            _db = db;
        }
        public override async Task<IEnumerable<CategoryDate>> GetAll()
        {
            return await _db.CategoryDates
                .Include(x=>x.Category)
                .ToListAsync();
        }
        

        public override async void Create(CategoryDate item, Guid? itemId)
        {
            await _db.AddAsync(item);
            await _db.SaveChangesAsync();
        }
    }
}