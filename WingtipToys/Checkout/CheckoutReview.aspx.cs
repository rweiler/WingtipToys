using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Logic;
using WingtipToys.Models;

namespace WingtipToys.Checkout {
	public partial class CheckoutReview : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				var payPalCaller = new NVPAPICaller();

				var retMsg = "";
				var token = Session["token"].ToString();
				var payerId = "";
				var decoder = new NVPCodec();

				var ret = payPalCaller.GetCheckoutDetails(token, ref payerId, ref decoder, ref retMsg);
				if (ret) {
					Session["PayerId"] = payerId;

					var myOrder = new Order();
					myOrder.OrderDate = Convert.ToDateTime(decoder["TIMESTAMP"]);
					myOrder.Username = User.Identity.Name;
					myOrder.FirstName = decoder["FIRSTNAME"];
					myOrder.LastName = decoder["LASTNAME"];
					myOrder.Address = decoder["SHIPTOSTREET"];
					myOrder.City = decoder["SHIPTOCITY"];
					myOrder.State = decoder["SHIPTOSTATE"];
					myOrder.PostalCode = decoder["SHIPTOZIP"];
					myOrder.Country = decoder["SHIPTOCOUNTRYCODE"];
					myOrder.Email = decoder["EMAIL"];
					myOrder.Total = Convert.ToDecimal(decoder["AMT"]);

					// Verify total payment amount as set on CheckoutStart.aspx
					try {
						var paymentAmountOnCheckout = Convert.ToDecimal(Session["payment_amt"].ToString());
						var paymentAmountFromPayPal = Convert.ToDecimal(decoder["AMT"]);
						if (paymentAmountOnCheckout != paymentAmountFromPayPal) {
							Response.Redirect("/Checkout/CheckoutError.aspx?Desc=Amount%20total%20mismatch.");
						}
					} catch (Exception) {
						Response.Redirect("/Checkout/CheckoutError.aspx?Desc=Amount%20total%20mismatch.");
					}

					// Get DB context
					var _db = new ProductContext();

					// Add order to db
					_db.Orders.Add(myOrder);
					_db.SaveChanges();

					// Get the shopping cart items and process them.
					using (var usersShoppingCart = new ShoppingCartActions()) {
						var myOrderList = usersShoppingCart.GetCartItems();

						// Add OrderDetail information to the db for each product purchased
						foreach (var item in myOrderList) {
							// Create a new OrderDetail object
							var myOrderDetail = new OrderDetail {
								OrderId = myOrder.OrderId,
								Username = User.Identity.Name,
								ProductId = item.ProductId,
								Quantity = item.Quantity,
								UnitPrice = item.Product.UnitPrice
							};
							// Add OrderDetail to db
							_db.OrderDetails.Add(myOrderDetail);
							_db.SaveChanges();
						}
						// Set OrderId
						Session["CurrentOrderId"] = myOrder.OrderId;

						// Display Order information
						var orderList = new List<Order>();
						orderList.Add(myOrder);
						ShipInfo.DataSource = orderList;
						ShipInfo.DataBind();

						// Display OrderDetails
						OrderItemList.DataSource = myOrderList;
						OrderItemList.DataBind();
					}
				} else {
					Response.Redirect($"/Checkout/CheckoutError.aspx?{retMsg}");
				}
			}
		}

		protected void CheckoutConfirm_Click(object sender, EventArgs e) {
			Session["UserCheckoutCompleted"] = "true";
			Response.Redirect("/Checkout/CheckoutComplete.aspx");
		}
	}
}