namespace donutrun.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class DonutRunModel : DbContext
    {
        // Your context has been configured to use a 'DonutRunModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'donutrun.Models.DonutRunModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'DonutRunModel' 
        // connection string in the application configuration file.
        public DonutRunModel()
            : base("name=DonutRunModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<ZipCode> ZipCodes { get; set; }

        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderedProduct> OrderedProducts { get; set; }

    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}