﻿using System;
using System.Linq;
using System.Web.ModelBinding;
using WingtipToys.Models;

namespace WingtipToys {
	public partial class ProductDetails : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
		}
		
		public IQueryable<Product> GetProduct([QueryString("productId")] int? productId, [RouteData] string productName) {
			var _db = new ProductContext();
			IQueryable<Product> query = _db.Products;
			if (productId.HasValue && productId > 0) {
				query = query.Where(p => p.ProductId == productId);
			} else if (!string.IsNullOrEmpty(productName)) {
				query = query.Where(p => string.Compare(p.Name, productName) == 0);
			} else {
				query = null;
			}
			return query;
		}
	}
}