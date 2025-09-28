using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace WebApplication2.Pages
{
    public class DanhSachSanModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public DanhSachSanModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        // Bang thoi gian
        public List<Field> FieldList = new List<Field>();
        public class Field
        {
            public string Name { get; set; }
            public List<string> SlotStatuses = new List<string>(); // Mỗi phần tử tương ứng 1 ca
        }
        public class Booking
        {
            public int PlaceId { get; set; }
            public int TimeSlotId { get; set; }
            public DateTime BookingDate { get; set; }
        }

        public class TimeSlot
        {
            public int TimeSlotId { get; set; }
            public string SlotLabel { get; set; }
        }
        [BindProperty(SupportsGet = true)]
        public DateTime SelectedDate { get; set; } = DateTime.Today;

        // Chi tiet san
        public List<PlacesInfo> listPlaces = new List<PlacesInfo>();
        public List<TimeSlot> TimeSlots = new List<TimeSlot>();
        public class PlacesInfo
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public decimal Price { get; set; }
            public string Location { get; set; }
            public string Img_URl { get; set; }
            public List<string> SlotStatuses { get; set; } = new List<string>();
        }
        public void OnGet()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
            DateTime today = SelectedDate;
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lấy danh sách sân (Places)
                string sqlPlaces = "SELECT * FROM Places";
                using (SqlCommand command = new SqlCommand(sqlPlaces, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PlacesInfo place = new PlacesInfo
                        {
                            ID = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Price = reader.GetDecimal(3),
                            Location = reader.GetString(4),
                            Img_URl = reader.GetString(5)
                        };
                        listPlaces.Add(place);
                    }
                }

                // Lấy danh sách khung giờ (TimeSlots)
                string sqlSlots = "SELECT * FROM TimeSlots";
                using (SqlCommand command = new SqlCommand(sqlSlots, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSlot Time = new TimeSlot
                        {
                            TimeSlotId = reader.GetInt32(0),
                            SlotLabel = reader.GetString(3)
                        };
                        TimeSlots.Add(Time);
                    }
                }
                // Sau khi đã gán listPlaces và SlotStatuses
                foreach (PlacesInfo place in listPlaces)
                {
                    FieldList.Add(new Field
                    {
                        Name = place.Name,
                        SlotStatuses = place.SlotStatuses
                    });
                }
                //  Lấy danh sách đặt sân trong hôm nay (Bookings)
                string sqlBookings = "SELECT place_id, timeslot_id, booking_date FROM Bookings WHERE booking_date = @today";
                using (SqlCommand command = new SqlCommand(sqlBookings, connection))
                {
                    command.Parameters.AddWithValue("@today", today);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bookings.Add(new Booking
                            {
                                PlaceId = reader.GetInt32(0),
                                TimeSlotId = reader.GetInt32(1),
                                BookingDate = reader.GetDateTime(2)
                            });
                        }
                    }
                }
            }

            //  Gán trạng thái từng khung giờ cho mỗi sân
            foreach (PlacesInfo place in listPlaces)
            {
                foreach (TimeSlot slot in TimeSlots)
                {
                    bool isBooked = bookings.Any(b => b.PlaceId == place.ID && b.TimeSlotId == slot.TimeSlotId);
                    place.SlotStatuses.Add(isBooked ? "Đã đặt" : "Còn trống");
                }
            }
        }
    }
}
