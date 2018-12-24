using Microsoft.EntityFrameworkCore;
using OTS.AuthCenter.Data;
using OTS.AuthCenter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Repositories
{
    public class SiteLocationRepository : DataRepository<SiteLocation>
    {
        public SiteLocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        { }

        public override void Create(SiteLocation entity)
        {
            _dbContext.siteLocations.Add(entity);
        }

        public override void Delete(SiteLocation entity)
        {
            _dbContext.siteLocations.Remove(entity);
        }

        public override void Delete(int id)
        {
            Delete(Get(id));
        }

        public override SiteLocation Get(int id)
        {
            return _dbContext.siteLocations.FirstOrDefault(sl => sl.Id == id);
        }

        public override IEnumerable<SiteLocation> GetAll()
        {
            return _dbContext.siteLocations;
        }

        public override void Update(SiteLocation entity)
        {
            _dbContext.siteLocations.Update(entity);
        }
    }
}
