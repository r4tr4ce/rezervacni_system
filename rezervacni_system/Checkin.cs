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
    public partial class Checkin : Form
    {
        public Checkin()
        {
            InitializeComponent();
            roomText.Text = Mapa.clickedBtn.Text.Substring(Mapa.clickedBtn.Text.Length - 1);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string guestName = guestText.Text;
            int nights = int.Parse(nightsText.Text);
            int roomNr = int.Parse(roomText.Text);
            DateTime selectedDate = monthCalendar1.SelectionStart;
            AddReservation(guestName, nights, selectedDate,roomNr);
        }
        public void AddReservation(string guestName, int nights, DateTime checkin, int roomNr)
        {
            Rezervace newReservation = new Rezervace(roomNr, guestName,nights,checkin);

            priceText.Text = newReservation.totalPrice.ToString() + " Kč";
            checkoutText.Text = newReservation.checkout.ToString("dd.MM.yyyy");

            Form1.rezervace.Add(newReservation);
            Form1.saveResDb(newReservation);
        }
    }
}
