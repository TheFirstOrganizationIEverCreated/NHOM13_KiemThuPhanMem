using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static WebApplication2.Pages.EditSanModel;

namespace WebApplication2.Pages
{
    [Authorize(Roles = "admin")]
    public class CreateSanModel : PageModel
    {


        public PlacesInfo placesInfo = new PlacesInfo();
        public string errorMessage = ""; //hiển thị thông báo lỗi khi người dùng ko nhập đủ thông tin
        public void OnGet() { }
        public void OnPost() //xử lý http method: POST xảy ra khi người dùng click button 
        {
            //property Request biểu diễn các thông tin do user gửi yêu cầu (request) Server
            placesInfo.Name = Request.Form["name"];
            placesInfo.Type = Request.Form["type"];
            placesInfo.Price = decimal.Parse(Request.Form["price"]);
            placesInfo.Location = Request.Form["location"];
            if (string.IsNullOrEmpty(placesInfo.Name) || string.IsNullOrEmpty(placesInfo.Type) || placesInfo.Price <= 0 || string.IsNullOrEmpty(placesInfo.Location))
            {
                errorMessage = "All fields are required!!!";
                return;
            }
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO Places (name, type, price, location) VALUES (@name, @type, @price, @location);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", placesInfo.Name);
                        command.Parameters.AddWithValue("@type", placesInfo.Type);
                        command.Parameters.AddWithValue("@price", placesInfo.Price);
                        command.Parameters.AddWithValue("@location", placesInfo.Location);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            Response.Redirect("/DSSan");
            //clear info for next input 
            placesInfo.Name = "";
            placesInfo.Type = "";
            placesInfo.Location = "";
            Response.Redirect("/DSSan");

        }
    }
}
