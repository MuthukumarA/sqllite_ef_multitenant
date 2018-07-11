using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;
using SQLiteEF_WPF.DBTenant;
using SQLiteEF_WPF.DBuser;


namespace SQLiteEF_WPF
{

    public static class BootStrapper
    {
        public static void Start()
        {

            if (_rootScope != null)
            {
                return;
            }
            
            var builder = new ContainerBuilder();
            builder.RegisterType<TenantDBEntities>()
                .As<DbContext>().InstancePerRequest();
            builder.RegisterType<USerdbEntities>()
                .As<DbContext>().SingleInstance();
            builder.RegisterType<UserDbFactory>()
                .As<IUserDbFactory>().SingleInstance();
            builder.RegisterType<TenantDbFactory>()
                .As<ITenantDbFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(MainRepository<>))
               .As(typeof(IMainRepository<>)).SingleInstance();
            builder.RegisterGeneric(typeof(TenantRepository<>))
                .As(typeof(ITenantRepository<>)).SingleInstance();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
         
            builder.RegisterType<MainWindow>();
           
            _rootScope = builder.Build();
        }
        private static ILifetimeScope _rootScope;

        public static void Stop()
        {
            _rootScope.Dispose();
        }

        public static T Resolve<T>()
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(new Parameter[0]);
        }

        public static T Resolve<T>(Parameter[] parameters)
        {
            if (_rootScope == null)
            {
                throw new Exception("Bootstrapper hasn't been started!");
            }

            return _rootScope.Resolve<T>(parameters);
        }
    }
}
