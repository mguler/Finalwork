using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using Unity;
using System;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using TR.Edu.Ankara.EUB201.Finalwork.Business;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using Unity.Registration;
using TR.Edu.Ankara.EUB201.Finalwork.DataAccess;
using Unity.Injection;

namespace TR.Edu.Ankara.EUB201.Finalwork
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            #region Unity DI Configuration

            //Add DI Container
            var container = this.AddUnity();

            //Data Access
            var connectionString = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            container.RegisterType<IDbConnection, SqlConnection>(new InjectionConstructor(connectionString));
            container.RegisterType<AdoNetDataRepository>();
            #endregion End Of Unity DI Configuration            
        }
    }
}