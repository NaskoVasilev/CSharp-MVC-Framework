using SULS.Models;
using SULS.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.Services
{
	public interface IProblemsService
	{
		void Create(Problem problem);

		IEnumerable<ProblemHomeViewModel> All();

		ProblemSubmissionViewModel GetProblemInfo(string problemId);

		int GetProblemPoints(string problemId);

		ProblemDetailsViewModel GetProblemDetails(string id);
	}
}
