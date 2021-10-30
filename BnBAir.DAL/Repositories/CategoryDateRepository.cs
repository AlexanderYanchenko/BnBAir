using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Repositories
{
    public class CategoryDateRepository : GenericRepository<CategoryDate>
    {
        public CategoryDateRepository(ReservationContext db)
            : base(db, db.CategoryDates)
        {
        }
    }
}