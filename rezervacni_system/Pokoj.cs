 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rezervacni_system
{
    public class Pokoj
    {
        public int roomNr { get; set; }
        public bool occupied { get; set; }
        public int price { get; set; }
        public Pokoj(int roomNr, int price, bool occupied = false)
        {
            this.roomNr = roomNr;
            this.occupied = occupied;
            this.price = price;
        }
    }
}
