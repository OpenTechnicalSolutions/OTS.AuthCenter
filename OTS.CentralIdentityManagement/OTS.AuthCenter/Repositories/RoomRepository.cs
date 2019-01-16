using OTS.AuthCenter.Data;
using OTS.AuthCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Repositories
{
    public class RoomRepository : DataRepository<Room>
    {
        public RoomRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override void Create(Room entity)
        {
            _dbContext.rooms.Add(entity);
        }

        public override void Delete(Room entity)
        {
            _dbContext.rooms.Remove(entity);
        }

        public override void Delete(int id)
        {
            Delete(Get(id));
        }

        public override Room Get(int id)
        {
            return _dbContext.rooms.FirstOrDefault(r => r.Id == id);
        }

        public override IEnumerable<Room> GetAll()
        {
            return _dbContext.rooms;
        }

        public override void Update(Room entity)
        {
            _dbContext.rooms.Update(entity);
        }
    }
}
