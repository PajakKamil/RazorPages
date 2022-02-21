using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Models.ViewModel;
using Abby.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order
{
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
    }
}
