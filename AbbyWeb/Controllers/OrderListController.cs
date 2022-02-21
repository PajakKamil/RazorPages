using Abby.DataAccess.Repository.IRepository;
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
		public IActionResult Get()
		{
																	//Wielkość liter ma znaczenie
			var orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser"); 
			return Json(new {data = orderHeaderList});
		}
	}
}