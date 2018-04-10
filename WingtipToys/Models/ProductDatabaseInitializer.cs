using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WingtipToys.Models {
	public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext> {
		protected override void Seed(ProductContext context) {
			GetCategories().ForEach(c => context.Categories.AddOrUpdate(c));
			GetProductOptions().ForEach(p => context.ProductOptions.AddOrUpdate(p));
			GetProductOptionItems().ForEach(i => context.ProductOptionItems.AddOrUpdate(i));
			GetProducts().ForEach(p => context.Products.AddOrUpdate(p));
		}

		private static List<Category> GetCategories() {
			var categories = new List<Category> {
				new Category { CategoryId=1, CategoryName="Cars", SortOrder=1 },
				new Category { CategoryId=2, CategoryName="Planes", SortOrder=2 },
				new Category { CategoryId=3, CategoryName="Trucks", SortOrder=3 },
				new Category { CategoryId=4, CategoryName="Boats", SortOrder=4 },
				new Category { CategoryId=5, CategoryName="Rockets", SortOrder=5 }
			};
			return categories;
		}

		private static List<ProductOption> GetProductOptions() {
			var productOptions = new List<ProductOption> {
				new ProductOption { ProductOptionId=1,	Name="Colour", SortOrder=1 },
				new ProductOption { ProductOptionId=2,	Name="Scale", SortOrder=2}
			};
			return productOptions;
		}

		private static List<ProductOptionItem> GetProductOptionItems() {
			var productOptionItems = new List<ProductOptionItem> {
				new ProductOptionItem { Name="Red", ProductOptionId=1, SortOrder=1 },
				new ProductOptionItem { Name="Green", ProductOptionId=1, SortOrder=2 },
				new ProductOptionItem { Name="Blue", ProductOptionId=1, SortOrder=3 },
				new ProductOptionItem { Name="Black", ProductOptionId=1, SortOrder=4 },
				new ProductOptionItem { Name="Orange", ProductOptionId=1, SortOrder=5 },

				new ProductOptionItem { Name="G", ProductOptionId=2, SortOrder=6 },
				new ProductOptionItem { Name="S", ProductOptionId=2, SortOrder=7 },
				new ProductOptionItem { Name="O", ProductOptionId=2, SortOrder=8 },
				new ProductOptionItem { Name="HO", ProductOptionId=2, SortOrder=9 },
				new ProductOptionItem { Name="N", ProductOptionId=2, SortOrder=10 },
				new ProductOptionItem { Name="Z", ProductOptionId=2, SortOrder=11 }
			};
			return productOptionItems;
		}

		private static List<Product> GetProducts() {
			var products = new List<Product> {
				new Product {
					Name="Convertible Car",
					Description="This convertible car is fast! The engine is powered by a neutrino based battery (not included). Power it up and let it go!",
					ImagePath="carconvert.png",
					UnitPrice=22.50m,
					CategoryId=1
				},
				new Product {
					Name="Old-time Car",
					Description="There's nothing old about this toy car, except it's looks. Compatible with other old toy cars.",
					ImagePath="carearly.png",
					UnitPrice=15.95m,
					CategoryId=1
				},
				new Product {
					Name="Fast Car",
					Description="Yes this car is fast, but it also floats in water.",
					ImagePath="carfast.png",
					UnitPrice=32.99m,
					CategoryId=1
				},
				new Product {
					Name="Super Fast Car",
					Description="Use this super fast car to entertain guests. Lights and doors work!",
					ImagePath="carfaster.png",
					UnitPrice=8.95m,
					CategoryId=1
				},
				new Product {
					Name="Old Style Racer",
					Description="This old style racer can fly (with user assistance). Gravity controls flight duration. No batteries required.",
					ImagePath="carracer.png",
					UnitPrice=34.95m,
					CategoryId=1
				},
				new Product {
					Name="Ace Plane",
					Description="Authentic airplane toy. Features realistic color and details.",
					ImagePath="planeace.png",
					UnitPrice=95.00m,
					CategoryId=2
				},
				new Product {
					Name="Glider",
					Description="This fun glider is made from real balsa wood. Some assembly required.",
					ImagePath="planeglider.png",
					UnitPrice=4.95m,
					CategoryId=2
				},
				new Product {
					Name="Paper Plane",
					Description="This paper plane is like no other paper plane. Some folding required.",
					ImagePath="planepaper.png",
					UnitPrice=2.95m,
					CategoryId=2
				},
				new Product {
					Name="Propeller Plane",
					Description="Rubber band powered plane features two wheels.",
					ImagePath="planeprop.png",
					UnitPrice=32.95m,
					CategoryId=2
				},
				new Product {
					Name="Early Truck",
					Description="This toy truck has a real gas powered engine. Requires regular tune ups.",
					ImagePath="truckearly.png",
					UnitPrice=15.00m,
					CategoryId=3
				},
				new Product {
					Name="Fire Truck",
					Description="You will have endless fun with this one quarter sized fire truck.",
					ImagePath="truckfire.png",
					UnitPrice=26.00m,
					CategoryId=3
				},
				new Product {
					Name="Big Truck",
					Description="This fun toy truck can be used to tow other trucks that are not as big.",
					ImagePath="truckbig.png",
					UnitPrice=29.00m,
					CategoryId=3
				},
				new Product {
					Name="Big Ship",
					Description="Is it a boat or a ship. Let this floating vehicle decide by using its artifically intelligent computer brain!",
					ImagePath="boatbig.png",
					UnitPrice=95.00m,
					CategoryId=4,
				},
				new Product {
					Name="Paper Boat",
					Description="Floating fun for all! This toy boat can be assembled in seconds. Floats for minutes! Some folding required.",
					ImagePath="boatpaper.png",
					UnitPrice=4.95m,
					CategoryId=4
				},
				new Product {
					Name="Sail Boat",
					Description="Put this fun toy sail boat in the water and let it go!",
					ImagePath="boatsail.png",
					UnitPrice=42.95m,
					CategoryId=4
				},
				new Product {
					Name="Rocket",
					Description="This fun rocket will travel up to a height of 200 feet.",
					ImagePath="rocket.png",
					UnitPrice=122.95m,
					CategoryId=5
				}
			};
			return products;
		}
	}
}