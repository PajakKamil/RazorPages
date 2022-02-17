using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;

namespace AbbyWeb.Pages.Customer.Cart
{
    public class OrderConfirmationModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public int OrderId { get; set; }

		public OrderConfirmationModel(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public void OnGet(int id)
        {
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(e => e.Id == id);
            if (orderHeader.SessionId == null)
                return;
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            if(session.PaymentStatus.ToLower() == "paid")
			{
                orderHeader.Status = SD.StatusSubmitted;
                _unitOfWork.Save();
			}
            //usuni�cie z w�zka wszystkich element�w, poniewa� tranzakcja posz�a!
            List<ShoppingCart> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(e => e.ApplicationUserId == orderHeader.UserId).ToList();
            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            OrderId = id;
        }
    }
}
