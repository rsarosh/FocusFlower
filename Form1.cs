using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Focus
{
    public partial class Form1 : Form
    {
        Engine engine = new Engine();
        
        public Form1()
        {
            InitializeComponent();
            this.listBox1.DrawMode = DrawMode.OwnerDrawFixed;
            System.Timers.Timer timer = new System.Timers.Timer(Constants.LOGGING_INTERVAL); //fire every sec 
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }
        

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            engine.StartLogging();
          
        }


        private void button1_Click(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        //called from timer
        private void RefreshScreen(object sender, ElapsedEventArgs e)
        {
            RefreshScreen();
        }

        //called from button
        private void RefreshScreen()
        {
            engine.DrawLines(pictureBox1.CreateGraphics());
            engine.FillListBox(listBox1);
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            CustomListbox item = listBox1.Items[e.Index] as CustomListbox; // Get the current item and cast it to MyListBoxItem
            if (item != null)
            {
                e.Graphics.DrawString( // Draw the appropriate text in the ListBox
                    item.Message, // The message linked to the item
                    listBox1.Font, // Take the font from the listbox
                    new SolidBrush(item.ItemColor), // Set the color 
                    0, // X pixel coordinate
                    e.Index * listBox1.ItemHeight // Y pixel coordinate.  Multiply the index by the ItemHeight defined in the listbox.
                );
            }
            else
            {
                // The item isn't a CustomListbox, do something about it
            }

        }

     

    }
}
