namespace storeexample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddZipCodeNotInServiceMessage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stores", "ZipCodeNotInServiceMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Stores", "ZipCodeNotInServiceMessage");
        }
    }
}
