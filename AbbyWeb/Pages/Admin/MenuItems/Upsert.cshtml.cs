using System.Net;
using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AbbyWeb.Pages.Admin.MenuItems
{
	[BindProperties]
	public class UpsertModel : PageModel
	{
		public MenuItem MenuItem { get; set; }
		public IEnumerable<SelectListItem> CategoryList { get; set; }
		public IEnumerable<SelectListItem> FoodTypeList { get; set; }

		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			MenuItem = new MenuItem();
			_webHostEnvironment = webHostEnvironment;
		}

		public void OnGet(int? id)
		{
			if (id != null)
			{
				//Edit
				//Przy edycji, wszystkie pola zostaj¹ automatycznie wype³nione
				MenuItem = _unitOfWork.MenuItem.GetFirstOrDefault(e => e.Id == id);
			}
			CategoryList = _unitOfWork.Category.GetAll().Select(e => new SelectListItem()
			{
				Text = e.Name,
				Value = e.Id.ToString()
			});

			FoodTypeList = _unitOfWork.Food.GetAll().Select(e => new SelectListItem()
			{
				Text = e.Name,
				Value = e.Id.ToString()
			});
		}

		public async Task<IActionResult> OnPost()
		{
			string webRootPath = _webHostEnvironment.WebRootPath;
			var files = HttpContext.Request.Form.Files;
			if (MenuItem.Id == 0)
			{
				//Create
				string fileName_new = Guid.NewGuid().ToString();
				var uploads = Path.Combine(webRootPath, @"images\MenuItems");
				var extension = Path.GetExtension(files[0].FileName);

				using (var filestream =
					   new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
				{
					files[0].CopyTo(filestream);
				}

				MenuItem.Image = @"\images\MenuItems\" + fileName_new + extension;
				_unitOfWork.MenuItem.Add(MenuItem);
				_unitOfWork.Save();
				return RedirectToPage("Index");
			}
			else
			{
				//Update
				var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(e => e.Id == MenuItem.Id);
				if (files.Count > 0)
				{
					string fileName_new = Guid.NewGuid().ToString();
					var uploads = Path.Combine(webRootPath, @"images\MenuItems");
					var extension = Path.GetExtension(files[0].FileName);

					//delete old image
					var oldImagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
					if (System.IO.File.Exists(oldImagePath))
					{
						System.IO.File.Delete(oldImagePath);
					}

					//new image upload
					using (var filestream =
						   new FileStream(Path.Combine(uploads, fileName_new + extension), FileMode.Create))
					{
						files[0].CopyTo(filestream);
					}
					MenuItem.Image = @"\images\MenuItems\" + fileName_new + extension;
				}
				else
				{
					MenuItem.Image = objFromDb.Image;
				}

				_unitOfWork.MenuItem.Update(MenuItem);
				_unitOfWork.Save();
			}
			return RedirectToPage("Index");
		}
	}
}
