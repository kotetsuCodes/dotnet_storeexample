namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddServiceAreaAndZipcode : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ServiceAreas",
                c => new
                    {
                        ServiceAreaId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.ServiceAreaId);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ZipCodeId = c.Int(nullable: false, identity: true),
                        Zip = c.Int(nullable: false),
                        ServiceArea_ServiceAreaId = c.Int(),
                    })
                .PrimaryKey(t => t.ZipCodeId)
                .ForeignKey("dbo.ServiceAreas", t => t.ServiceArea_ServiceAreaId)
                .Index(t => t.ServiceArea_ServiceAreaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ZipCodes", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropIndex("dbo.ZipCodes", new[] { "ServiceArea_ServiceAreaId" });
            DropTable("dbo.ZipCodes");
            DropTable("dbo.ServiceAreas");
        }
    }
}
