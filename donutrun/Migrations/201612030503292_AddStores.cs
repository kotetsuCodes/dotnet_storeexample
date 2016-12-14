namespace donutrun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStores : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Countries", "IsoCode", c => c.String(maxLength: 2, fixedLength: true, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Countries", "IsoCode");
        }
    }
}
