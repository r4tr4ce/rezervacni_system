using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rezervacni_system
{
    public partial class RoomClicked : Form
    {
        public static Rezervace activeReservation;
        public RoomClicked()
        {
            InitializeComponent();
            textBox1.Text = Mapa.clickedBtn.Text.Substring(Mapa.clickedBtn.Text.Length - 1);
            searchRoom();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Checkin checkin = new Checkin();
            checkin.Visible = true;
            this.Close();
        }
        private void searchRoom()
        {
            activeReservation = Form1.rezervace.Find(r => r.roomNr == (int.Parse(textBox1.Text)));

            if (activeReservation != null)
            {
                textBox2.Text = activeReservation.guest;
                textBox3.Text = activeReservation.checkin.ToString("dd.MM.yyyy");
                textBox4.Text = activeReservation.checkout.ToString("dd.MM.yyyy");
                button1.Enabled = false;
            }
            else
            {
                button2.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Checkout checkout = new Checkout();
            checkout.Visible = true;
            this.Close();
        }
    }
}
