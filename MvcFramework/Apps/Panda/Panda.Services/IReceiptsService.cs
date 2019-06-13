using Panda.Data.Models;
using System.Linq;

namespace Panda.Services
{
	public interface IReceiptsService
	{
		void CreateReceipt(Package package);

		IQueryable<Receipt> GetUserReceipts(string username);
	}
}
