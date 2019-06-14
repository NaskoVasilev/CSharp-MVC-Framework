using Musaca.Data;
using Musaca.Data.Models;
using Musaca.Data.Models.Enums;
using Musaca.Models.Orders;
using Musaca.Models.Products;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Musaca.Services
{
	public class OrdersService : IOrdersService
	{
		private readonly MusacaDbContext context;

		public OrdersService(MusacaDbContext context)
		{
			this.context = context;
		}

		public void AddProductToOrder(string orderId, string productId)
		{
			OrderProduct orderProduct = new OrderProduct { OrderId = orderId, ProductId = productId };
			context.OrderProducts.Add(orderProduct);
			context.SaveChanges();
		}

		public void Cashout(string orderId, string userId)
		{
			Order order = context.Orders.FirstOrDefault(o => o.Id == orderId);

			if(order == null)
			{
				return;
			}

			order.Status = OrderStatus.Completed;
			this.Create(userId);
			context.SaveChanges();
		}

		public Order Create(string userId)
		{
			Order order = new Order
			{
				CashierId = userId,
				IssuedOn = DateTime.UtcNow,
			};

			context.Orders.Add(order);
			context.SaveChanges();

			return order;
		}

		public IEnumerable<CashierOrderViewModel> GetCashierCompletedOrders(string cashierId)
		{
			var orders = context.Orders.Where(o => o.CashierId == cashierId && o.Status == OrderStatus.Completed)
				.Select(o => new CashierOrderViewModel
				{
					Cashier = o.Cashier.Username,
					Id = o.Id,
					IssuedOn = o.IssuedOn.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
					Total = o.Products.Sum(p => p.Product.Price)
				})
				.ToList();

			return orders;
		}

		public OrderViewModel GetUserActiveOrder(string userId)
		{
			var activeOrder = this.context.Orders
				.Where(o => o.CashierId == userId)
				.Where(o => o.Status == OrderStatus.Active)
				.Select(o => new OrderViewModel
				{
					Id = o.Id,
					Products = o.Products.Select(op => new ProductViewModel()
					{
						Name = op.Product.Name,
						Price = op.Product.Price
					})
					.ToList()
				})
				.FirstOrDefault();

			if(activeOrder == null)
			{
				Order newOrder = Create(userId);
				activeOrder = new OrderViewModel { Id = newOrder.Id, Products = new List<ProductViewModel>() };
			}

			return activeOrder;
		}
	}
}
