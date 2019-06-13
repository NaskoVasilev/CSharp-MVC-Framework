using MvcFramework;
using MvcFramework.Attributes.Security;
using MvcFramework.Results;
using Panda.Services;
using Panda.Web.ViewModels.Receipts;
using System.Linq;

namespace Panda.Web.Controllers
{
	[Authorize]
	public class ReceiptsController : Controller
	{
		private readonly IReceiptsService receiptsService;

		public ReceiptsController(IReceiptsService receiptsService)
		{
			this.receiptsService = receiptsService;
		}

		public IActionResult Index()
		{
			var receipts = receiptsService.GetUserReceipts(this.User.Username)
				.Select(r => new ReceiptViewModel()
				{
					Fee = r.Fee,
					Id = r.Id,
					IssuedOn = r.IssuedOn.ToString("dd/MM/yyyy HH:mm"),
					RecipientName = r.Recipient.Username
				})
				.ToList();

			return View(receipts);
		}
	}
}
