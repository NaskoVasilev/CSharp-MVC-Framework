using SULS.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.ViewModels.Home
{
	public class HomeViewModel
	{
		public IEnumerable<ProblemHomeViewModel> Problems { get; set; }
	}
}
