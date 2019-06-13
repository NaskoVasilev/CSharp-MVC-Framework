using Panda.Data.Models;
using Panda.Data.Models.Enums;
using System.Linq;

namespace Panda.Services
{
	public interface IPackagesService
	{
		void CreatePackage(Package package);

		IQueryable<Package> GetByStatus(PackageStatus status, string username);

		void DeliverPackage(string id);
	}
}
