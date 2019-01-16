using OTS.AuthCenter.Data;
using OTS.AuthCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Repositories
{
    public class BuildingRepository : DataRepository<Building>
    {
        public BuildingRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override void Create(Building entity)
        {
            _dbContext.buildings.Add(entity);
        }

        public override void Delete(Building entity)
        {
            _dbContext.buildings.Remove(entity);
        }

        public override void Delete(int id)
        {
            Delete(Get(id));
        }

        public override Building Get(int id)
        {
            return _dbContext.buildings.FirstOrDefault(b => b.Id == id);
        }

        public override IEnumerable<Building> GetAll()
        {
            return _dbContext.buildings;
        }

        public override void Update(Building entity)
        {
            _dbContext.buildings.Update(entity);
        }
    }
}
