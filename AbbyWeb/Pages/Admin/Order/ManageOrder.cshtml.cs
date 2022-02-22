using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]
    public class ManageOrderModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
		public List<_OrderDetailVM> OrderDetailVM { get; set; }

		public ManageOrderModel(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}


        public void OnGet()
        {
            OrderDetailVM = new();
            List<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(e => e.Status == SD.StatusSubmitted ||
            e.Status == SD.StatusInProcess).ToList();
            foreach(OrderHeader item in orderHeaders)
			{
                _OrderDetailVM individual = new _OrderDetailVM()
                {
                    OrderHeader = item,
                    OrderDetailsList = _unitOfWork.OrderDetails.GetAll(e => e.OrderId == item.Id).ToList()
                };
                OrderDetailVM.Add(individual);
			}
        }

        public IActionResult OnPostOrderInProcess(int orderId)
		{
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusInProcess);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
		}

        public IActionResult OnPostOrderReady(int orderId)
		{
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusReady);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
		}

        public IActionResult OnPostOrderCancel(int orderId)
        {
            _unitOfWork.OrderHeader.UpdateStatus(orderId, SD.StatusCancelled);
            _unitOfWork.Save();
            return RedirectToPage("ManageOrder");
        }

    }
}
