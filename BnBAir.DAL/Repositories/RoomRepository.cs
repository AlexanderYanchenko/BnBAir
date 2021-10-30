using BnBAir.DAL.EF;
using BnBAir.DAL.Enitities;

namespace BnBAir.DAL.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(ReservationContext db)
            : base(db, db.Rooms)
        {
        }
    }
}