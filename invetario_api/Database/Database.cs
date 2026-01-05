using invetario_api.Modules.categories.entity;
using invetario_api.Modules.products.entity;
using invetario_api.Modules.unit.entity;
using invetario_api.Modules.users.entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using invetario_api.Modules.store.entity;
using invetario_api.Modules.provider.entity;
using invetario_api.Modules.client.entity;
using invetario_api.Modules.entryorder.entity;
using invetario_api.Modules.images.entity;
using invetario_api.Modules.departureorder.entity;
namespace invetario_api.database
{
    public class Database : DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }

        public DbSet<Unit> units { get; set; }

        public DbSet<Product> products { get; set; }


        public DbSet<Store> stores { get; set; }

        public DbSet<ProductStore> productStores { get; set; }

        public DbSet<Provider> providers { get; set; }

        public DbSet<Client> clients { get; set; }

        public DbSet<Entryorder> entryorders { get; set; }

        public DbSet<EntryOrderDetail> entryOrderDetails { get; set; }

        public DbSet<Images> images { get; set; }

        public DbSet<Departureorder> departureorders { get; set; }

        public DbSet<DepartureOrderDetail> departureOrderDetails { get; set; }

        public Database(DbContextOptions<Database> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
