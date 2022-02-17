using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe.Checkout;
using System.Security.Claims;

namespace AbbyWeb.Pages.Customer.Cart
{
	[Authorize]
	[BindProperties]                    //<---------------Pamiêtaj OOOOOOOO TYYYYYYYYYYYYYYYYYYYYYYYYYYMMMMMMMMM!! Bo inaczej bêdziesz siê dziwiæ dlaczego ci nie przypisuje!
	public class SummaryModel : PageModel
	{
		public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
		private readonly IUnitOfWork _unitOfWork;
		public OrderHeader OrderHeader { get; set; }

		public SummaryModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
			OrderHeader = new OrderHeader();
		}

		public void OnGet()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: e => e.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.Food,MenuItem.Category");
				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}
				OrderHeader.OrderTotal = Math.Round(OrderHeader.OrderTotal, 3);
			}
			ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(e => e.Id == claim.Value);
			OrderHeader.PickUpName = applicationUser.FirstName + " " + applicationUser.LastName;
			OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
		}

		public IActionResult OnPost()
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			if (claim != null)
			{
				ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(filter: e => e.ApplicationUserId == claim.Value,
					includeProperties: "MenuItem,MenuItem.Food,MenuItem.Category");

				foreach (var cart in ShoppingCartList)
				{
					OrderHeader.OrderTotal += (cart.MenuItem.Price * cart.Count);
				}

				OrderHeader.Status = SD.StatusPending;
				OrderHeader.OrderDate = System.DateTime.Now;
				OrderHeader.UserId = claim.Value;

				OrderHeader.PickUpTime = Convert.ToDateTime(OrderHeader.PickUpDate.ToShortDateString()
					+ " "
					+ OrderHeader.PickUpTime.ToShortTimeString());
				_unitOfWork.OrderHeader.Add(OrderHeader);
				_unitOfWork.Save();

				foreach (var item in ShoppingCartList)
				{
					OrderDetails orderDetails = new()
					{
						MenuItemId = item.MenuItemId,
						OrderId = OrderHeader.Id,
						Name = item.MenuItem.Name,
						Price = item.MenuItem.Price,
						Count = item.Count,
					};
					_unitOfWork.OrderDetails.Add(orderDetails);
				}
				//_unitOfWork.ShoppingCart.RemoveRange(ShoppingCartList);
				_unitOfWork.Save();


				var domain = "https://localhost:44375";
				var options = new Stripe.Checkout.SessionCreateOptions
				{
					LineItems = new List<SessionLineItemOptions>(),

					PaymentMethodTypes = new List<string>
				{
					"card",
				},
					Mode = "payment",
					SuccessUrl = domain + $"/Customer/Cart/OrderConfirmation?id={OrderHeader.Id}",
					CancelUrl = domain + "/Customer/Cart/Index",
				};

				//add line items
				foreach (var item in ShoppingCartList)
				{
					var SessionLineItem = new Stripe.Checkout.SessionLineItemOptions
					{
						// Provide the exact Price ID (for example, pr_1234) of the product you want to sell
						PriceData = new SessionLineItemPriceDataOptions
						{
							//7.99->799
							UnitAmount = (long?)(item.MenuItem.Price * 100),
							Currency = "usd",
							ProductData = new SessionLineItemPriceDataProductDataOptions
							{
								Name = item.MenuItem.Name,
							},
						},
						Quantity = item.Count,
					};
					options.LineItems.Add(SessionLineItem);
				}
				var service = new Stripe.Checkout.SessionService();
				Stripe.Checkout.Session session = service.Create(options);
				OrderHeader.SessionId = session.Id;
				OrderHeader.PaymentIntentId = session.PaymentIntentId;
				Response.Headers.Add("Location", session.Url);
				_unitOfWork.Save();
				return new StatusCodeResult(303);
			}
			return Page();
		}

	}
}