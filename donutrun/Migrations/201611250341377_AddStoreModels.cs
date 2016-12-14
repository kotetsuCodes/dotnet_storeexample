namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoreModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Province_ProvinceId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId)
                .ForeignKey("dbo.Provinces", t => t.Province_ProvinceId)
                .Index(t => t.Province_ProvinceId);
            
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
                        ServiceArea_ServiceAreaId = c.Int(),
                        Driver_DriverId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.ServiceAreas", t => t.ServiceArea_ServiceAreaId)
                .ForeignKey("dbo.Drivers", t => t.Driver_DriverId)
                .Index(t => t.ServiceArea_ServiceAreaId)
                .Index(t => t.Driver_DriverId);
            
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
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Store_StoreId);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProvinceId)
                .ForeignKey("dbo.Countries", t => t.Country_CountryId)
                .Index(t => t.Country_CountryId);
            
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
                "dbo.OrderedProducts",
                c => new
                    {
                        OrderedProductId = c.Int(nullable: false, identity: true),
                        QuantityOrdered = c.Int(nullable: false),
                        Product_ProductId = c.Int(),
                    })
                .PrimaryKey(t => t.OrderedProductId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .Index(t => t.Product_ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BaseQuantity = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsMultipleItem = c.Boolean(nullable: false),
                        IsAlwaysAvailable = c.Boolean(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.Stores", t => t.Store_StoreId)
                .Index(t => t.Store_StoreId);
            
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
                        DefaultCity_CityId = c.Int(),
                        DefaultCountry_CountryId = c.Int(),
                        DefaultProvince_ProvinceId = c.Int(),
                    })
                .PrimaryKey(t => t.StoreId)
                .ForeignKey("dbo.Cities", t => t.DefaultCity_CityId)
                .ForeignKey("dbo.Countries", t => t.DefaultCountry_CountryId)
                .ForeignKey("dbo.Provinces", t => t.DefaultProvince_ProvinceId)
                .Index(t => t.DefaultCity_CityId)
                .Index(t => t.DefaultCountry_CountryId)
                .Index(t => t.DefaultProvince_ProvinceId);
            
            CreateTable(
                "dbo.DriverServiceAreas",
                c => new
                    {
                        Driver_DriverId = c.Int(nullable: false),
                        ServiceArea_ServiceAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Driver_DriverId, t.ServiceArea_ServiceAreaId })
                .ForeignKey("dbo.Drivers", t => t.Driver_DriverId, cascadeDelete: true)
                .ForeignKey("dbo.ServiceAreas", t => t.ServiceArea_ServiceAreaId, cascadeDelete: true)
                .Index(t => t.Driver_DriverId)
                .Index(t => t.ServiceArea_ServiceAreaId);
            
            AddColumn("dbo.ServiceAreas", "DisplayName", c => c.String());
            AddColumn("dbo.ServiceAreas", "City_CityId", c => c.Int());
            AddColumn("dbo.ServiceAreas", "Store_StoreId", c => c.Int());
            CreateIndex("dbo.ServiceAreas", "City_CityId");
            CreateIndex("dbo.ServiceAreas", "Store_StoreId");
            AddForeignKey("dbo.ServiceAreas", "City_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.ServiceAreas", "Store_StoreId", "dbo.Stores", "StoreId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceAreas", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Products", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Drivers", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stores", "DefaultProvince_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Stores", "DefaultCountry_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Stores", "DefaultCity_CityId", "dbo.Cities");
            DropForeignKey("dbo.Countries", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Contacts", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.OrderedProducts", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Cities", "Province_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.ServiceAreas", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.DriverServiceAreas", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropForeignKey("dbo.DriverServiceAreas", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropIndex("dbo.DriverServiceAreas", new[] { "ServiceArea_ServiceAreaId" });
            DropIndex("dbo.DriverServiceAreas", new[] { "Driver_DriverId" });
            DropIndex("dbo.Stores", new[] { "DefaultProvince_ProvinceId" });
            DropIndex("dbo.Stores", new[] { "DefaultCountry_CountryId" });
            DropIndex("dbo.Stores", new[] { "DefaultCity_CityId" });
            DropIndex("dbo.Products", new[] { "Store_StoreId" });
            DropIndex("dbo.OrderedProducts", new[] { "Product_ProductId" });
            DropIndex("dbo.Provinces", new[] { "Country_CountryId" });
            DropIndex("dbo.Countries", new[] { "Store_StoreId" });
            DropIndex("dbo.Contacts", new[] { "Store_StoreId" });
            DropIndex("dbo.Orders", new[] { "Driver_DriverId" });
            DropIndex("dbo.Orders", new[] { "ServiceArea_ServiceAreaId" });
            DropIndex("dbo.Drivers", new[] { "Store_StoreId" });
            DropIndex("dbo.ServiceAreas", new[] { "Store_StoreId" });
            DropIndex("dbo.ServiceAreas", new[] { "City_CityId" });
            DropIndex("dbo.Cities", new[] { "Province_ProvinceId" });
            DropColumn("dbo.ServiceAreas", "Store_StoreId");
            DropColumn("dbo.ServiceAreas", "City_CityId");
            DropColumn("dbo.ServiceAreas", "DisplayName");
            DropTable("dbo.DriverServiceAreas");
            DropTable("dbo.Stores");
            DropTable("dbo.Products");
            DropTable("dbo.OrderedProducts");
            DropTable("dbo.Customers");
            DropTable("dbo.Provinces");
            DropTable("dbo.Countries");
            DropTable("dbo.Contacts");
            DropTable("dbo.Orders");
            DropTable("dbo.Drivers");
            DropTable("dbo.Cities");
        }
    }
}
