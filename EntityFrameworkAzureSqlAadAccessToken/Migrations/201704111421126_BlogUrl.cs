namespace EntityFrameworkAzureSqlAadAccessToken.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class BlogUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "Url", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Blogs", "Url");
        }
    }
}