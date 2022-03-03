using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Cart
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
        private readonly IUnitOfWork _unitOfWork;
        public double CartTotal { get; set; }

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            CartTotal = 0;
        }

        public void OnGet()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: e => e.ApplicationUserId == claim.Value,
                    includeProperties: "MenuItem,MenuItem.Food,MenuItem.Category");
                foreach(var cart in ShoppingCartList)
				{
                    CartTotal += (cart.MenuItem.Price * cart.Count);
				}
            }
        }

        public IActionResult OnPostPlus(int CartId)
		{
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(e => e.Id == CartId);
            _unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostMinus(int CartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(e => e.Id == CartId);
            if (cart.Count == 1)
            {
                OnPostRemove(CartId);
            }
            _unitOfWork.ShoppingCart.DecrementCound(cart, 1);
            return RedirectToPage("/Customer/Cart/Index");
        }

        public IActionResult OnPostRemove(int CartId)
        {
            var cart = _unitOfWork.ShoppingCart.GetFirstOrDefault(e => e.Id == CartId);
            var count = _unitOfWork.ShoppingCart.GetAll(e => e.ApplicationUserId == cart.ApplicationUserId).ToList().Count-1;
            _unitOfWork.ShoppingCart.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}