using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
	}
}
