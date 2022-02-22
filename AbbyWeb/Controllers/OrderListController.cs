using Abby.DataAccess.Repository.IRepository;
using Abby.Utility;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderListController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;

		public OrderListController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		[HttpGet]
		[Authorize]
		public IActionResult Get(string? status = null)
		{
																	//Wielkość liter ma znaczenie
			var orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser"); 

			if (status == "cancelled")
				orderHeaderList = orderHeaderList.Where(x => x.Status == SD.StatusCancelled || x.Status == SD.StatusRejected);
			else if(status == "completed")
				orderHeaderList = orderHeaderList.Where(x => x.Status == SD.StatusCompleted);
			else if(status == "ready")
				orderHeaderList= orderHeaderList.Where(x => x.Status == SD.StatusReady);
			else
				orderHeaderList = orderHeaderList.Where(x => x.Status == SD.StatusInProcess || x.Status == SD.StatusSubmitted);

			return Json(new {data = orderHeaderList});
		}
	}
}