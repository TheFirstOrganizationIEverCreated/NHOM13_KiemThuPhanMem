using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Pages
{
    public class TaiKhoanModel : PageModel
    {
        private readonly ILogger<TaiKhoanModel> _logger;
        public TaiKhoanModel(ILogger<TaiKhoanModel> logger)
        {
            _logger = logger;
        }
        public string loginEmail = "";
        public string loginPassword = "";
        public string loginError = "";
        public string errorMessage = ""; //hiển thị thông báo lỗi khi người dùng ko nhập đủ 
        public class ClientInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string Password { get; set; }
        }

        public ClientInfo clientInfo = new ClientInfo();
        public void OnGet()
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                Response.Redirect("/KhachHang"); // Đổi hướng URL nếu dẫ dăng nhập
            }
        }

        public void OnPost()
        {
            Microsoft.Extensions.Primitives.StringValues formAction = Request.Form["formAction"];

            if (formAction == "register")
            {
                HandleRegister();
            }
            else if (formAction == "login")
            {
                HandleLogin();
            }
        }
        private void HandleRegister()
        {
            //Đăng Ký
            clientInfo.Name = Request.Form["name"]; //thông tin từ form user đã submit 
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            clientInfo.Password = Request.Form["RegisterPassword"];
            if (string.IsNullOrEmpty(clientInfo.Name) || string.IsNullOrEmpty(clientInfo.Email)
            || string.IsNullOrEmpty(clientInfo.Phone) || string.IsNullOrEmpty(clientInfo.Password))
            {
                errorMessage = "All fields are required!!!";
                return;
            }
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                //kiểm tra
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string checkEmailSql = "SELECT COUNT(*) FROM Customers WHERE email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkEmailSql, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", clientInfo.Email);
                        int count = (int)checkCmd.ExecuteScalar();
                        if (count > 0)
                        {
                            errorMessage = "Email đã được sử dụng. Vui lòng chọn email khác.";
                            return;
                        }
                    }
                    // thêm tài khoản
                    string sql = "INSERT INTO Customers (full_name, email, phone, Password) VALUES (@name, @email, @phone, @password)";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.Name);
                        command.Parameters.AddWithValue("@email", clientInfo.Email);
                        command.Parameters.AddWithValue("@phone", clientInfo.Phone);
                        command.Parameters.AddWithValue("@password", clientInfo.Password);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Đăng ký thất bại: " + ex.Message);
                throw;
            }
        }
        private async Task HandleLogin()
        {
            //Đăng Nhập
            loginEmail = Request.Form["LoginEmail"];
            loginPassword = Request.Form["LoginPassword"];

            if (string.IsNullOrEmpty(loginEmail) || string.IsNullOrEmpty(loginPassword))
            {
                loginError = "Vui lòng nhập email và mật khẩu.";
                return;
            }

            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT customer_id, full_name, email, phone, Password, Role FROM Customers WHERE email = @email AND Password = @password";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@email", loginEmail);
                        command.Parameters.AddWithValue("@password", loginPassword);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.Id = reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Email = reader.GetString(2);
                                clientInfo.Phone = reader.GetString(3);
                                clientInfo.Password = reader.GetString(4);
                                string role = reader["Role"].ToString();
                                // Tạo danh sách Claims
                                List<Claim> claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.NameIdentifier, clientInfo.Id.ToString()),
                                new Claim(ClaimTypes.Name, clientInfo.Name),//Lưu tên người dùng
                                new Claim(ClaimTypes.Email, clientInfo.Email),// Lưu email sử dụng
                                new Claim("Phone", clientInfo.Phone), // Lưu số điện thoại
                                new Claim("UserId", clientInfo.Id.ToString()), // Lưu ID người dùng
                                new Claim(ClaimTypes.Role, role) // Lưu vai trò người dùng
                            };

                                // Tạo Identity & Principal
                                ClaimsIdentity identity = new ClaimsIdentity(claims, "MyCookieAuth");
                                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                                // Đăng nhập với Cookie
                                await HttpContext.SignInAsync("MyCookieAuth", principal);

                                // Chuyển hướng sang trang chính hoặc dashboard
                                Response.Redirect("/Index");
                            }
                            else
                            {
                                loginError = "Email hoặc mật khẩu không đúng!";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                loginError = "Lỗi khi đăng nhập: " + ex.Message;
            }
        }
    }
}
