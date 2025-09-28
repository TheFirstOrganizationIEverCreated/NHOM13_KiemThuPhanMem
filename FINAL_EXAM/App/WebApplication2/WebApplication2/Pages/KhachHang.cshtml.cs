using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Pages
{
    public class KhachHangModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public KhachHangModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public List<BookingInfo> Bookings { get; set; } = new();

        public class BookingInfo
        {
            public string PlaceName { get; set; }
            public string Type { get; set; }
            public decimal Price { get; set; }
            public string TimeSlot { get; set; }
            public DateTime BookingDate { get; set; }
        }

        public void OnGet()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                Response.Redirect("/TaiKhoan");
                return;
            }

            Name = User.FindFirstValue(ClaimTypes.Name);
            Email = User.FindFirstValue(ClaimTypes.Email);
            Phone = User.FindFirstValue("Phone");
            int userId = int.Parse(User.FindFirstValue("UserId"));
            string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = @"
                    SELECT 
                        p.name AS PlaceName,
                        p.type,
                        p.price,
                        t.start_time,
                        t.end_time,
                        b.booking_date
                    FROM Bookings b
                    INNER JOIN Places p ON b.place_id = p.place_id
                    INNER JOIN TimeSlots t ON b.timeslot_id = t.timeslot_id
                    WHERE b.customer_id = @CustomerId
                    ORDER BY b.booking_date DESC";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CustomerId", userId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Bookings.Add(new BookingInfo
                            {
                                PlaceName = reader.GetString(0),
                                Type = reader.GetString(1),
                                Price = reader.GetDecimal(2),
                                TimeSlot = $"{reader.GetTimeSpan(3):hh\\:mm} - {reader.GetTimeSpan(4):hh\\:mm}",
                                BookingDate = reader.GetDateTime(5)
                            });
                        }
                    }
                }
            }
        }
    }
}

