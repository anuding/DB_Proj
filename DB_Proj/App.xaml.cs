using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Core.EntityClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace DB_Proj
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string _db_ConnectionString;
        /// <summary>
        /// DB Connection String With Password
        /// </summary>
        public static string DBConnectionString
        {
            get
            {
                if (String.IsNullOrEmpty(_db_ConnectionString))
                {
                    var originalConnectionString = ConfigurationManager.ConnectionStrings["db_MarketEntities"].ConnectionString;
                    var entityBuilder = new EntityConnectionStringBuilder(originalConnectionString);
                    var factory = DbProviderFactories.GetFactory(entityBuilder.Provider);
                    var providerBuilder = factory.CreateConnectionStringBuilder();
                    providerBuilder.ConnectionString = entityBuilder.ProviderConnectionString;
                   // providerBuilder.Add("Password", "Your Password");
                    _db_ConnectionString = providerBuilder.ToString();
                }
                return _db_ConnectionString;
            }
        }
    }
}
