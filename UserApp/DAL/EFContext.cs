namespace DAL
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFContext")
        {

        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Product> Products { get; set; }
    }
}