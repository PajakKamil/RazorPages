using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace AbbyWeb.Pages.Admin.Foods
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Food Food { get; set; }


	    private readonly IUnitOfWork _unitOfWork;

	    public EditModel(IUnitOfWork unitOfWork)
	    {
		    _unitOfWork = unitOfWork;
	    }



        public void OnGet(int id)
        {
	        Food = _unitOfWork.Food.GetFirstOrDefault(e => e.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
	        if (!ModelState.IsValid)
		        return Page();

	        _unitOfWork.Food.Update(Food);
            _unitOfWork.Save();
            TempData["success"] = "Food Updated Successfully";
            return RedirectToPage("Index");
        }
    }
}
