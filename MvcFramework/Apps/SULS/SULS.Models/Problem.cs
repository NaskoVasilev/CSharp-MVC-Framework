using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SULS.Models
{
	public class Problem : BaseModel
	{
		public Problem()
		{
			this.Submissions = new HashSet<Submission>();
		}

		[Required]
		[MaxLength(20)]
		public string Name { get; set; }

		public int Points { get; set; }

		public ICollection<Submission> Submissions { get; set; }
	}
}
