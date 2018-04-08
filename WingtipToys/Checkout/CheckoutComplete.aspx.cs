using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Logic;
using WingtipToys.Models;

namespace WingtipToys.Checkout {
	public partial class CheckoutComplete : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				// Verify user has completed the checkout process
				if (Session["UserCheckoutCompleted"].ToString() != "true") {
					Session["UserCheckoutCompleted"] = string.Empty;
					Response.Redirect("/Checkout/CheckoutError.aspx?Desc=Unvalidated@20Checkout.");
				}

				var payPalCaller = new NVPAPICaller();
				var retMsg = "";
				var token = "";
				var finalPaymentAmount = "";
				var payerId = "";
				var decoder = new NVPCodec();

				token = Session["token"].ToString();
				payerId = Session["payerId"].ToString();
				finalPaymentAmount = Session["payment_amt"].ToString();

				var ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, payerId, ref decoder, ref retMsg);
				if (ret) {
					// Retrieve PayPal confirmation value
					var PaymentConfirmation = decoder["PAYMENTINFO_0_TRANSACTIONID"];
					TransactionId.Text = PaymentConfirmation;

					var _db = new ProductContext();
					// Get the current order id
					var currentOrderId = -1;
					if (Session["currentOrderId"].ToString() != string.Empty) {
						currentOrderId = Convert.ToInt32(Session["CurrentOrderId"]);
					}
					Order myCurrentOrder;
					if (currentOrderId >= 0) {
						// Get the order based on order id
						myCurrentOrder = _db.Orders.Single(o => o.OrderId == currentOrderId);
						// Update the order to reflect payment has been completed
						myCurrentOrder.PaymentTransactionId = PaymentConfirmation;
						// Save to db
						_db.SaveChanges();
					}

					// Clear shopping cart
					using (var usersShoppingCart = new ShoppingCartActions()) {
						usersShoppingCart.EmptyCart();
					}

					// Clear order id
					Session["CurrentOrderId"] = string.Empty;
				} else {
					Response.Redirect($"/Checkout/CheckoutError.aspx?{retMsg}");
				}
			}
		}

		protected void Continue_Click(object sender, EventArgs e) {
			Response.Redirect("~/");
		}
	}
}