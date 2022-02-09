using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Customer.Home
{
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
		public DetailsModel(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
		}

        public MenuItem MenuItem { get; set; }

        [BindProperty]
        [Range(1, 10, ErrorMessage = "Please select a count between 1 and 10")]
        public int Count { get; set; }

        public void OnGet(int id)
        {
           MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(e=>e.Id == id, includeProperties: "Category,Food");
        }
    }
}
