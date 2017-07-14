using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Focus
{ 
    class Colorization
    {
        private List<Color> color;
        public Colorization()
        {
            color = new List<Color>();
            InitColor();
        }
        private void InitColor()
        {
            //System.Array colorsArray = Enum.GetValues(typeof(KnownColor));
            //allColors = new KnownColor[colorsArray.Length];
            //Array.Copy(colorsArray, allColors, colorsArray.Length);
            color.Add(Color.Black);
            color.Add(Color.Magenta);
            color.Add(Color.Blue);
            color.Add(Color.Red);
            color.Add(Color.DarkCyan);
            color.Add(Color.Orange);
            color.Add(Color.CadetBlue);
            color.Add(Color.Chocolate);
            color.Add(Color.Coral);
            color.Add(Color.CornflowerBlue);
            color.Add(Color.Green);
            color.Add(Color.Cyan);
            color.Add(Color.DarkBlue);
            color.Add(Color.DarkGoldenrod);
            color.Add(Color.DarkGreen);
            color.Add(Color.Crimson);
            color.Add(Color.DarkOrange);
            color.Add(Color.DarkSlateBlue);
            color.Add(Color.DarkTurquoise);
            color.Add(Color.DeepPink);
            color.Add(Color.DeepSkyBlue);
            color.Add(Color.DodgerBlue);
            color.Add(Color.Gold);
            color.Add(Color.Brown);
            color.Add(Color.BlueViolet);
            color.Add(Color.Indigo);
            color.Add(Color.LightSlateGray);
            color.Add(Color.LimeGreen);
            color.Add(Color.MidnightBlue);
            color.Add(Color.Olive);
            color.Add(Color.DarkRed);
            color.Add(Color.OliveDrab);

            color.Add(Color.SteelBlue);
          color.Add(Color.BurlyWood);

        }

        public Color GetColor (int i)
        {
            if (i > color.Count - 1)
                i = 0;
            return color[i];
        }

        public Pen GetPen(string appName, Dictionary<string, long> dictionary, int penThickness)
        {
            int i = 0;
            foreach (var v in dictionary)
            {
                if (v.Key == appName)
                {
                    break;
                }
                i++; //Just so i get different color shades
            }
            if (i > color.Count -1)
                i = 0;
            Pen p = new System.Drawing.Pen(color[i], penThickness);
         //   p.Alignment = System.Drawing.Drawing2D.PenAlignment.Right;
            return p;
        }
    }

   

}
