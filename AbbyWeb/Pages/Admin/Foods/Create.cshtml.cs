using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Foods
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Food Food { get; set; }
	    private readonly IUnitOfWork _unitOfWork;

	    public CreateModel(IUnitOfWork unitOfWork)
	    {
		    _unitOfWork = unitOfWork;
	    }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
	        if (!ModelState.IsValid)
		        return Page();
	        _unitOfWork.Food.Add(Food);
            _unitOfWork.Save();
            TempData["success"] = "Food Added Successfully";
            return RedirectToPage("Index");
        }
    }
}
