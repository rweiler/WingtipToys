using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models {
	public class ProductOption {
		public int ProductOptionId { get; set; }

		[Required, StringLength(50)]
		public string Name { get; set; }

		[Required]
		public int SortOrder { get; set; }


		// Navigation Properties
		public virtual ICollection<Product> Products { get; set; }
		
		public virtual ICollection<ProductOptionItem> ProductOptionItems { get; set; }
	}
}