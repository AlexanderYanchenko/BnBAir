using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>
    {
        public CategoryRepository(ReservationContext db)
            : base(db, db.Categories)
        {
        }
    }
}