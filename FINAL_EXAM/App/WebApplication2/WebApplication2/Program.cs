namespace WebApplication2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            // dịch vụ xác thực Cookie
            builder.Services.AddAuthentication("MyCookieAuth")
            .AddCookie("MyCookieAuth", options =>
            {
                options.LoginPath = "/TaiKhoan";   // trang đăng nhập nếu chưa có cookie
                options.LogoutPath = "/Logout";    // đường dẫn đăng xuất
                options.AccessDeniedPath = "/AccessDenied"; // nếu bị chặn

            });
            builder.Services.AddAuthorization();
            builder.Services.AddRazorPages();

            WebApplication app = builder.Build();

            // Middleware xử lý lỗi
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //Thêm xác thực trước authorization
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.Run();
        }
    }
}
