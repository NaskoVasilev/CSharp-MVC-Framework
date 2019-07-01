using System;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
	public class Submission : BaseModel
	{
		public Submission()
		{
			this.CreatedOn = DateTime.Now;
		}

		[Required]
		[MaxLength(800)]
		public string Code { get; set; }

		public int ArchievedResult { get; set; }

		public DateTime CreatedOn { get; set; }

		public Problem Problem { get; set; }
		[Required]
		public string ProblemId { get; set; }

		public User User { get; set; }
		[Required]
		public string UserId { get; set; }
	}
}
