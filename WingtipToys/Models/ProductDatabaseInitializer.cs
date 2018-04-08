using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace WingtipToys.Models {
	public class ProductDatabaseInitializer : DropCreateDatabaseIfModelChanges<ProductContext> {
		protected override void Seed(ProductContext context) {
			GetCategories().ForEach(c => context.Categories.AddOrUpdate(c));
			GetProducts().ForEach(p => context.Products.AddOrUpdate(p));
		}

		private static List<Category> GetCategories() {
			var categories = new List<Category> {
				new Category {
					CategoryId=1,
					CategoryName="Cars"
				},
				new Category {
					CategoryId=2,
					CategoryName="Planes"
				},
				new Category {
					CategoryId=3,
					CategoryName="Trucks"
				},
				new Category {
					CategoryId=4,
					CategoryName="Boats"
				},
				new Category {
					CategoryId=5,
					CategoryName="Rockets"
				}
			};
			return categories;
		}

		private static List<Product> GetProducts() {
			var products = new List<Product> {
				new Product {
					ProductId=1,
					ProductName="Convertible Car",
					Description="This convertible car is fast! The engine is powered by a neutrino based battery (not included). Power it up and let it go!",
					ImagePath="carconvert.png",
					UnitPrice=22.50,
					CategoryId=1
				},
				new Product {
					ProductId=2,
					ProductName="Old-time Car",
					Description="There's nothing old about this toy car, except it's looks. Compatible with other old toy cars.",
					ImagePath="carearly.png",
					UnitPrice=15.95,
					CategoryId=1
				},
				new Product {
					ProductId=3,
					ProductName="Fast Car",
					Description="Yes this car is fast, but it also floats in water.",
					ImagePath="carfast.png",
					UnitPrice=32.99,
					CategoryId=1
				},
				new Product {
					ProductId=4,
					ProductName="Super Fast Car",
					Description="Use this super fast car to entertain guests. Lights and doors work!",
					ImagePath="carfaster.png",
					UnitPrice=8.95,
					CategoryId=1
				},
				new Product {
					ProductId=5,
					ProductName="Old Style Racer",
					Description="This old style racer can fly (with user assistance). Gravity controls flight duration. No batteries required.",
					ImagePath="carracer.png",
					UnitPrice=34.95,
					CategoryId=1
				},
				new Product {
					ProductId=6,
					ProductName="Ace Plane",
					Description="Authentic airplane toy. Features realistic color and details.",
					ImagePath="planeace.png",
					UnitPrice=95.00,
					CategoryId=2
				},
				new Product {
					ProductId=7,
					ProductName="Glider",
					Description="This fun glider is made from real balsa wood. Some assembly required.",
					ImagePath="planeglider.png",
					UnitPrice=4.95,
					CategoryId=2
				},
				new Product {
					ProductId=8,
					ProductName="Paper Plane",
					Description="This paper plane is like no other paper plane. Some folding required.",
					ImagePath="planepaper.png",
					UnitPrice=2.95,
					CategoryId=2
				},
				new Product {
					ProductId=9,
					ProductName="Propeller Plane",
					Description="Rubber band powered plane features two wheels.",
					ImagePath="planeprop.png",
					UnitPrice=32.95,
					CategoryId=2
				},
				new Product {
					ProductId=10,
					ProductName="Early Truck",
					Description="This toy truck has a real gas powered engine. Requires regular tune ups.",
					ImagePath="truckearly.png",
					UnitPrice=15.00,
					CategoryId=3
				},
				new Product {
					ProductId=11,
					ProductName="Fire Truck",
					Description="You will have endless fun with this one quarter sized fire truck.",
					ImagePath="truckfire.png",
					UnitPrice=26.00,
					CategoryId=3
				},
				new Product {
					ProductId=12,
					ProductName="Big Truck",
					Description="This fun toy truck can be used to tow other trucks that are not as big.",
					ImagePath="truckbig.png",
					UnitPrice=29.00,
					CategoryId=3
				},
				new Product {
					ProductId=13,
					ProductName="Big Ship",
					Description="Is it a boat or a ship. Let this floating vehicle decide by using its artifically intelligent computer brain!",
					ImagePath="boatbig.png",
					UnitPrice=95.00,
					CategoryId=4,
				},
				new Product {
					ProductId=14,
					ProductName="Paper Boat",
					Description="Floating fun for all! This toy boat can be assembled in seconds. Floats for minutes! Some folding required.",
					ImagePath="boatpaper.png",
					UnitPrice=4.95,
					CategoryId=4
				},
				new Product {
					ProductId=15,
					ProductName="Sail Boat",
					Description="Put this fun toy sail boat in the water and let it go!",
					ImagePath="boatsail.png",
					UnitPrice=42.95,
					CategoryId=4
				},
				new Product {
					ProductId=16,
					ProductName="Rocket",
					Description="This fun rocket will travel up to a height of 200 feet.",
					ImagePath="rocket.png",
					UnitPrice=122.95,
					CategoryId=5
				}
			};
			return products;
		}
	}
}