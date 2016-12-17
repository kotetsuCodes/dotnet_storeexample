namespace storeexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddHeaderToHomePageItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HomePageItems", "Heading", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.HomePageItems", "Heading");
        }
    }
}
