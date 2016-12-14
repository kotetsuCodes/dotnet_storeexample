namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedMoneyColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "BasePrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "BasePrice", c => c.Decimal(nullable: false, storeType: "money"));
        }
    }
}
