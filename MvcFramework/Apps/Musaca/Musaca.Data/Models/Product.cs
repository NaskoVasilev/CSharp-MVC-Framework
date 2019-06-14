using System.ComponentModel.DataAnnotations;

namespace Musaca.Data.Models
{
	public class Product : BaseModel
	{
		[Required]
		[MaxLength(10)]
		public string Name { get; set; }

		[Range(typeof(decimal), "0.01", "1000000")]
		public decimal Price { get; set; }
	}
}
