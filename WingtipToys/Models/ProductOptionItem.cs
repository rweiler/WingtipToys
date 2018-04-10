using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models {
	public class ProductOptionItem {
		public int ProductOptionItemId { get; set; }

		[Required, StringLength(50), Display(Name = "Product Option Item Name")]
		public string Name { get; set; }

		[Required]
		public int SortOrder { get; set; }

		public int ProductOptionId { get; set; }

		// Navigation Properties
		public virtual ProductOption ProductOption { get; set; }
	}
}