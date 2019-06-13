using Panda.Data;
using Panda.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Panda.Services
{
	public class ReceiptsService : IReceiptsService
	{

		private const decimal PriceMultiplier = 2.67M;
		private readonly PandaDbContext context;
		private readonly IUsersService usersService;

		public ReceiptsService(PandaDbContext context, IUsersService usersService)
		{
			this.context = context;
			this.usersService = usersService;
		}

		public void CreateReceipt(Package package)
		{
			Receipt receipt = new Receipt()
			{
				Fee = (decimal)package.Weight * PriceMultiplier,
				IssuedOn = DateTime.Now,
				PackageId = package.Id,
				RecipientId = package.RecipientId
			};

			context.Receipts.Add(receipt);
			context.SaveChanges();
		}

		IQueryable<Receipt> IReceiptsService.GetUserReceipts(string username)
		{
			string userId = usersService.GetUserIdByUsername(username);
			return context.Receipts.Where(r => r.RecipientId == userId);
		}
	}
}
