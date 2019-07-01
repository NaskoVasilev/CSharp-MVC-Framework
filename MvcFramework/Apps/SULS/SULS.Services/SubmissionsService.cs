using SULS.Data;
using SULS.Models;
using System;

namespace SULS.Services
{
	public class SubmissionsService : ISubmissionsService
	{
		private readonly SULSContext context;
		private readonly IProblemsService problemsService;

		public SubmissionsService(SULSContext context, IProblemsService problemsService)
		{
			this.context = context;
			this.problemsService = problemsService;
		}

		public void Create(Submission submission)
		{
			context.Submissions.Add(submission);
			int maxPoints = problemsService.GetProblemPoints(submission.ProblemId);
			submission.ArchievedResult = new Random().Next(0, maxPoints);
			context.SaveChanges();
		}

		public void Delete(string id)
		{
			Submission submission = context.Submissions.Find(id);
			context.Submissions.Remove(submission);
			context.SaveChanges();
		}
	}
}
