using Abby.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Admin.Order
{
    [Authorize(Roles = $"{SD.ManagerRole},{SD.FrontDeskRole}")]
    public class OrderListModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
