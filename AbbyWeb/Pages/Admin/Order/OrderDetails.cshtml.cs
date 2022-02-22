using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Abby.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace AbbyWeb.Pages.Admin.Order
{
	public class OrderDetailsModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;
		[BindProperty]
		public _OrderDetailVM OrderDetailVM { get; set; }

		public OrderDetailsModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public void OnGet(int id)
		{
			OrderDetailVM = new()
			{
				OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(includeProperties: "ApplicationUser", filter: e => e.Id == id),
				OrderDetailsList = _unitOfWork.OrderDetails.GetAll(e => e.OrderId == id).ToList(),
			};
		}

		public IActionResult OnPostOrderComplete(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCompleted);
			_unitOfWork.Save();
			return RedirectToPage("OrderList");
		}

		public IActionResult OnPostOrderCancel(int orderId)
		{
			_unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
			_unitOfWork.Save();
			return RedirectToPage("OrderList");
		}

		public IActionResult OnPostOrderRefund(int orderId)
		{
			OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(e=>e.Id == orderId);
			var options = new RefundCreateOptions
			{
				Reason = RefundReasons.RequestedByCustomer,
				PaymentIntent = orderHeader.PaymentIntentId,
			};

			var service = new RefundService();
			Refund refund = service.Create(options);

			_unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusRefunded);
			_unitOfWork.Save();
			return RedirectToPage("OrderList");
		}
	}
}
