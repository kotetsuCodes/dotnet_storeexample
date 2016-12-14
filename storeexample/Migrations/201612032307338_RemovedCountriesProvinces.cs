namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCountriesProvinces : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropForeignKey("dbo.DriverServiceAreas", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.DriverServiceAreas", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropForeignKey("dbo.ZipCodes", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas");
            DropForeignKey("dbo.ServiceAreas", "City_CityId", "dbo.Cities");
            DropForeignKey("dbo.Cities", "Province_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Countries", "Store_StoreId", "dbo.Stores");
            DropForeignKey("dbo.Stores", "DefaultCity_CityId", "dbo.Cities");
            DropForeignKey("dbo.Stores", "DefaultCountry_CountryId", "dbo.Countries");
            DropForeignKey("dbo.Stores", "DefaultProvince_ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.ServiceAreas", "Store_StoreId", "dbo.Stores");
            DropIndex("dbo.Cities", new[] { "Province_ProvinceId" });
            DropIndex("dbo.ServiceAreas", new[] { "City_CityId" });
            DropIndex("dbo.ServiceAreas", new[] { "Store_StoreId" });
            DropIndex("dbo.Orders", new[] { "ServiceArea_ServiceAreaId" });
            DropIndex("dbo.ZipCodes", new[] { "ServiceArea_ServiceAreaId" });
            DropIndex("dbo.Countries", new[] { "Store_StoreId" });
            DropIndex("dbo.Provinces", new[] { "Country_CountryId" });
            DropIndex("dbo.Stores", new[] { "DefaultCity_CityId" });
            DropIndex("dbo.Stores", new[] { "DefaultCountry_CountryId" });
            DropIndex("dbo.Stores", new[] { "DefaultProvince_ProvinceId" });
            DropIndex("dbo.DriverServiceAreas", new[] { "Driver_DriverId" });
            DropIndex("dbo.DriverServiceAreas", new[] { "ServiceArea_ServiceAreaId" });
            AddColumn("dbo.Orders", "ZipCode_ZipCodeId", c => c.Int());
            AddColumn("dbo.ZipCodes", "IsServiced", c => c.Boolean(nullable: false));
            AddColumn("dbo.ZipCodes", "Driver_DriverId", c => c.Int());
            CreateIndex("dbo.Orders", "ZipCode_ZipCodeId");
            CreateIndex("dbo.ZipCodes", "Driver_DriverId");
            AddForeignKey("dbo.Orders", "ZipCode_ZipCodeId", "dbo.ZipCodes", "ZipCodeId");
            AddForeignKey("dbo.ZipCodes", "Driver_DriverId", "dbo.Drivers", "DriverId");
            DropColumn("dbo.Orders", "ServiceArea_ServiceAreaId");
            DropColumn("dbo.ZipCodes", "ServiceArea_ServiceAreaId");
            DropColumn("dbo.Stores", "DefaultCity_CityId");
            DropColumn("dbo.Stores", "DefaultCountry_CountryId");
            DropColumn("dbo.Stores", "DefaultProvince_ProvinceId");
            DropTable("dbo.Cities");
            DropTable("dbo.ServiceAreas");
            DropTable("dbo.Countries");
            DropTable("dbo.Provinces");
            DropTable("dbo.DriverServiceAreas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DriverServiceAreas",
                c => new
                    {
                        Driver_DriverId = c.Int(nullable: false),
                        ServiceArea_ServiceAreaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Driver_DriverId, t.ServiceArea_ServiceAreaId });
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        ProvinceId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Country_CountryId = c.Int(),
                    })
                .PrimaryKey(t => t.ProvinceId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        IsoCode = c.String(maxLength: 2, fixedLength: true, unicode: false),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            CreateTable(
                "dbo.ServiceAreas",
                c => new
                    {
                        ServiceAreaId = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(),
                        City_CityId = c.Int(),
                        Store_StoreId = c.Int(),
                    })
                .PrimaryKey(t => t.ServiceAreaId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        CityId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Province_ProvinceId = c.Int(),
                    })
                .PrimaryKey(t => t.CityId);
            
            AddColumn("dbo.Stores", "DefaultProvince_ProvinceId", c => c.Int());
            AddColumn("dbo.Stores", "DefaultCountry_CountryId", c => c.Int());
            AddColumn("dbo.Stores", "DefaultCity_CityId", c => c.Int());
            AddColumn("dbo.ZipCodes", "ServiceArea_ServiceAreaId", c => c.Int());
            AddColumn("dbo.Orders", "ServiceArea_ServiceAreaId", c => c.Int());
            DropForeignKey("dbo.ZipCodes", "Driver_DriverId", "dbo.Drivers");
            DropForeignKey("dbo.Orders", "ZipCode_ZipCodeId", "dbo.ZipCodes");
            DropIndex("dbo.ZipCodes", new[] { "Driver_DriverId" });
            DropIndex("dbo.Orders", new[] { "ZipCode_ZipCodeId" });
            DropColumn("dbo.ZipCodes", "Driver_DriverId");
            DropColumn("dbo.ZipCodes", "IsServiced");
            DropColumn("dbo.Orders", "ZipCode_ZipCodeId");
            CreateIndex("dbo.DriverServiceAreas", "ServiceArea_ServiceAreaId");
            CreateIndex("dbo.DriverServiceAreas", "Driver_DriverId");
            CreateIndex("dbo.Stores", "DefaultProvince_ProvinceId");
            CreateIndex("dbo.Stores", "DefaultCountry_CountryId");
            CreateIndex("dbo.Stores", "DefaultCity_CityId");
            CreateIndex("dbo.Provinces", "Country_CountryId");
            CreateIndex("dbo.Countries", "Store_StoreId");
            CreateIndex("dbo.ZipCodes", "ServiceArea_ServiceAreaId");
            CreateIndex("dbo.Orders", "ServiceArea_ServiceAreaId");
            CreateIndex("dbo.ServiceAreas", "Store_StoreId");
            CreateIndex("dbo.ServiceAreas", "City_CityId");
            CreateIndex("dbo.Cities", "Province_ProvinceId");
            AddForeignKey("dbo.ServiceAreas", "Store_StoreId", "dbo.Stores", "StoreId");
            AddForeignKey("dbo.Stores", "DefaultProvince_ProvinceId", "dbo.Provinces", "ProvinceId");
            AddForeignKey("dbo.Stores", "DefaultCountry_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Stores", "DefaultCity_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.Countries", "Store_StoreId", "dbo.Stores", "StoreId");
            AddForeignKey("dbo.Provinces", "Country_CountryId", "dbo.Countries", "CountryId");
            AddForeignKey("dbo.Cities", "Province_ProvinceId", "dbo.Provinces", "ProvinceId");
            AddForeignKey("dbo.ServiceAreas", "City_CityId", "dbo.Cities", "CityId");
            AddForeignKey("dbo.ZipCodes", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas", "ServiceAreaId");
            AddForeignKey("dbo.DriverServiceAreas", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas", "ServiceAreaId", cascadeDelete: true);
            AddForeignKey("dbo.DriverServiceAreas", "Driver_DriverId", "dbo.Drivers", "DriverId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "ServiceArea_ServiceAreaId", "dbo.ServiceAreas", "ServiceAreaId");
        }
    }
}
