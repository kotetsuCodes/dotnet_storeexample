namespace storeexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamedHomePageItemToCrumbs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HomePageCrumbs",
                c => new
                    {
                        HomePageCrumbId = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        Content = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.HomePageCrumbId);
            
            DropTable("dbo.HomePageItems");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.HomePageItems",
                c => new
                    {
                        HomePageItemId = c.Int(nullable: false, identity: true),
                        Heading = c.String(),
                        Content = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.HomePageItemId);
            
            DropTable("dbo.HomePageCrumbs");
        }
    }
}
