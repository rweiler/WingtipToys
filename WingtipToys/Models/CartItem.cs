﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WingtipToys.Models {
	public class CartItem {
		[Key]
		public string ItemId { get; set; }
		public string CartId { get; set; }
		public int Quantity { get; set; }
		public DateTime DateCreated { get; set; }
		public int ProductId { get; set; }

		// Navigation Properties
		public virtual Product Product { get; set; }
	}
}