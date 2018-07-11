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
    public class TenantDbFactory : Disposable, ITenantDbFactory
    {
        public TenantDBEntities TenantDbDbContext { set; get; }

        
        public TenantDbFactory()
        {
            Init();
        }
        public DbContext Init()
        {
            return TenantDbDbContext ?? (TenantDbDbContext = new TenantDBEntities());
        }

        protected override void DisposeCore()
        {
            TenantDbDbContext?.Dispose();
        }

    }
    public class UserDbFactory : Disposable, IUserDbFactory
    {
        public USerdbEntities UserDbContext { get; set; }

        public UserDbFactory()
        {
            Init();
        }

        public DbContext Init()
        {
            return UserDbContext ?? (UserDbContext = new USerdbEntities());
        }

        protected override void DisposeCore()
        {
            UserDbContext?.Dispose();
        }

    }


    public interface IUserDbFactory
    {
         USerdbEntities UserDbContext { get; set; }

        DbContext Init();
    }
    public interface ITenantDbFactory
    {
        TenantDBEntities TenantDbDbContext { set; get; }

        DbContext Init();
    }
}
