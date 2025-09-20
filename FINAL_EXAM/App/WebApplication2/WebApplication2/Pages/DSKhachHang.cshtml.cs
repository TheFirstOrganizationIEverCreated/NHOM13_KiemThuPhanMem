using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Pages
{
    [Authorize(Roles = "admin")]
    public class DSKhachHangModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DSKhachHangModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public class ClientInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
        }
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "Select * from Customers";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.Id = reader.GetInt32(0);
                                clientInfo.Name = reader.GetString(1);
                                clientInfo.Phone = reader.GetString(2);
                                clientInfo.Email = reader.GetString(3);
                                listClients.Add(clientInfo);
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
