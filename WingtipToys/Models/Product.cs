using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models {
	public class Product {

		[ScaffoldColumn(false)]
		public int ProductId { get; set; }

		[Required, StringLength(100), Display(Name = "Product Name")]
		public string Name { get; set; }

		[Required, StringLength(10000), Display(Name = "Product Description"), DataType(DataType.MultilineText)]
		public string Description { get; set; }

		[StringLength(100)]
		public string ImagePath { get; set; }

		[Display(Name = "Price")]
		public decimal? UnitPrice { get; set; }

		public int? CategoryId { get; set; }


		// Navigation Properties
		public virtual Category Category { get; set; }

		public virtual ICollection<ProductOption> ProductOptions { get; set; }
	}
}