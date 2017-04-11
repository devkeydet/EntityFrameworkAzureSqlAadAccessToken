using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.SqlServer;
using System.Data.SqlClient;
using EntityFrameworkAzureSqlAadAccessToken.Utils;

namespace EntityFrameworkAzureSqlAadAccessToken.Models
{
    //NOTE:
    //  By default, when you add EF using NuGet, it adds a <entityFramework/> section.
    //  I removed the <entityFramework/> section from web.config in favor of doing this in code.
    //  If you prefer putting this in config, make adjustments accordingly.
    //  See: http://www.entityframeworktutorial.net/entityframework6/code-based-configuration.aspx
    [DbConfigurationType(typeof(MyConfiguration))]
    public class BlogContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
    }

    public class AadAccessTokenConnectionFactory : IDbConnectionFactory
    {
        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            // Manually getting the connection so we can attach an access token to it.
            // We get teh access token from Azure AD
            var factory = DbConfiguration.DependencyResolver.GetService<DbProviderFactory>("System.Data.SqlClient");
            var conn = factory.CreateConnection() as SqlConnection;
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["BlogContextConnectionString"].ToString();
            conn.AccessToken = TokenHelper.GetAccessToken();

            return conn;
        }
    }

    public class MyConfiguration : DbConfiguration
    {
        public MyConfiguration()
        {
            SetDatabaseInitializer(new NullDatabaseInitializer<BlogContext>());
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
            SetProviderServices("System.Data.SqlClient", SqlProviderServices.Instance);
            SetDefaultConnectionFactory(new AadAccessTokenConnectionFactory());
        }
    }
}