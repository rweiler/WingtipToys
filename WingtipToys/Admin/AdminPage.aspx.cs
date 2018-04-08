using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using WingtipToys.Logic;
using System.IO;

namespace WingtipToys.Admin {
	public partial class AdminPage : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			var productAction = Request.QueryString["ProductAction"];
			if (productAction == "add") {
				LabelAddStatus.Text = "Product added!";
			}

			if (productAction == "remove") {
				LabelRemoveStatus.Text = "Product removed!";
			}
		}

		protected void AddProductButton_Click(object sender, EventArgs e) {
			var fileOk = false;
			var path = Server.MapPath("~/Catalog/Images/");
			if (ProductImage.HasFile) {
				var fileExtension = Path.GetExtension(ProductImage.FileName).ToLower();
				string[] allowedExtensions = { ".gif", ".png", ".jpg", ".jpeg" };
				for (int i = 0; i < allowedExtensions.Length; i++) {
					if (fileExtension == allowedExtensions[i]) {
						fileOk = true;
					}
				}
			}
			if (fileOk) {
				try {
					// Save to Images folder
					ProductImage.PostedFile.SaveAs(path + ProductImage.FileName);
					// Save to Images/Thumbs folder.
					ProductImage.PostedFile.SaveAs($"{path}Thumbs/{ProductImage.FileName}");
				} catch (Exception ex) {
					LabelAddStatus.Text = ex.Message;
				}

				// Add product data to db
				var products = new AddProducts();
				var addSuccess = products.AddProduct(AddProductName.Text, AddProductDescription.Text, AddProductPrice.Text, ddlAddCategory.SelectedValue, ProductImage.FileName);
				if (addSuccess) {
					// Reload the page
					var pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
					Response.Redirect($"{pageUrl}?ProductAction=add");
				} else {
					LabelAddStatus.Text = "Unable to add new product to database";
				}
			} else {
				LabelAddStatus.Text = "Unable to accept file type";
			}
		}

		public IQueryable GetCategories() {
			var _db = new ProductContext();
			return _db.Categories;
		}

		public IQueryable GetProducts() {
			var _db = new ProductContext();
			return _db.Products;
		}

		protected void RemoveProductButton_Click(object sender, EventArgs e) {
			using (var _db = new ProductContext()) {
				var productId = Convert.ToInt32(ddlRemoveProduct.SelectedValue);
				var myItem = (from c in _db.Products
											where c.ProductId == productId
											select c).FirstOrDefault();
				if (myItem != null) {
					_db.Products.Remove(myItem);
					_db.SaveChanges();

					// Reload the page
					var pageUrl = Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.Count() - Request.Url.Query.Count());
					Response.Redirect($"{pageUrl}?ProductAction=remove");
				} else {
					LabelRemoveStatus.Text = "Unable to locate product";
				}
			}
		}
	}
}