using Abby.DataAccess.Repository.IRepository;
using Abby.Models;
using AbbyWeb.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Foods
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
	        _unitOfWork = unitOfWork;
        }

        public IEnumerable<Abby.Models.Food> Foods { get; set; }

        public void OnGet()
        {
	        Foods = _unitOfWork.Food.GetAll();
        }
    }
}
