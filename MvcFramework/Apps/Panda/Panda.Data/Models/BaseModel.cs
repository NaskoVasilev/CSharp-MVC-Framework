using System;
using System.ComponentModel.DataAnnotations;

namespace Panda.Data.Models
{
	public abstract class BaseModel
	{
		public BaseModel()
		{
			this.Id = Guid.NewGuid().ToString();
		}

		[Key]
		public string Id { get; set; }
	}
}
