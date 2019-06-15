using System.Collections.Generic;
using Torshia.Models.Tasks;

namespace Torshia.Services
{
	public interface ITasksService
	{
		void Create(TaskCreateInputModel model);

		IEnumerable<TaskHomeViewModel> GetAllTasks();

		TaskViewModel GetById(string id);

		void Report(string taskId, string userId);
	}
}
