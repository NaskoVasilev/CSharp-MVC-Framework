using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Torshia.Data;
using Torshia.Models.Reports;

namespace Torshia.Services
{
	public class ReportsService : IReportsService
	{
		private readonly TorshiaDbContext context;

		public ReportsService(TorshiaDbContext context)
		{
			this.context = context;
		}

		public IEnumerable<ReportViewModel> GetAll()
		{
			var reports = context.Reports.Select(r => new ReportViewModel
			{
				Id = r.Id,
				Status = r.Status.ToString(),
				AffectedSectors = r.Task.AffectedSectors,
				TaskTitle = r.Task.Title
			}).ToList();

			return reports;
		}

		public ReportDetailsViewModel GetById(string id)
		{
			var report = context.Reports.Where(r => r.Id == id)
				.Select(r => new ReportDetailsViewModel
				{
					Id = r.Id,
					TaskTitle = r.Task.Title,
					TaskDescription = r.Task.Description,
					TaskParticipantNames = r.Task.Participants.Select(p => p.Participant.Name).ToList(),
					TaskDueDate = r.Task.DueDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
					ReportedOn = r.ReportedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
					Status = r.Status.ToString(),
					Reporter = r.Reporter.Username,
					AffectedSectors = r.Task.AffectedSectors
				})
				.FirstOrDefault();

			return report;
		}
	}
}
