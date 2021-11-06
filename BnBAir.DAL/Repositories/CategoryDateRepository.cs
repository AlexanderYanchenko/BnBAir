using System.Collections.Generic;
using System.Linq;
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
        public override IEnumerable<CategoryDate> GetAll()
        {
            return _db.CategoryDates
                .Include(x=>x.Category)
                .ThenInclude(ctg=>ctg.Rooms);
        }
    }
}