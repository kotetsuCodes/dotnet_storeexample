namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProductToProductCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderedProducts", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.OrderedProducts", "Order_OrderId");
            AddForeignKey("dbo.OrderedProducts", "Order_OrderId", "dbo.Orders", "OrderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderedProducts", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.OrderedProducts", new[] { "Order_OrderId" });
            DropColumn("dbo.OrderedProducts", "Order_OrderId");
        }
    }
}
