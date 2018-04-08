using System;
using System.Diagnostics;
using WingtipToys.Logic;

namespace WingtipToys {
	public partial class AddToCart : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			string rawId = Request.QueryString["ProductId"];
			int productId;
			if (!string.IsNullOrEmpty(rawId) && int.TryParse(rawId, out productId)) {
				using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions()) {
					usersShoppingCart.AddToCart(Convert.ToInt32(rawId));
				}
			} else {
				Debug.Fail("ERROR: We should never get to AddToCart.aspx without a ProductId.");
				throw new Exception("ERROR: It is illegal to load AddToCart.aspx without setting a ProductId.");
			}
			Response.Redirect("ShoppingCart.aspx");
		}
	}
}