using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static WebApplication2.Pages.DSKhachHangModel;

namespace WebApplication2.Pages
{
    [Authorize(Roles = "admin")]
    public class editModel : PageModel
    {
        public ClientInfo clientInfo = new ClientInfo();
        public string errorMessage = ""; //hiển thị thông báo lỗi khi người dùng ko nhập thông tin
        public void OnGet()
        {
            String id = Request.Query["id"]; //lấy id trên url của request 
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Customers where customer_id = @id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                clientInfo.Id = reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Phone = reader.GetString(2);
                                clientInfo.Email = reader.GetString(3);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        public void OnPost() //xử lý http method: POST khi người dùng bấm submit trong form Edit Client
        {
            int.TryParse(Request.Query["id"], out int id);
            clientInfo.Id = id;
            clientInfo.Name = Request.Form["name"];
            clientInfo.Email = Request.Form["email"];
            clientInfo.Phone = Request.Form["phone"];
            //check all fields are filled 
            if (string.IsNullOrEmpty(clientInfo.Name) || string.IsNullOrEmpty(clientInfo.Email) || string.IsNullOrEmpty(clientInfo.Phone))
            {
                errorMessage = "All fields are required!!!";
                return;
            }

            //if ok,update client to database 
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "UPDATE Customers" + " SET full_name=@name, email=@email, phone=@phone" + " WHERE customer_id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", clientInfo.Name);
                        command.Parameters.AddWithValue("@email", clientInfo.Email);
                        command.Parameters.AddWithValue("@phone", clientInfo.Phone);
                        command.Parameters.AddWithValue("@id", clientInfo.Id);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            Response.Redirect("/DSKhachHang");
        }
    }
}
