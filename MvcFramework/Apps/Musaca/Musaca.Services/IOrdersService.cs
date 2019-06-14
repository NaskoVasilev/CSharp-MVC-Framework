using Musaca.Data.Models;
using Musaca.Models.Orders;
using System.Collections.Generic;

namespace Musaca.Services
{
	public interface IOrdersService
	{
		OrderViewModel GetUserActiveOrder(string userId);

		Order Create(string userId);

		void AddProductToOrder(string orderId, string productId);

		void Cashout(string orderId, string userId);


		IEnumerable<CashierOrderViewModel> GetCashierCompletedOrders(string cashierId);
	}
}
