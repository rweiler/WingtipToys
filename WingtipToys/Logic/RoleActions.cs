using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WingtipToys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WingtipToys.Logic {
	internal class RoleActions {
		internal void CreateAdmin() {
			// Access the application context
			using (var context = new ProductContext()) {
				// Create a RoleStore object by using the ApplicationDbContext object.
				// The RoleStore is only allowed to contain IdentityRole objects.
				var roleStore = new RoleStore<IdentityRole>(context);

				// Create a RoleManager object that is only allowed to contain IdentityRole objects
				// When creating the RoleManager object, you pass in (as a parameter) a new RoleStore object
				var roleManager = new RoleManager<IdentityRole>(roleStore);

				// Then you create the "Administrator" role if it doesn't already exist.
				if (!roleManager.RoleExists("Administrator")) {
					var IdRoleResult = roleManager.Create(new IdentityRole("Administrator"));
					if (!IdRoleResult.Succeeded) {
						// Handle the error condition if there's a problem creating the RoleManager object.
					}
				}

				// Create a UserManager object based on the UserStore object and the ApplicationDbContect object.
				// Note that you can create new objects and use them as parameters in a single line of code, rather than using multiple lines of code, as you did for the RoleManager object
				var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
				var appUser = new ApplicationUser() {
					UserName = "Admin",
					Email = "admin@example.com"
				};
				var IdUserResult = userManager.Create(appUser, "Pa$$word");

				// If the new "Admin" user was successfully created, add the "Admin" user to the "Administration" role
				if (IdUserResult.Succeeded) {
					IdUserResult = userManager.AddToRole(appUser.Id, "Administrator");
					if (!IdUserResult.Succeeded) {
						// Handle the error conditoin if there's a problem adding the user to the role.
					}
				} else {
					// Handle the error condition if there's a problem creating the new user.
				}
			}
		}
	}
}