using System.Linq;

namespace Torshia.Models.Tasks
{
	public class TaskHomeViewModel
	{
		public string Id { get; set; }

		public string Title { get; set; }

		public string AffectedSectors { get; set; }

		public int Level => AffectedSectors.Split(", ").Count();
	}
}
