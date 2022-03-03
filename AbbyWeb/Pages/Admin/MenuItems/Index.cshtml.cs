using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.MenuItems
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.KitchenRole}")]
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
