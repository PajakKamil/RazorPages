using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AbbyWeb.Pages.Customer.Home
{
	public class IndexModel : PageModel
	{
		private readonly IUnitOfWork _unitOfWork;
		public IndexModel(IUnitOfWork unitOfWork)
		{
			_unitOfWork	= unitOfWork;
		}

		public IEnumerable<MenuItem> MenuItemList { get; set; }
		public IEnumerable<Category> CategoryList { get; set; }

		public void OnGet()
		{
			MenuItemList = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,Food");
			CategoryList = _unitOfWork.Category.GetAll(orderby: e=>e.OrderBy(c => c.DisplayOrder));
		}
	}
}
