using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WingtipToys.Models {
	public class ProductContext : IdentityDbContext<ApplicationUser> {
		public ProductContext() : base("WingtipToys") {
		}

		public static ProductContext Create() {
			return new ProductContext();
		}

		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<CartItem> ShoppingCartItems { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderDetail> OrderDetails { get; set; }
	}
}