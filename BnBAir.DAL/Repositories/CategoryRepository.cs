using System.Collections.Generic;
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
        
        public override IEnumerable<Category> GetAll()
        {
            return _db.Categories
                .Include(category=>category.CategoryDates)
                .Include(rooms=>rooms.Rooms);
        }
    }
}