using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WingtipToys {
	public static class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			var settings = new FriendlyUrlSettings {
				AutoRedirectMode = RedirectMode.Permanent
			};
			routes.EnableFriendlyUrls(settings);

			// Register custom routes for the web application
			routes.MapPageRoute("ProductsByCategoryRoute", "Category/{categoryName}", "~/ProductList.aspx");
			routes.MapPageRoute("ProductByNameRoute", "Product/{productName}", "~/ProductDetails.aspx");
		}
	}
}
