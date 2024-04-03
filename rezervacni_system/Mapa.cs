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
    public partial class Mapa : Form
    {
        public static Button clickedBtn;
        public Mapa()
        {
            InitializeComponent();
            UpdateOccupiedRoomButtons();
        }
        private void UpdateOccupiedRoomButtons()
        {
            foreach (var pokoj in Form1.pokoje)
            {
                if (pokoj.occupied == true)
                {
                    int index = Form1.pokoje.IndexOf(pokoj);
                    if (index != -1)
                    {
                        Button button = FindButtonByName("button" + (index + 1));

                        if (button != null)
                        {
                            button.BackColor = Color.Red;
                        }
                    }
                }
            }

        }
        private Button FindButtonByName(string buttonName)
        {
            foreach (Control control in Controls)
            {
                if (control is Button && control.Name == buttonName)
                {
                    return (Button)control;
                }
            }
            return null;
        }
        private void button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                clickedBtn = clickedButton;
                RoomClicked rc = new RoomClicked();
                rc.Visible = true;
                this.Close();
            }
        }

    }
}
