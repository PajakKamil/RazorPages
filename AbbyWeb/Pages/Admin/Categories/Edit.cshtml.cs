using System.ComponentModel.DataAnnotations;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AbbyWeb.Pages.Admin.Categories
{
    [Authorize(Roles = $"{SD.ManagerRole}")]
    public class EditModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public EditModel(IUnitOfWork unitOfWork)
        {
	        _unitOfWork = unitOfWork;
        }

        public void OnGet(int id)
        {
	        Category = _unitOfWork.Category.GetFirstOrDefault(e => e.Id == id);
	        //Category = _db.Category.FirstOrDefault(u => u.Id == id);
        }

        public async Task<IActionResult> OnPost()
        {
	        if (Category.Name == Category.DisplayOrder.ToString())
	        {
                ModelState.AddModelError("Category.Name", "The Display Order and Name CANNOT be the same");
	        }
	        if (!ModelState.IsValid)
		        return Page();
	        _unitOfWork.Category.Update(Category);
	        _unitOfWork.Save();
	        TempData["success"] = "Category updated successfully";
            return RedirectToPage("Index");
        }
    }
}
