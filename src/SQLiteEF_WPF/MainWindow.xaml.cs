using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SQLiteEF_WPF.DBTenant;
using SQLiteEF_WPF.DBuser;

namespace SQLiteEF_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow(IUnitOfWork unitOfWork, IMainRepository<User> userRepository, ITenantRepository<Store> storeRepository )
        {
            //InitializeComponent();
            //var dbLocation = string.Empty;

            //var userCount = userRepository.GetAll().Count();
            //var storeCount = storeRepository.GetAll().Count();
            //if (unitOfWork.ChangeDataBase("Store",@"C:\Users\muthukumara\Documents\TenantDB.db"))
            //{
            //    var newcount = storeRepository.GetAll().Count();
            //}

            var tempUser = BootStrapper.Resolve<IMainRepository<User>>();
            if (tempUser != null)
            {
                userRepository.Add(new User() {Name = "Temp" + DateTime.Now});
            }
            userRepository.Add(new User() {Name = DateTime.Now.ToString()} );
            unitOfWork.Commit();
            //using (var dbUser = new DBuser.USerdbEntities())
            //{
            //    var user = dbUser.Users.FirstOrDefault(u => u.Name == "NR");
            //    if (user != null)
            //    {
            //        dbLocation = user.DBName;
            //    }
            //}

            try
            {
                
                var connectionString = ConnectionTools.GetConnectionString("TenantDBEntities",
                    dataSource: @"C:\\Users\\muthukumara\\Documents\\SQL_MT\\TenantDB.db");
                //if (dbLocation != string.Empty)
                //{
                //    using (var dbtenant = new DBTenant.TenantDBEntities())
                //    {
                //        dbtenant.Database.Connection.ConnectionString = connectionString;
                //        dbtenant.ChangeDatabase(dataSource: @"C:\\Users\\muthukumara\\Documents\\SQL_MT\\TenantDB.db");
                //        var count = dbtenant.Stores.Count();
                //        dbtenant.ChangeDatabase(dataSource: @"C:\Users\muthukumara\Documents\TenantDB.db");
                //        count = dbtenant.Stores.Count();
                //    }
                //}
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
    public static class ConnectionTools
    {

        public static string GetConnectionString(string dBsourcename, string initialCatalog = "",
            string dataSource = "",
            string userId = "",
            string password = "",
            bool integratedSecuity = true,
            string configConnectionStringName = "")
        {
            try
            {

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    ("data source =" + dataSource);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // now flip the properties that were changed
                return sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                // set log item if required
            }
            return null;
        }
        // all params are optional
        public static void ChangeDatabase(
        this DbContext source,
        string initialCatalog = "",
        string dataSource = "",
        string userId = "",
        string password = "",
        bool integratedSecuity = true,
        string configConnectionStringName = "")
        /* this would be used if the
        *  connectionString name varied from 
        *  the base EF class name */
        {
            try
            {
                // use the const name if it's not null, otherwise
                // using the convention of connection string = EF contextname
                // grab the type name and we're done
                //var configNameEf = string.IsNullOrEmpty(configConnectionStringName)
                //    ? source.GetType().Name 
                //    : configConnectionStringName;

                //// add a reference to System.Configuration
                //var entityCnxStringBuilder = new EntityConnectionStringBuilder
                //    (System.Configuration.ConfigurationManager
                //        .ConnectionStrings[configNameEf].ConnectionString);

                // init the sqlbuilder with the full EF connectionstring cargo
                var sqlCnxStringBuilder = new SqlConnectionStringBuilder
                    ("data source =" + dataSource);

                // only populate parameters with values if added
                if (!string.IsNullOrEmpty(initialCatalog))
                    sqlCnxStringBuilder.InitialCatalog = initialCatalog;
                if (!string.IsNullOrEmpty(dataSource))
                    sqlCnxStringBuilder.DataSource = dataSource;
                if (!string.IsNullOrEmpty(userId))
                    sqlCnxStringBuilder.UserID = userId;
                if (!string.IsNullOrEmpty(password))
                    sqlCnxStringBuilder.Password = password;

                // set the integrated security status
                sqlCnxStringBuilder.IntegratedSecurity = integratedSecuity;

                // now flip the properties that were changed
                source.Database.Connection.ConnectionString
                    = sqlCnxStringBuilder.ConnectionString;
            }
            catch (Exception ex)
            {
                // set log item if required
            }
        }
    }
}
