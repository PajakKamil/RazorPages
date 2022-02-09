using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
	    private readonly IUnitOfWork _unitOfWork;

	    public IndexModel(IUnitOfWork unitOfWork)
	    {
		    _unitOfWork = unitOfWork;
	    }

		public IEnumerable<Category> Categories { get; set; }

	    public void OnGet()
	    {
		    Categories = _unitOfWork.Category.GetAll();
	    }
    }
}
