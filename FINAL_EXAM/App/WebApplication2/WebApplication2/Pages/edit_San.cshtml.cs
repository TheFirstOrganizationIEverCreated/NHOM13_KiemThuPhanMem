using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static WebApplication2.Pages.EditSanModel;

namespace WebApplication2.Pages
{
    [Authorize(Roles = "admin")]

    public class edit_SanModel : PageModel
    {
        public PlacesInfo placesInfo = new PlacesInfo();
        public string errorMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Places where place_id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("id", id);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                placesInfo.Id = reader.GetInt32(0);
                                placesInfo.Name = reader.GetString(1);
                                placesInfo.Type = reader.GetString(2);
                                placesInfo.Price = reader.GetDecimal(3);
                                placesInfo.Location = reader.GetString(4);
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
            placesInfo.Id = id;
            placesInfo.Name = Request.Form["name"];
            placesInfo.Type = Request.Form["type"];
            placesInfo.Price = decimal.Parse(Request.Form["price"]);
            placesInfo.Location = Request.Form["location"];
            //check all fields are filled 
            if (string.IsNullOrEmpty(placesInfo.Name) || string.IsNullOrEmpty(placesInfo.Type) || placesInfo.Price <= 0)
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
                    string sql = "UPDATE Places" + " SET name=@name, type=@type, price=@price, location=@location" + " WHERE place_id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@name", placesInfo.Name);
                        command.Parameters.AddWithValue("@type", placesInfo.Type);
                        command.Parameters.AddWithValue("@price", placesInfo.Price);
                        command.Parameters.AddWithValue("@id", placesInfo.Id);
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
        }
    }
}
