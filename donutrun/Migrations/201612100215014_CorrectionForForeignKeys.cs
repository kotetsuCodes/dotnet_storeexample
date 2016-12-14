namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectionForForeignKeys : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderedProducts", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderedProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.OrderedProducts", new[] { "Order_OrderId" });
            DropIndex("dbo.OrderedProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.Products", new[] { "Category_ProductCategoryId" });
            RenameColumn(table: "dbo.OrderedProducts", name: "Order_OrderId", newName: "OrderId");
            RenameColumn(table: "dbo.OrderedProducts", name: "Product_ProductId", newName: "ProductId");
            RenameColumn(table: "dbo.Products", name: "Category_ProductCategoryId", newName: "ProductCategoryId");
            AlterColumn("dbo.OrderedProducts", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.OrderedProducts", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderedProducts", "OrderId");
            CreateIndex("dbo.OrderedProducts", "ProductId");
            CreateIndex("dbo.Products", "ProductCategoryId");
            AddForeignKey("dbo.OrderedProducts", "OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.OrderedProducts", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories", "ProductCategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderedProducts", "OrderId", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.OrderedProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderedProducts", new[] { "OrderId" });
            AlterColumn("dbo.Products", "ProductCategoryId", c => c.Int());
            AlterColumn("dbo.OrderedProducts", "ProductId", c => c.Int());
            AlterColumn("dbo.OrderedProducts", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.Products", name: "ProductCategoryId", newName: "Category_ProductCategoryId");
            RenameColumn(table: "dbo.OrderedProducts", name: "ProductId", newName: "Product_ProductId");
            RenameColumn(table: "dbo.OrderedProducts", name: "OrderId", newName: "Order_OrderId");
            CreateIndex("dbo.Products", "Category_ProductCategoryId");
            CreateIndex("dbo.OrderedProducts", "Product_ProductId");
            CreateIndex("dbo.OrderedProducts", "Order_OrderId");
            AddForeignKey("dbo.Products", "Category_ProductCategoryId", "dbo.ProductCategories", "ProductCategoryId");
            AddForeignKey("dbo.OrderedProducts", "Product_ProductId", "dbo.Products", "ProductId");
            AddForeignKey("dbo.OrderedProducts", "Order_OrderId", "dbo.Orders", "OrderId");
        }
    }
}
