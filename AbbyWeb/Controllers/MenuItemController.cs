using Abby.DataAccess.Repository.IRepository;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace AbbyWeb.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MenuItemController : Controller
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment; //Do plików, jeśnie nie operujesz na plikach - usuń

		public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
		{
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpGet]
		public IActionResult Get()
		{
																	//Wielkość liter ma znaczenie
			var menuItemlist = _unitOfWork.MenuItem.GetAll(includeProperties: "Category,Food"); 
			return Json(new {data = menuItemlist});
		}

		[HttpDelete("{id}")]
		public IActionResult Delete(int id)
		{
			//Wielkość liter ma znaczenie
			var objFromDb = _unitOfWork.MenuItem.GetFirstOrDefault(e => e.Id == id);
			//Usuwanie image
			var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, objFromDb.Image.TrimStart('\\'));
			if (System.IO.File.Exists(oldImagePath))
			{
				System.IO.File.Delete(oldImagePath);
			}
			//usuwanie z tabeli
			_unitOfWork.MenuItem.Remove(objFromDb);
			_unitOfWork.Save();
			return Json(new { success = true, message = "Delete successful."});
		}
	}
}