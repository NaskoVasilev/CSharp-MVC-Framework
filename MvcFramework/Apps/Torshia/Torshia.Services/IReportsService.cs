using System.Collections.Generic;
using Torshia.Models.Reports;

namespace Torshia.Services
{
	public interface IReportsService
	{
		IEnumerable<ReportViewModel> GetAll();

		ReportDetailsViewModel GetById(string id);
	}
}
