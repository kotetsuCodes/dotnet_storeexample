namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductCategoryToOneToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProductCategoryProducts", "ProductCategory_ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategoryProducts", "Product_ProductId", "dbo.Products");
            DropIndex("dbo.ProductCategoryProducts", new[] { "ProductCategory_ProductCategoryId" });
            DropIndex("dbo.ProductCategoryProducts", new[] { "Product_ProductId" });
            AddColumn("dbo.Products", "Category_ProductCategoryId", c => c.Int());
            CreateIndex("dbo.Products", "Category_ProductCategoryId");
            AddForeignKey("dbo.Products", "Category_ProductCategoryId", "dbo.ProductCategories", "ProductCategoryId");
            DropTable("dbo.ProductCategoryProducts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductCategoryProducts",
                c => new
                    {
                        ProductCategory_ProductCategoryId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_ProductCategoryId, t.Product_ProductId });
            
            DropForeignKey("dbo.Products", "Category_ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.Products", new[] { "Category_ProductCategoryId" });
            DropColumn("dbo.Products", "Category_ProductCategoryId");
            CreateIndex("dbo.ProductCategoryProducts", "Product_ProductId");
            CreateIndex("dbo.ProductCategoryProducts", "ProductCategory_ProductCategoryId");
            AddForeignKey("dbo.ProductCategoryProducts", "Product_ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
            AddForeignKey("dbo.ProductCategoryProducts", "ProductCategory_ProductCategoryId", "dbo.ProductCategories", "ProductCategoryId", cascadeDelete: true);
        }
    }
}
