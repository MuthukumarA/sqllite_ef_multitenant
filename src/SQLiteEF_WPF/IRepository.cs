using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteEF_WPF
{
    public interface IRepository <T>
    {
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        T FindById(int Id);
        IQueryable<T> GetAll();
        bool ChangeDatabase(string connectionString);
    }

    public interface IMainRepository<T> : IRepository<T>
    {
        
    }
    public interface ITenantRepository<T> : IRepository<T>
    {

    }

}
