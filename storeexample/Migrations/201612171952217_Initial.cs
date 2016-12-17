namespace storeexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DisplayName = c.String(),
                        IsFullNameVisible = c.Boolean(nullable: false),
                        Phone = c.String(),
                        Email = c.String(),
                        ContactType = c.Int(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Store_StoreId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Drivers",
                c => new
                    {
                        DriverId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Address3 = c.String(),
                        Phone = c.String(),
                        EmailAddress = c.String(),
                        Rating = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.DriverId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Store_StoreId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        DateOrdered = c.DateTime(nullable: false),
                        ScheduledDeliveryDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        RecurFrequency = c.Int(nullable: false),
                        WeeklyRecurDay = c.Int(nullable: false),
                        MonthlyRecurDay = c.Int(nullable: false),
                        IsRecurring = c.Boolean(nullable: false),
                        DeliveryAddress1 = c.String(),
                        DeliveryAddress2 = c.String(),
                        DeliveryAddress3 = c.String(),
                        City = c.String(),
                        State = c.String(),
                        ZipCode_ZipCodeId = c.Int(),
                        Driver_DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCode_ZipCodeId)
                .ForeignKey("dbo.Drivers", t => t.Driver_DriverId)
                .Index(t => t.ZipCode_ZipCodeId)
                .Index(t => t.Driver_DriverId);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        ZipCodeId = c.Int(nullable: false, identity: true),
                        Zip = c.Int(nullable: false),
                        IsServiced = c.Boolean(nullable: false),
                        Driver_DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.ZipCodeId)
                .ForeignKey("dbo.Drivers", t => t.Driver_DriverId)
                .Index(t => t.Driver_DriverId);
            
            CreateTable(
                "dbo.HomePageItems",
                c => new
                    {
                        HomePageItemId = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        ImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.HomePageItemId);
            
            CreateTable(
                "dbo.OrderedProducts",
                c => new
                    {
                        OrderedProductId = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        QuantityOrdered = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderedProductId)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Description = c.String(),
                        ImageUrl = c.String(),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BaseQuantity = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsMultipleItem = c.Boolean(nullable: false),
                        IsAlwaysAvailable = c.Boolean(nullable: false),
                        ProductCategoryId = c.Int(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.ProductCategoryId)
                .Index(t => t.Store_StoreId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        Url = c.String(),
                        IsOpenOnHolidays = c.Boolean(nullable: false),
                        HolidayMessage = c.String(),
                        DeliveryAvailable = c.Boolean(nullable: false),
                        IsMultiNational = c.Boolean(nullable: false),
                        IsMultiProvince = c.Boolean(nullable: false),
                        IsMultiCity = c.Boolean(nullable: false),
                        HomeSplashImageUrl = c.String(),
                    })
                .PrimaryKey(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Drivers", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Contacts", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.OrderedProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ProductCategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.OrderedProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.ZipCodes", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "ZipCode_ZipCodeId", "dbo.ZipCodes");
            DropIndex("dbo.Products", new[] { "Store_StoreId" });
            DropIndex("dbo.Products", new[] { "ProductCategoryId" });
            DropIndex("dbo.OrderedProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderedProducts", new[] { "OrderId" });
            DropIndex("dbo.ZipCodes", new[] { "Driver_DriverId" });
            DropIndex("dbo.Orders", new[] { "Driver_DriverId" });
            DropIndex("dbo.Orders", new[] { "ZipCode_ZipCodeId" });
            DropIndex("dbo.Drivers", new[] { "Store_StoreId" });
            DropIndex("dbo.Contacts", new[] { "Store_StoreId" });
            DropTable("dbo.Stores");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.OrderedProducts");
            DropTable("dbo.HomePageItems");
            DropTable("dbo.ZipCodes");
            DropTable("dbo.Orders");
            DropTable("dbo.Drivers");
            DropTable("dbo.Customers");
            DropTable("dbo.Contacts");
        }
    }
}
