using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace WebApplication2.Pages
{
    [Authorize(Roles = "admin")]
    public class EditSanModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public EditSanModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public class PlacesInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public decimal Price { get; set; }
            public string Location { get; set; }

        }
        public List<PlacesInfo> placesInfos = new List<PlacesInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Places";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PlacesInfo placesInfo = new PlacesInfo();
                                placesInfo.Id = reader.GetInt32(0);
                                placesInfo.Name = reader.GetString(1);
                                placesInfo.Type = reader.GetString(2);
                                placesInfo.Price = reader.GetDecimal(3);
                                placesInfo.Location = reader.GetString(4);
                                placesInfos.Add(placesInfo);
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
    }
}
