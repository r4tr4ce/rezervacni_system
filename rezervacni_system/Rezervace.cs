using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rezervacni_system
{
    public class Rezervace
    {
        public int roomNr { get; set; }
        public string guest { get; set; }
        public int nights { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime checkin { get; set; }
        public DateTime checkout => checkin.AddDays(nights);
        public bool active { get; set; }
        public Rezervace(int roomNr, string guest, int nights, DateTime checkin)
        {
            this.roomNr = roomNr;
            this.guest = guest;
            this.nights = nights;
            this.checkin = checkin;
            active = true;
            CalculateTotalPrice();
            MarkRoomOccupied();
        }
        private void CalculateTotalPrice()
        {
            Pokoj selectedRoom = Form1.pokoje.Find(p => p.roomNr == roomNr);

            if (selectedRoom != null)
            {
                totalPrice = selectedRoom.price * nights;
            }
            else
            {
                throw new InvalidOperationException("Room not found.");
            }
        }
        private void MarkRoomOccupied()
        {
            Pokoj selectedRoom = Form1.pokoje.Find(p => p.roomNr == roomNr);

            if (selectedRoom != null)
            {
                selectedRoom.occupied = true;
            }
            else
            {
                throw new InvalidOperationException("Room not found.");
            }
        }
    }
}
