using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace WingtipToys.Logic {

	public class NVPAPICaller {
		// Flag that determines the PayPal environment (live or sandbox)
		private const bool bSandbox = true;
		private const string CVV2 = "CVV2";

		// Live strings
		private string pEndPointURL = "https://api-3t.paypal.com/nvp";
		private string host = "www.paypal.com";

		// Sandbox strings
		private string pEndPointURL_SB = "https://api-3t.sandbox.paypal.com/nvp";
		private string host_SB = "www.sandbox.paypal.com";

		private const string SIGNATURE = "SIGNATURE";
		private const string PWD = "PWD";
		private const string ACCT = "ACCT";

		public string APIUsername = "wingtiptoydeveloper-facilitator_api1.raymondweiler.com";
		private string APIPassword = "HGTESZLTUP48YJET";
		private string APISignature = "AEpkQeo.7CeU2vMwvJUpaJz5A5pwAa8389GRkVNGyJ9CeQe3wmhRYCVe";
		private string Subject = "";
		private string BNCode = "PP-ECWizard";

		// HttpWebRequest Timeout specified in milliseconds
		private const int Timeout = 15000;
		private static readonly string[] SECURED_NVPS = new string[] { ACCT, CVV2, SIGNATURE, PWD };


		public void SetCredentials(string userId, string pwd, string signature) {
			APIUsername = userId;
			APIPassword = pwd;
			APISignature = signature;
		}

		public bool ShortcutExpressCheckout(string amt, ref string token, ref string retMsg) {
			if (bSandbox) {
				pEndPointURL = pEndPointURL_SB;
				host = host_SB;
			}

			var returnURL = "https://localhost:44324/Checkout/CheckoutReview.aspx";
			var cancelURL = "https://localhost:44324/Checkout/CheckoutCancel.aspx";

			var encoder = new NVPCodec {
				["METHOD"] = "SetExpressCheckout",
				["RETURNURL"] = returnURL,
				["CANCELURL"] = cancelURL,
				["BRANDNAME"] = "Wingtip Toys Sample Application",
				["PAYMENTREQUEST_0_AMT"] = amt,
				["PAYMENTREQUEST_0_ITEMAMT"] = amt,
				["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale",
				["PAYMENTREQUEST_0_CURRENCYCODE"] = "CAD"
			};

			// Get the Shopping Cart Products
			using (var myCartOrders = new ShoppingCartActions()) {
				var myOrderList = myCartOrders.GetCartItems();

				for (int i = 0; i < myOrderList.Count; i++) {
					encoder[$"L_PAYMENTREQUEST_0_NAME{i}"] = myOrderList[i].Product.ProductName;
					encoder[$"L_PAYMENTREQUEST_0_AMT{i}"] = myOrderList[i].Product.UnitPrice.ToString();
					encoder[$"L_PAYMENTREQUEST_0_QTY{i}"] = myOrderList[i].Quantity.ToString();
				}
			}

			var pStrrequestforNvp = encoder.Encode();
			var pStresponsenvp = HttpCall(pStrrequestforNvp);

			var decoder = new NVPCodec();
			decoder.Decode(pStresponsenvp);

			var strAck = decoder["ACK"].ToLower();
			if (strAck != null && (strAck == "success" || strAck == "successwithwarning")) {
				token = decoder["TOKEN"];
				var ECURL = $"https://{host}/cgi-bin/webscr?cmd=_express-checkout&token={token}";
				retMsg = ECURL;
				return true;
			} else {
				retMsg = $"ErrorCode={decoder["L_ERRORCODE0"]}&Desc={decoder["L_SHORTMESSAGE0"]}&Desc2={decoder["L_LONGMESSAGE0"]}";
				return false;
			}
		}

		public bool GetCheckoutDetails(string token, ref string payerId, ref NVPCodec decoder, ref string retMsg) {
			if (bSandbox) {
				pEndPointURL = pEndPointURL_SB;
			}

			var encoder = new NVPCodec();
			encoder["METHOD"] = "GetExpressCheckoutDetails";
			encoder["TOKEN"] = token;

			var pStrrequestforNvp = encoder.Encode();
			var pStresponsenvp = HttpCall(pStrrequestforNvp);

			decoder = new NVPCodec();
			decoder.Decode(pStresponsenvp);

			var strAck = decoder["ACK"].ToLower();
			if (strAck != null && (strAck == "success" || strAck == "successwithwarning")) {
				payerId = decoder["PAYERID"];
				return true;
			} else {
				retMsg = $"ErrorCode={decoder["L_ERRORCODE0"]}&Desc={decoder["L_SHORTMESSAGE0"]}&Desc2={decoder["L_LONGMESSAGE0"]}";
				return false;
			}
		}

		public bool DoCheckoutPayment(string finalPaymentAmount, string token, string payerId, ref NVPCodec decoder, ref string retMsg) {
			if (bSandbox) {
				pEndPointURL = pEndPointURL_SB;
			}

			var encoder = new NVPCodec {
				["METHOD"] = "DoExpressCheckoutPayment",
				["TOKEN"] = token,
				["PAYERID"] = payerId,
				["PAYMENTREQUEST_0_AMT"] = finalPaymentAmount,
				["PAYMENTREQUEST_0_CURRENCYCODE"] = "CAD",
				["PAYMENTREQUEST_0_PAYMENTACTION"] = "Sale"
			};

			var pStrrequestforNvp = encoder.Encode();
			var pStresponsenvp = HttpCall(pStrrequestforNvp);

			decoder = new NVPCodec();
			decoder.Decode(pStresponsenvp);

			var strAck = decoder["ACK"].ToLower();
			if (strAck != null && (strAck == "success" || strAck == "successwithwarning")) {
				return true;
			} else {
				retMsg = $"ErrorCode={decoder["L_ERRORCODE0"]}&Desc={decoder["L_SHORTMESSAGE0"]}&Desc2={decoder["L_LONGMESSAGE0"]}";
				return false;
			}
		}

		public string HttpCall(string nvpRequest) {
			var url = pEndPointURL;

			var strPost = $"{nvpRequest}&{BuildCredentialsNVPString()}&BUTTONSOURCE={HttpUtility.UrlEncode(BNCode)}";

			var objRequest = (HttpWebRequest)WebRequest.Create(url);
			objRequest.Timeout = Timeout;
			objRequest.Method = "POST";
			objRequest.ContentLength = strPost.Length;

			try {
				using (var myWriter = new StreamWriter(objRequest.GetRequestStream())) {
					myWriter.Write(strPost);
				}
			} catch (Exception) {
				// No logging for this tutorial
			}

			// Retrieve the Response returned from the NVP API call to PayPal
			var objResponse = (HttpWebResponse)objRequest.GetResponse();
			string result;
			using (var sr = new StreamReader(objResponse.GetResponseStream())) {
				result = sr.ReadToEnd();
			}
			return result;
		}

		private string BuildCredentialsNVPString() {
			var codec = new NVPCodec();
			if (!string.IsNullOrEmpty(APIUsername)) {
				codec["USER"] = APIUsername;
			}

			if (!string.IsNullOrEmpty(APIPassword)) {
				codec["PWD"] = APIPassword;
			}

			if (!string.IsNullOrEmpty(APISignature)) {
				codec["SIGNATURE"] = APISignature;
			}

			if (!string.IsNullOrEmpty(Subject)) {
				codec["SUBJECT"] = Subject;
			}

			codec["VERSION"] = "88.0";
			return codec.Encode();
		}
	}

	public sealed class NVPCodec : NameValueCollection {

		private const string AMPERSAND = "&";
		private const string EQUALS = "=";
		private static readonly char[] AMPERSAND_CHAR_ARRAY = AMPERSAND.ToCharArray();
		private static readonly char[] EQUALS_CHAR_ARRAY = EQUALS.ToCharArray();

		public string Encode() {
			var sb = new StringBuilder();
			var firstPair = true;

			foreach (var kv in AllKeys) {
				var name = HttpUtility.UrlEncode(kv);
				var value = HttpUtility.UrlEncode(this[kv]);
				if (!firstPair) {
					sb.Append(AMPERSAND);
				}
				sb.Append(name).Append(EQUALS).Append(value);
				firstPair = false;
			}
			return sb.ToString();
		}

		public void Decode(string nvpstring) {
			Clear();
			foreach (var nvp in nvpstring.Split(AMPERSAND_CHAR_ARRAY)) {
				string[] tokens = nvp.Split(EQUALS_CHAR_ARRAY);
				if (tokens.Length >= 2) {
					string name = HttpUtility.UrlDecode(tokens[0]);
					string value = HttpUtility.UrlDecode(tokens[1]);
					Add(name, value);
				}
			}
		}

		public void Add(string name, string value, int index) {
			this.Add(GetArrayName(index, name), value);
		}

		public void Remove(string arrayName, int index) {
			this.Remove(GetArrayName(index, arrayName));
		}

		public string this[string name, int index] {
			get {
				return this[GetArrayName(index, name)];
			}
			set {
				this[GetArrayName(index, name)] = value;
			}
		}

		private static string GetArrayName(int index, string name) {
			if (index < 0) {
				throw new ArgumentOutOfRangeException("index", $"index cannot be negative: {index}");
			}
			return name + index;
		}
	}
}