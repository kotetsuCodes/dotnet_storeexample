namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHomeSplashImageUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "HomeSplashImageUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "HomeSplashImageUrl");
        }
    }
}
