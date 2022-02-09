using System.ComponentModel.DataAnnotations;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AbbyWeb.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public DeleteModel(IUnitOfWork unitOfWork)
        {
	        _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
	        Category = _unitOfWork.Category.GetFirstOrDefault(e=>e.Id == id);
	        //Category = _db.Category.FirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
	        if (!ModelState.IsValid)
		        return Page();
	        _unitOfWork.Category.Remove(Category);
	        _unitOfWork.Save();
	        TempData["success"] = "Category deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
