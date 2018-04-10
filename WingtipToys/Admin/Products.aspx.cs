using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;

namespace WingtipToys.Admin {
	public partial class Products : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
		}

		public IQueryable<Product> GetProducts() {
			var _db = new ProductContext();
			return _db.Products.Include("Category").Include("ProductOptions").OrderByDescending(p => p.Name);
		}

		public IQueryable<ProductOption> GetProductOptions() {
			var _db = new ProductContext();
			return _db.ProductOptions;
		}


		#region >>>>> gvProducts GridView Event Handlers <<<<<
		protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e) {
			// Get a reference to the sending GridView to abstract it
			var gv = (GridView)sender;

			switch (e.CommandName) {
				case "AddProductOptionToProduct":
					var gvr = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
					// Get the ProductId of the row the plus sign was clicked in
					var productId = Convert.ToInt32(e.CommandArgument.ToString());
					// Get the ProductOptionId to be added to the product
					var productOptionId = Convert.ToInt32(((DropDownList)gvr.FindControl("ddlProductOptions")).SelectedValue);
					// Add the selected ProductOption to the Product
					AddProductOptionToProduct(productId, productOptionId);
					// Refresh the GridView
					gv.DataBind();
					break;
			}
		}
		#endregion


		#region >>>>> Helper Methods <<<<<
		private void AddProductOptionToProduct(int productId, int productOptionId) {
			using (var _db = new ProductContext()) {
				// Get the product
				var product = _db.Products.FirstOrDefault(p => p.ProductId == productId);
				if (product != null) {
					// Get the product option to add to the product
					var productOption = _db.ProductOptions.FirstOrDefault(o => o.ProductOptionId == productOptionId);
					if (productOption != null) {
						product.ProductOptions.Add(productOption);
						_db.SaveChanges();
					} else {
						Response.Write($"ProductOptionId [{productOptionId}] could not be found");
					}
				} else {
					Response.Write($"ProductId [{productId}] could not be found");
				}
			}
		}

		protected string GetProductOptionsForProduct(int productId) {
			var _db =  new ProductContext();
			var product = _db.Products.FirstOrDefault(p => p.ProductId == productId);
			var productOptions = new List<string>();
			if (product != null) {
				foreach (var item in product.ProductOptions) {
					productOptions.Add(item.Name);
				}
			}
			return string.Join(", ", productOptions.ToArray());
		}
		#endregion
	}
}