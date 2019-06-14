using MvcFramework.Attributes.Validation;

namespace Musaca.Models.Products
{
	public class ProductCreateInputModel
	{
		[Required]
		[StringLength(5, 10)]
		public string Name { get; set; }

		[Range(typeof(decimal), "0.01", "1000000")]
		public decimal Price { get; set; }
	}
}
