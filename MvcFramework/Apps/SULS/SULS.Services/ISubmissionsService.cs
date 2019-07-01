using SULS.Models;

namespace SULS.Services
{
	public interface ISubmissionsService
	{
		void Create(Submission submission);

		void Delete(string id);
	}
}
