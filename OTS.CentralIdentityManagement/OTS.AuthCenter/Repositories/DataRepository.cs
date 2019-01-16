using OTS.AuthCenter.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OTS.AuthCenter.Repositories
{
    public abstract class DataRepository<T> where T : class
    {
        protected ApplicationDbContext _dbContext { get; set; }

        public DataRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public abstract IEnumerable<T> GetAll();

        public abstract T Get(int id);

        public abstract void Update(T entity);

        public abstract void Create(T entity);

        public abstract void Delete(T entity);

        public abstract void Delete(int id);

    }
}
