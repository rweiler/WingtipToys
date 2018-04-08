using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Models;
using WingtipToys.Logic;
using System.Collections.Specialized;

namespace WingtipToys {
	public partial class ShoppingCart : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			using (ShoppingCartActions usersShoppingCart = new ShoppingCartActions()) {
				decimal cartTotal = 0;
				cartTotal = usersShoppingCart.GetTotal();
				if (cartTotal > 0) {
					// Display total
					lblTotal.Text = string.Format("{0:c2}", cartTotal);
				} else {
					LabelTotalText.Text = string.Empty;
					lblTotal.Text = string.Empty;
					ShoppingCartTitle.InnerText = "Shopping Cart is Empty";
					UpdateBtn.Visible = false;
					CheckoutImageBtn.Visible = false;
				}
			}
		}

		public List<CartItem> UpdateCartItems() {
			using (var usersShoppingCart = new ShoppingCartActions()) {
				string cartId = usersShoppingCart.GetCartId();

				var cartUpdates = new ShoppingCartActions.ShoppingCartUpdates[CartList.Rows.Count];
				for (int i = 0; i < CartList.Rows.Count; i++) {
					IOrderedDictionary rowValues = new OrderedDictionary();
					rowValues = GetValues(CartList.Rows[i]);
					cartUpdates[i].ProductId = Convert.ToInt32(rowValues["ProductId"]);

					var cbRemove = (CheckBox)CartList.Rows[i].FindControl("Remove");
					cartUpdates[i].RemoveItem = cbRemove.Checked;

					var quantityTextBox = (TextBox)CartList.Rows[i].FindControl("PurchaseQuantity");
					cartUpdates[i].PurchaseQuantity = Convert.ToInt32(quantityTextBox.Text);
				}
				usersShoppingCart.UpdateShoppingCartDatabase(cartId, cartUpdates);
				CartList.DataBind();
				lblTotal.Text = string.Format("{0:c2}", usersShoppingCart.GetTotal());
				return usersShoppingCart.GetCartItems();
			}
		}

		public static IOrderedDictionary GetValues(GridViewRow row) {
			var values = new OrderedDictionary();
			foreach (DataControlFieldCell cell in row.Cells) {
				if (cell.Visible) {
					// Extract values from the cell
					cell.ContainingField.ExtractValuesFromCell(values, cell, row.RowState, true);
				}
			}
			return values;
		}

		protected void UpdateBtn_Click(object sender, EventArgs e) {
			UpdateCartItems();
		}

		protected void CheckoutBtn_Click(object sender, ImageClickEventArgs e) {
			using (var usersShoppingCart = new ShoppingCartActions()) {
				Session["payment_amt"] = usersShoppingCart.GetTotal();
			}
			Response.Redirect("/Checkout/CheckoutStart.aspx");
		}

		public List<CartItem> GetShoppingCartItems() {
			ShoppingCartActions actions = new ShoppingCartActions();
			return actions.GetCartItems();
		}
	}
}