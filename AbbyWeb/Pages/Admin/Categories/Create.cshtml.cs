using System.ComponentModel.DataAnnotations;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using Abby.Utility;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Categories
{
    [Authorize(Roles = $"{SD.ManagerRole}")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }
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
	        if (Category.Name == Category.DisplayOrder.ToString())
	        {
                ModelState.AddModelError("Category.Name", "The Display Order and Name CANNOT be the same");
	        }
	        if (!ModelState.IsValid)
		        return Page();
	        _unitOfWork.Category.Add(Category);
	        _unitOfWork.Save();
	        TempData["success"] = "Category created successfully";
            return RedirectToPage("Index");
        }
    }
}
