namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductCategories : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.ProductCategoryProducts",
                c => new
                    {
                        ProductCategory_ProductCategoryId = c.Int(nullable: false),
                        Product_ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductCategory_ProductCategoryId, t.Product_ProductId })
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_ProductId, cascadeDelete: true)
                .Index(t => t.ProductCategory_ProductCategoryId)
                .Index(t => t.Product_ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductCategoryProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductCategoryProducts", "ProductCategory_ProductCategoryId", "dbo.ProductCategories");
            DropIndex("dbo.ProductCategoryProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.ProductCategoryProducts", new[] { "ProductCategory_ProductCategoryId" });
            DropTable("dbo.ProductCategoryProducts");
            DropTable("dbo.ProductCategories");
        }
    }
}
