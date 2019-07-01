using SULS.ViewModels.Submissions;
using System.Collections.Generic;

namespace SULS.ViewModels.Problems
{
	public class ProblemDetailsViewModel
	{
		public IEnumerable<SubmissionViewModel> Submissions { get; set; }

		public int MaxPoints { get; set; }

		public string Name { get; set; }

	}
}
