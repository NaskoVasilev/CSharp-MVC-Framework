using Panda.Data.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Data.Models
{
	public class Package : BaseModel
	{
		[Required]
		[MaxLength(20)]
		public string Description { get; set; }

		public double Weight { get; set; }

		public string ShippingAddress { get; set; }

		public PackageStatus Status { get; set; }

		public DateTime EstimatedDeliveryDate { get; set; }

		[Required]
		public string RecipientId { get; set; }
		public User Recipient  { get; set; }

	}
}
