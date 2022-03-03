using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
		public DetailsModel(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        [BindProperty]
        public ShoppingCart ShoppingCart { get; set; }

        public void OnGet(int id)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCart = new()
            {
                ApplicationUserId = claim.Value,
                MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(e => e.Id == id, includeProperties: "Category,Food"),
                MenuItemId = id,
            };
        }

        public async Task<IActionResult> OnPost()
		{
            if(!ModelState.IsValid)
			    return Page();
            ShoppingCart shoppingCartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
                filter: e => e.ApplicationUserId == ShoppingCart.ApplicationUserId &&
                e.MenuItemId == ShoppingCart.MenuItemId);

            if (shoppingCartFromDb == null)
            {
                _unitOfWork.ShoppingCart.Add(ShoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(e => e.ApplicationUserId == ShoppingCart.ApplicationUserId).ToList().Count);
            }
			else
			{
                _unitOfWork.ShoppingCart.IncrementCount(shoppingCartFromDb, ShoppingCart.Count);
			}
            return RedirectToPage("Index");
		}
    }
}
