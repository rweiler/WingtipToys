using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingtipToys.Models;

namespace WingtipToys.Logic {
	public class AddProducts {

		public bool AddProduct(string ProductName, string ProductDescription, string ProductPrice, string ProductCategory, string ProductImagePath) {
			var myProduct = new Product() {
				ProductName = ProductName,
				Description = ProductDescription,
				UnitPrice = Convert.ToDouble(ProductPrice),
				ImagePath = ProductImagePath,
				CategoryId = Convert.ToInt32(ProductCategory)
			};

			using (var _db = new ProductContext()) {
				// Add product to db
				_db.Products.Add(myProduct);
				_db.SaveChanges();
			}
			// Success
			return true;
		}
	}
}