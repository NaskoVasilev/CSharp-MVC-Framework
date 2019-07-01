using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SULS.Data;
using SULS.Models;
using SULS.ViewModels.Problems;
using SULS.ViewModels.Submissions;

namespace SULS.Services
{
	public class ProblemsService : IProblemsService
	{
		private readonly SULSContext context;

		public ProblemsService(SULSContext context)
		{
			this.context = context;
		}

		public IEnumerable<ProblemHomeViewModel> All()
		{
			var problems = context.Problems
				.Select(p => new ProblemHomeViewModel
				{
					Id = p.Id,
					Name = p.Name,
					Count = p.Submissions.Count
				})
				.ToList();

			return problems;
		}

		public void Create(Problem problem)
		{
			context.Problems.Add(problem);
			context.SaveChanges();
		}

		public ProblemDetailsViewModel GetProblemDetails(string id)
		{
			return this.context.Problems.Where(p => p.Id == id)
				.Select(p => new ProblemDetailsViewModel
				{
					MaxPoints = p.Points,
					Name = p.Name,
					Submissions = p.Submissions.Select(s => new SubmissionViewModel
					{
						AchievedResult = s.ArchievedResult,
						Id = s.Id,
						CreatedOn = s.CreatedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
						Username = s.User.Username
					})
					.ToList()
				})
				.FirstOrDefault();
		}

		public ProblemSubmissionViewModel GetProblemInfo(string problemId)
		{
			return this.context.Problems.Where(p => p.Id == problemId)
				.Select(p => new ProblemSubmissionViewModel { Name = p.Name, ProblemId = p.Id })
				.FirstOrDefault();
		}

		public int GetProblemPoints(string problemId)
		{
			int maxPoints = context.Problems.Where(p => p.Id == problemId)
				.Select(p => p.Points)
				.FirstOrDefault();
			return maxPoints;
		}
	}
}
