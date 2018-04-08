using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models {
	public class Category {

		[ScaffoldColumn(false)]
		public int CategoryId { get; set; }

		[Required, StringLength(100), Display(Name = "Name")]
		public string CategoryName { get; set; }

		[Display(Name = "Category Description")]
		public string Description { get; set; }

		// Navigation Properties
		public virtual ICollection<Product> Products { get; set; }
	}
}