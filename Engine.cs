using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Focus
{
    
    class Engine
    {
        /// <summary>
        /// Keeps the AppName and Usage Count
        /// </summary>
        public static Dictionary<string, long> dictionary = new Dictionary<string, long>();
        /// <summary>
        /// Dictionary for name to index
        /// </summary>
        public static Dictionary<string, int> nameIndex = new Dictionary<string, int>();
        /// <summary>
        /// Dicttionary for number to name
        /// </summary>
        public static Dictionary<int, string> indexName = new Dictionary<int, string>();

        /// <summary>
        /// Keeps the every sec log of activity
        /// </summary>
        public static List<int> listAudit = new List<int>();

        Colorization color;

        public Engine()
        {
            color = new Colorization();
           
        }

        public void StartLogging()
        {
            string key = SystemCalls.GetAppName();

            if (String.IsNullOrEmpty(key)) return;

            if (dictionary.ContainsKey(key))
            {
                dictionary[key] = dictionary[key] + 1; //update the counter
            }
            else
            {
                dictionary.Add(key, 1);
                nameIndex.Add(key, nameIndex.Count);
                indexName.Add(nameIndex.Count - 1, key);
            }

            listAudit.Add(nameIndex[key]);
            if (listAudit.Count > 65500) //65535 - 18 hours of data
            {
                listAudit.RemoveAt(0);
            }
        }



        public void FillListBox(ListBox listBox1)
        {
            listBox1.Items.Clear();
            for (int i = 0; i < dictionary.Count; i++)
            {
                var v = dictionary.ElementAt(i);
                int min = (int) (v.Value / 60);
                int sec = (int) (v.Value % 6);
                TimeSpan span = TimeSpan.FromSeconds(v.Value * (Constants.LOGGING_INTERVAL/1000));
                string timeLabel = span.ToString(@"hh\:mm\:ss");
                listBox1.Items.Add(new CustomListbox(color.GetColor(i), String.Format("{0}, {1}", v.Key, timeLabel)));
            }
        }


        private int GetPenThickNess(int startIndex)
        {
            int value = listAudit[startIndex];
            for (int j = startIndex + 1; j < listAudit.Count - 1; j++) 
            {
                if (listAudit[j] != value)
                    return (j - startIndex);
            }
            return 1;

        }
        public void DrawLines(Graphics g)
        {
            try
            {
                int bottomStartX = 10;
                g.Clear(Color.White);

                var rectangle = new Rectangle(Constants.RECT_X, Constants.RECT_Y, Constants.RECT_WIDTH, Constants.RECT_HEIGHT);
                var topLeft = new Point(rectangle.X, rectangle.Y);
                //var topRight = new Point((int)rectangle.Width, 0);
                var bottomLeft = new Point(bottomStartX, (int)rectangle.Height);
                //var bottomRight = new Point((int)rectangle.Width - 1, (int)rectangle.Height - 1);
                int penThickness;
                int prevApp = -1;
                var pen1 = color.GetPen(indexName[listAudit[0]], dictionary, 1); ; ;
                for (int i = 0; i < listAudit.Capacity; i++)
                {
                    //get the name of the app for the int listAudit [i]. Want to keep listAudit small so keeping only int in place of string
                    penThickness = 1; // GetPenThickNess(i); Penthickness and alignment not working, lets jest draw each line at a time
                    if (prevApp != listAudit[i])
                    {
                        pen1 = color.GetPen(indexName[listAudit[i]], dictionary, penThickness);
                        prevApp = listAudit[i];
                    }
                    if (bottomLeft.X < rectangle.Width - 2)
                    {
                        bottomLeft.X++;
                        topLeft.X++;
                        g.DrawLine(pen1, bottomLeft, topLeft);
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
