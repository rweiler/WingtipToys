using System.Data.Entity;

namespace WingtipToys.Models {
	public class ProductContext : DbContext {
		public ProductContext() 
			: base("WingtipToys") {
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<ProductOption> ProductOptions { get; set; }
		public DbSet<ProductOptionItem> ProductOptionItems { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<CartItem> ShoppingCartItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			modelBuilder.Entity<Product>()
				.HasMany<ProductOption>(p => p.ProductOptions)
				.WithMany(po => po.Products)
				.Map(ppo => {
					ppo.MapLeftKey("ProductId");
					ppo.MapRightKey("ProductOptionId");
					ppo.ToTable("ProductProductOptions");
				});
		}
	}
}