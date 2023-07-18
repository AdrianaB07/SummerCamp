using SummerCamp.DataAccessLayer.Interfaces;
using SummerCamp.DataModels.Models;
using System.Linq.Expressions;

namespace SummerCamp.DataAccessLayer.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        protected readonly SummerCampDbContext dbContext;

        public GenericRepository(SummerCampDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual IList<T> GetAll()
        {
            return dbContext.Set<T>().ToList();
        }

        public virtual T? GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }
        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public virtual IList<T> Get(Expression<Func<T, bool>> expression)
        {
            return dbContext.Set<T>().Where(expression).ToList();
        }

        public void Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }
    }
}
