using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;

namespace AbbyWeb.Pages.Admin.Foods
{
    [Authorize(Roles = $"{SD.ManagerRole}")]
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Food Food { get; set; }

	    private readonly IUnitOfWork _unitOfWork;

	    public DeleteModel(IUnitOfWork unitOfWork)
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

	        _unitOfWork.Food.Remove(Food);
            _unitOfWork.Save();
            TempData["success"] = "Food Deleted Successfully";
            return RedirectToPage("Index");
        }
    }
}
