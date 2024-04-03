using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rezervacni_system
{
    public partial class Checkout : Form
    {
        public Checkout()
        {
            InitializeComponent();
            textBox2.Text = RoomClicked.activeReservation.guest;
            textBox3.Text = RoomClicked.activeReservation.totalPrice.ToString() + " Kč";
        }
        public void UpdateActiveStatusInDatabase(bool activeReservation, int roomNr)
        {
            string updateQuery = "UPDATE reservation SET active = @active WHERE roomNr = @roomNr AND active = 1";

            using (SQLiteConnection connection = new SQLiteConnection(Form1.connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@active", activeReservation ? 1 : 0);
                    command.Parameters.AddWithValue("@roomNr", roomNr);
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateActiveStatusInDatabase(false, RoomClicked.activeReservation.roomNr);
            Form1.rezervace.Clear();
            Form1.LoadResFromDatabase();
            MarkRoomUnoccupied();
            this.Close();
        }
        private void MarkRoomUnoccupied()
        {
            int roomNr = RoomClicked.activeReservation.roomNr;
            Pokoj selectedRoom = Form1.pokoje.Find(p => p.roomNr == roomNr);

            if (selectedRoom != null)
            {
                selectedRoom.occupied = false;
            }
            else
            {
                throw new InvalidOperationException("Room not found.");
            }
        }
    }
}
