using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;
using Microsoft.EntityFrameworkCore;

namespace BnBAir.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public readonly ReservationContext _db;
        public CategoryRepository(ReservationContext db)
            : base(db, db.Categories)
        {
            _db = db;
        }
        
        public override async Task<IEnumerable<Category>> GetAll()
        {
            return await _db.Categories
                .Include(category=>category.CategoryDates)
                .Include(rooms=>rooms.Rooms)
                .ToListAsync();
            
        }
        
        public override async void Create(Category category, Guid? itemId)
        {
            var categoryDate = _db.CategoryDates.FirstOrDefaultAsync(x => x.CategoryDateId == itemId);
            category.CategoryDates.Add(await categoryDate);
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();
        }
    }
}