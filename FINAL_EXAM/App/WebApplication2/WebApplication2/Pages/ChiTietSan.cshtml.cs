using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Pages
{
    [Authorize]
    public class ChiTietSanModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public ChiTietSanModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

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
            public string Img_URl_map { get; set; }

            public List<string> SlotStatuses { get; set; } = new List<string>();
        }
        //add
        public string errorMessage = ""; //hiển thị thông báo lỗi khi người dùng ko nhập thông tin

        [BindProperty(SupportsGet = true)]
        public int PlaceId { get; set; }
        public int UserId { get; set; }
        public PlacesInfo SelectedPlace { get; set; } = new();

        [BindProperty]
        public int TimeSlotId { get; set; }

        [BindProperty]
        public DateTime BookingDate { get; set; }

        public void OnGet()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
            List<Booking> bookings = new List<Booking>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Lấy thông tin sân được chọn
                string sql = "SELECT * FROM Places WHERE place_id = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", PlaceId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            SelectedPlace.ID = reader.GetInt32(0);
                            SelectedPlace.Name = reader.GetString(1);
                            SelectedPlace.Type = reader.GetString(2);
                            SelectedPlace.Price = reader.GetDecimal(3);
                            SelectedPlace.Location = reader.GetString(4);
                            SelectedPlace.Img_URl = reader.GetString(5);
                            SelectedPlace.Img_URl_map = reader.GetString(6);
                        }
                    }
                }
                // Thêm vào listPlaces
                listPlaces.Add(SelectedPlace);
                // Lấy danh sách khung giờ
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

                // Lấy danh sách đặt sân trong hôm nay (có thể thay bằng BookingDate nếu cần)
                string sqlBookings = "SELECT place_id, timeslot_id, booking_date FROM Bookings WHERE booking_date = @today";
                using (SqlCommand command = new SqlCommand(sqlBookings, connection))
                {
                    command.Parameters.AddWithValue("@today", DateTime.Today); // Hoặc SelectedDate nếu cần
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

            // Gán trạng thái từng khung giờ cho sân
            foreach (PlacesInfo place in listPlaces)
            {
                foreach (TimeSlot slot in TimeSlots)
                {
                    bool isBooked = bookings.Any(b => b.PlaceId == place.ID && b.TimeSlotId == slot.TimeSlotId);
                    place.SlotStatuses.Add(isBooked ? "Đã đặt" : "Còn trống");
                }

                FieldList.Add(new Field
                {
                    Name = place.Name,
                    SlotStatuses = place.SlotStatuses
                });
            }
        }
        public void OnPost()
        {
            string? idStr = User.FindFirstValue("UserId");
            if (int.TryParse(idStr, out int id))
            {
                UserId = id;
            }
            string connectionString = "Data Source=localhost;Initial Catalog=SANBONG;Integrated Security=True;TrustServerCertificate=True";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Kiểm tra trùng
                    string checkSql = "SELECT COUNT(*) FROM Bookings WHERE place_id = @place AND timeslot_id = @slot AND booking_date = @date";
                    using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                    {
                        checkCmd.Parameters.AddWithValue("@place", PlaceId);
                        checkCmd.Parameters.AddWithValue("@slot", TimeSlotId);
                        checkCmd.Parameters.AddWithValue("@date", BookingDate.Date);

                        int exists = (int)checkCmd.ExecuteScalar();
                        if (exists > 0)
                        {
                            errorMessage = "Khung giờ này đã có người đặt!";
                            OnGet();
                            return;
                        }
                    }

                    // Thêm mới
                    string insertSql = "INSERT INTO Bookings (place_id, timeslot_id, booking_date, customer_id) VALUES (@place, @slot, @date, @id)";
                    using (SqlCommand insertCmd = new SqlCommand(insertSql, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@place", PlaceId);
                        insertCmd.Parameters.AddWithValue("@slot", TimeSlotId);
                        insertCmd.Parameters.AddWithValue("@date", BookingDate.Date);
                        insertCmd.Parameters.AddWithValue("@id", UserId);
                        insertCmd.ExecuteNonQuery();
                    }

                    errorMessage = "Đặt sân thành công!";
                }
            }
            catch (Exception ex)
            {
                errorMessage = "Lỗi: " + ex.Message;
            }

            // Gọi lại OnGet() để hiển thị lại dữ liệu
            OnGet();
        }

    }
}
