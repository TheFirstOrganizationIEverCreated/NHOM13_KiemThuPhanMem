using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication2.Pages
{
    public class LogoutModel : PageModel
    {
        public async Task OnGet()
        {
            // Hủy phiên đăng nhập
            await HttpContext.SignOutAsync("MyCookieAuth");

            // Chuyển hướng về trang đăng nhập
            Response.Redirect("/TaiKhoan");
        }
    }
}
