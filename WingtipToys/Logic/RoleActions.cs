using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingtipToys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Text;

namespace WingtipToys.Logic {
	internal class RoleActions {
		internal void CreateAdmin() {
			// Access the application context
			using (var context = new ApplicationDbContext()) {
				// Create the Administrator role if it doesn't exist.
				var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

				if (!roleManager.RoleExists("Administrator")) {
					var IdRoleResult = roleManager.Create(new IdentityRole("Administrator"));
					if (!IdRoleResult.Succeeded) {
						var errors = new StringBuilder();
						foreach (var item in IdRoleResult.Errors) {
							errors.AppendLine(item);
						}
						throw new Exception($"An error occured while creating the Administrator role: {errors.ToString()}");
					}
				}

				// Create the Admin user if it doesn't exist
				var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
				var adminUser = userManager.FindByName("admin@raymondweiler.com");
				if (adminUser == null) {
					adminUser = new ApplicationUser() { UserName = "admin@raymondweiler.com", Email = "admin@raymondweiler.com" };
					var IdUserResult = userManager.Create(adminUser, "$Welcome2Admin$");
					if (!IdUserResult.Succeeded) {
						// Handle the error condition if there's a problem creating the Admin user
						var errors = new StringBuilder();
						foreach (var item in IdUserResult.Errors) {
							errors.AppendLine(item);
						}
						throw new Exception($"An error occured while creating the admin user: {errors.ToString()}");
					}
				}

				// Add the Admin user to the Administration role if it isn't already
				if (!userManager.IsInRole(adminUser.Id, "Administrator")) {
					var IdUserResult = userManager.AddToRole(adminUser.Id, "Administrator");
					if (!IdUserResult.Succeeded) {
						var errors = new StringBuilder();
						foreach (var item in IdUserResult.Errors) {
							errors.AppendLine(item);
						}
						throw new Exception($"An error occured while adding the admin user to its role: {errors.ToString()}");
					}
				}
			}
		}
	}
}