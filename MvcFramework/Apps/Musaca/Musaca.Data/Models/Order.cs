using Musaca.Data.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.Models
{
	public class Order : BaseModel
	{
		public Order()
		{
			this.Status = OrderStatus.Active;
			this.Products = new HashSet<OrderProduct>();
		}

		public OrderStatus Status { get; set; }

		public DateTime IssuedOn { get; set; }

		[Required]
		public string CashierId { get; set; }
		public User Cashier { get; set; }

		public ICollection<OrderProduct> Products { get; set; }
	}
}
