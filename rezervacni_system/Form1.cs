using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace rezervacni_system
{
    public partial class Form1 : Form
    {
        public static List<Pokoj> pokoje = new List<Pokoj>();
        public static List<Rezervace> rezervace = new List<Rezervace>();
        public static string connectionString = "Data Source=../../database.db;Version=3;";
        public Form1()
        {
            InitializeComponent();
            LoadRoomFromDatabase();
            LoadResFromDatabase();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form mapa = new Mapa();
            mapa.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form reservationList = new ReservationList();
            reservationList.Visible = true;
        }
        public static void saveRoomDb()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                foreach (Pokoj pokojItem in pokoje)
                {
                    string query = "INSERT INTO pokoje (roomNr, occupied, price) " +
                                   "VALUES (@roomNr, @occupied, @price)";

                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@roomNr", pokojItem.roomNr);
                        command.Parameters.AddWithValue("@occupied", pokojItem.occupied);
                        command.Parameters.AddWithValue("@price", pokojItem.price);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public static void LoadRoomFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"SELECT * FROM rooms;";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int roomNr = reader.GetInt32(0);
                            bool occupied = reader.GetBoolean(1);
                            int price = reader.GetInt32(2);

                            pokoje.Add(new Pokoj(roomNr, price, occupied));
                        }
                    }
                }
                connection.Close();
            }
        }
        public static void LoadResFromDatabase()
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = @"SELECT * FROM reservation WHERE active = 1;";

                using (SQLiteCommand command = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int roomNr = reader.GetInt32(0);
                            string guest = reader.GetString(1);
                            int nights = reader.GetInt32(2);
                            decimal totalPrice = reader.GetDecimal(3);
                            DateTime checkin = reader.GetDateTime(4);

                            rezervace.Add(new Rezervace(roomNr, guest, nights, checkin));
                        }
                    }
                }
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public static void saveResDb(Rezervace reservation)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO reservation (roomNr, guest, nights, totalPrice, checkinDate) " +
                               "VALUES (@roomNr, @guest, @nights, @totalPrice, @checkinDate)";

                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@roomNr", reservation.roomNr);
                    command.Parameters.AddWithValue("@guest", reservation.guest);
                    command.Parameters.AddWithValue("@nights", reservation.nights);
                    command.Parameters.AddWithValue("@totalPrice", reservation.totalPrice);
                    command.Parameters.AddWithValue("@checkinDate", reservation.checkin);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
