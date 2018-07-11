using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteEF_WPF.DBTenant;

namespace SQLiteEF_WPF
{
    public class MainRepository< T> :
    IMainRepository<T> where T :class, IEntity
    {
        public DbContext Context => UserDbFactory.UserDbContext;
        public IUserDbFactory UserDbFactory { get; set; }

        public MainRepository(IUserDbFactory userDbFactory)
        {
            UserDbFactory = userDbFactory;
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = Context.Set<T>().Where(predicate);
            return query;
        }

        public virtual T FindById(int id)
        {
            var entity = Context.Set<T>().FirstOrDefault(p => p.ID == id);
            return entity;
        }
        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {

        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public bool ChangeDatabase(string connectionString)
        {
            throw new NotImplementedException();
        }
    }

    public class TenantRepository<T> :
    ITenantRepository<T> where T : class, IEntity
    {
        public DbContext Context => TenantDbFactory.TenantDbDbContext;

        public ITenantDbFactory TenantDbFactory { get; set; }

        public TenantRepository(ITenantDbFactory tenantDbFactory)
        {
            TenantDbFactory = tenantDbFactory;
        }

        public virtual IQueryable<T> GetAll()
        {

            IQueryable<T> query = Context.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {

            IQueryable<T> query = Context.Set<T>().Where(predicate);
            return query;
        }

        public virtual T FindById(int id)
        {
            var entity = Context.Set<T>().FirstOrDefault(p => p.ID == id);
            return entity;
        }
        public virtual void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public virtual void Update(T entity)
        {

        }

        public virtual void Save()
        {
            Context.SaveChanges();
        }

        public bool ChangeDatabase(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
