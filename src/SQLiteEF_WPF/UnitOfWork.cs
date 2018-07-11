using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteEF_WPF.DBTenant;
using SQLiteEF_WPF.DBuser;

namespace SQLiteEF_WPF
{
    public class UnitOfWork : IUnitOfWork
    {

        public DbContext UsedDbEntities => _userDbFactory.UserDbContext ?? _userDbFactory.Init();

        public DbContext TenantDbEntities => _tenantDbFactory.TenantDbDbContext ?? _tenantDbFactory.Init();

        private IUserDbFactory _userDbFactory;
        private ITenantDbFactory _tenantDbFactory;
        public UnitOfWork(IUserDbFactory userDbFactory, ITenantDbFactory tenantDbFactory)
        {
            _userDbFactory = userDbFactory;
            _tenantDbFactory = tenantDbFactory;
        }


        public void Commit()
        {
            UsedDbEntities.SaveChanges();
            TenantDbEntities.SaveChanges();
        }

        public void Commit(string entityName)
        {
            if (!string.IsNullOrEmpty(entityName))
            {
                if (entityName == "User")
                {
                    UsedDbEntities.SaveChanges();
                }
                else
                {
                    TenantDbEntities.SaveChanges();
                }
            }
        }

        public bool ChangeDataBase(string dbName,string dataSource)
        {
            try
            {
                if (dbName == "Store")
                    TenantDbEntities.ChangeDatabase(dataSource: dataSource);

            }
            catch (Exception)
            {

                return false;
            }
            return true;

        }
    }

    public interface IUnitOfWork
    {
        void Commit();
        void Commit(string entityName);
        bool ChangeDataBase(string dbName, string dataSource);
    }
}
