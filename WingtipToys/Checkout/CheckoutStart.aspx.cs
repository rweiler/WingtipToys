using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WingtipToys.Logic;

namespace WingtipToys.Checkout {
	public partial class CheckoutStart : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			var payPalCaller = new NVPAPICaller();
			var retMsg = "";
			var token = "";

			if (Session["payment_amt"] != null) {
				var amt = Session["payment_amt"].ToString();
				var ret = payPalCaller.ShortcutExpressCheckout(amt, ref token, ref retMsg);
				if (ret) {
					Session["token"] = token;
					Response.Redirect(retMsg);
				} else {
					Response.Redirect($"/Checkout/CheckoutError.aspx?{retMsg}");
				}
			} else {
				Response.Redirect("/Checkout/CheckoutError.aspx?ErrorCode=AmtMissing");
			}
		}
	}
}