using Abby.DataAccess.Repository.IRepository;
using Abby.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbbyWeb.ViewComponents
{
	public class ShoppingCartViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;
		public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			int count = 0;
			if (claim == null || (HttpContext.Session.GetInt32(SD.SessionCart) == null))
				return View(count);

			count = _unitOfWork.ShoppingCart.GetAll(e => e.ApplicationUserId == claim.Value).ToList().Count;
			HttpContext.Session.SetInt32(SD.SessionCart, count);
			return View(count);
		}
	}
}
