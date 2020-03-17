using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PhotoThemes
{
    public partial class Form1 : Form
    {
        public static Bitmap original;
        public static Bitmap edit;

        public Form1()
        {
            InitializeComponent();
        }

        //Functions

        private static double Luminance(Color pixel)
        {
            return 0.2126 * pixel.R + 0.7152 * pixel.G + 0.0722 * pixel.B;
        }

        private static void blackAndWhite()
        {
            for(int column = 0; column < original.Width; column++)
            {
                for(int row = 0; row < original.Height; row++)
                {
                    int luminance = (int)Luminance(original.GetPixel(column, row));
                    edit.SetPixel(column, row, Color.FromArgb(luminance, luminance, luminance));
                }
            }
        }

        private static void blackAndWhiteOld()
        {
            Random r = new Random();
            int no = r.Next(5, 10);
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    if(row % no == 0)
                    {
                        edit.SetPixel(column, row, Color.Black);
                    }
                    else
                    {
                        int luminance = (int)Luminance(original.GetPixel(column, row));
                        edit.SetPixel(column, row, Color.FromArgb(luminance, luminance, luminance));
                    }
                    
                }
            }
        }

        private static void reverseBlackAndWhite()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int luminance = 255 - (int)Luminance(original.GetPixel(column, row));
                    edit.SetPixel(column, row, Color.FromArgb(luminance, luminance, luminance));
                }
            }
        }

        private static void medium()
        {
            double x = 0, y = 0, z = 0;
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    x += pixel.R;
                    y += pixel.G;
                    z += pixel.B;
                }
            }

            x /= (original.Width * original.Height);
            y /= (original.Width * original.Height);
            z /= (original.Width * original.Height);

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int r, g, b;
                    Color pixel = original.GetPixel(column, row);
                    r = (int)Math.Abs(x - pixel.R);
                    g = (int)Math.Abs(y - pixel.G);
                    b = (int)Math.Abs(z - pixel.B);
                    edit.SetPixel(column, row, Color.FromArgb(r, g, b));
                }
            }

        }

        private static int LinearStretching(double delta, double min, int x)
        {
            return (int)((x - min) * delta);
        }

        private static void contrast()
        {
            double minR = 255, maxR = 0;
            double minG = 255, maxG = 0;
            double minB = 255, maxB = 0;

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    if (minR > pixel.R)
                        minR = pixel.R;
                    if (maxR < pixel.R)
                        maxR = pixel.R;

                    if (minG > pixel.G)
                        minG = pixel.G;
                    if (maxG < pixel.G)
                        maxG = pixel.G;

                    if (minB > pixel.B)
                        minB = pixel.B;
                    if (maxB < pixel.B)
                        maxB = pixel.B;
                }
            }

            if (minR - maxR == 0)
                maxR++;
            if (minG - maxG == 0)
                maxG++;
            if (minB - maxB == 0)
                maxB++;

            double deltaR = 255.0 / (maxR - minR);
            double deltaG = 255.0 / (maxG - minG);
            double deltaB = 255.0 / (maxB - minB);

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int r, g, b;
                    Color pixel = original.GetPixel(column, row);

                    r = LinearStretching(deltaR, minR, pixel.R);
                    g = LinearStretching(deltaG, minG, pixel.G);
                    b = LinearStretching(deltaB, minB, pixel.B);

                    edit.SetPixel(column, row, Color.FromArgb(r, g, b));
                }
            }
        }

        private static void reverseColor()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, 255 - pixel.R, 255 - pixel.G, 255 - pixel.B));
                }
            }
        }

        private static void Transition_RGB_RBG()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, pixel.R, pixel.B, pixel.G));
                }
            }
        }

        private static void Transition_RGB_BRG()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    
                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, pixel.B, pixel.R, pixel.G));
                }
            }
        }

        private static void Transition_RGB_BGR()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, pixel.B, pixel.G, pixel.R));
                }
            }
        }

        private static void Transition_RGB_GRB()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, pixel.G, pixel.R, pixel.B));
                }
            }
        }

        private static void Transition_RGB_GBR()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, pixel.G, pixel.B, pixel.R));
                }
            }
        }

        private static void MaxColourValue()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    int max = pixel.R;
                    if (pixel.B > max)
                        max = pixel.B;
                    if (pixel.G > max)
                        max = pixel.G;
                    edit.SetPixel(column, row, Color.FromArgb(pixel.A, max, max, max));
                }
            }
        }

        private static void PureRGB()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    int max = pixel.R;
                    int color = 0;
                    if (pixel.B > max)
                    {
                        max = pixel.B;
                        color = 2;
                    }
                    if (pixel.G > max)
                    {
                        max = pixel.G;
                        color = 1;
                    }
                    if(color == 0)
                        edit.SetPixel(column, row, Color.FromArgb(pixel.A, max, 0, 0));
                    else if(color == 1)
                        edit.SetPixel(column, row, Color.FromArgb(pixel.A, 0, max, 0));
                    else
                        edit.SetPixel(column, row, Color.FromArgb(pixel.A, 0, 0, max));
                }
            }
        }

        private static double ColorDistance(Color a, Color b)
        {
            double cR = a.R - b.R;
            double cG = a.G - b.G;
            double cB = a.B - b.B;
            double uR = a.R + b.R;

            return cR * cR * (2 + uR / 256) + cG * cG * 4 + cB * cB * (2 + (255 - uR) / 256);
        }

        private static void PureColor()
        {
            List<Color> colors = new List<Color>();
            for (int red = 0; red <= 255; red += 66)
                for (int green = 0; green <= 255; green += 66)
                    for (int blue = 0; blue <= 255; blue += 66)
                        colors.Add(Color.FromArgb(red, green, blue));

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    int minIndex = 0;
                    double distance = ColorDistance(pixel, colors[0]);
                    for(int index = 1; index < colors.Count; index++)
                    {
                        double newDistance = ColorDistance(pixel, colors[index]);
                        if(newDistance < distance)
                        {
                            distance = newDistance;
                            minIndex = index;
                        }
                    }
                        edit.SetPixel(column, row, colors[minIndex]);
                }
            }
        }

        private static void MirrorX()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(original.Width - 1 - column, row, pixel);
                }
            }
        }

        private static void MirrorY()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);

                    edit.SetPixel(column, original.Height - 1 - row, pixel);
                }
            }
        }

        private static void Brighten()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    Color newPixel = Color.FromArgb((pixel.R + 255) / 2, (pixel.G + 255) / 2, (pixel.B + 255) / 2);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void Darken()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    Color newPixel = Color.FromArgb((pixel.R) / 2, (pixel.G) / 2, (pixel.B) / 2);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private struct pair
        {
            public int key;
            public double value;

            public pair(int key, double value)
            {
                this.key = key;
                this.value = value;
            }
        }

        private static void Sort(List<pair> pairs)
        {
            pairs.OrderBy(x => x.value);
        }

        private static void SortColors(List<Color> colors)
        {
            List<Color> newColors = new List<Color>();
            Color startingColor = Color.FromArgb(255, 255, 255);
            List<pair> pairs = new List<pair>();

            for (int index = 0; index < colors.Count; index++)
            {
                double distance = ColorDistance(startingColor, colors[index]);
                pairs.Add(new pair(index, distance));
            }

            Sort(pairs);

            foreach (pair p in pairs)
                newColors.Add(colors[p.key]);

            colors = newColors;
        }

        private static void Strip()
        {
            List<Color> colors = new List<Color>();
             
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    colors.Add(pixel);
                }
            }

            SortColors(colors);

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    edit.SetPixel(column, row, colors[original.Width * row + column]);
                }
            }
        }

        private static void ColorStrip()
        {
            List<Color> colorsRed = new List<Color>();
            List<Color> colorsGreen = new List<Color>();
            List<Color> colorsBlue = new List<Color>();

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    int max = pixel.R;
                    int color = 0;
                    if (pixel.B > max)
                    {
                        max = pixel.B;
                        color = 2;
                    }
                    if (pixel.G > max)
                    {
                        max = pixel.G;
                        color = 1;
                    }
                    if (color == 0)
                        colorsRed.Add(pixel);
                    else if (color == 1)
                        colorsGreen.Add(pixel);
                    else
                        colorsBlue.Add(pixel);
                }
            }

            SortColors(colorsRed);
            SortColors(colorsGreen);
            SortColors(colorsBlue);

            colorsGreen.AddRange(colorsBlue);
            colorsRed.AddRange(colorsGreen);

            List<Color> colors = colorsRed;

            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    edit.SetPixel(column, row, colors[original.Width * row + column]);
                }
            }
        }

        private static void FilterR()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    Color newPixel = Color.FromArgb(pixel.R, 0, 0);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void FilterG()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    Color newPixel = Color.FromArgb(0, pixel.G, 0);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void FilterB()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    Color pixel = original.GetPixel(column, row);
                    Color newPixel = Color.FromArgb(0, 0, pixel.B);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void convertRGBToHSL(int rval, int gval, int bval,
                                    ref int hval, ref int sval, ref int lval)
        {
            double r, g, b, h, s, l; 
            r = rval / 255.0;
            g = gval / 255.0;
            b = bval / 255.0;

            double maxColor = Math.Max(r, Math.Max(g, b));
            double minColor = Math.Min(r, Math.Min(g, b));
            
            if ((r == g) && (g == b))
            {
                h = 0.0; 
                s = 0.0;
                l = r; 
            }       
            else
            {
                double d = maxColor - minColor;
                l = (minColor + maxColor) / 2;

                if (l < 0.5)
                    s = d / (maxColor + minColor);
                else
                    s = d / (2.0 - maxColor - minColor);

                if (r == maxColor)
                    h = (g - b) / (maxColor - minColor);
                else if (g == maxColor)
                    h = 2.0 + (b - r) / (maxColor - minColor);
                else
                    h = 4.0 + (r - g) / (maxColor - minColor);

                h /= 6; 
                if (h < 0)
                    h++;
            }

            hval = (int)(h * 360.0);
            sval = (int)(s * 255.0);
            lval = (int)(l * 255.0);
        }

        private static void convertHSLToRGB(int hval, int sval, int lval,
                             ref int rval, ref int gval, ref int bval)
        {
            double r, g, b, h, s, l; 
            double temp1, temp2, tempr, tempg, tempb;

            h = hval / 360.0;
            s = sval / 256.0;
            l = lval / 256.0;
            
            if (s == 0)
            {
                r = l;
                g = l;
                b = l;
            }
            else
            {
                if (l < 0.5)
                    temp2 = l * (1 + s);
                else
                    temp2 = (l + s) - (l * s);
                temp1 = 2 * l - temp2;

                tempr = h + 1.0 / 3.0;
                if (tempr > 1)
                    tempr--;
                tempg = h;
                tempb = h - 1.0 / 3.0;
                if (tempb < 0)
                    tempb++;

                //Red
                if (tempr < 1.0 / 6.0)
                    r = temp1 + (temp2 - temp1) * 6.0 * tempr;
                else if (tempr < 0.5)
                    r = temp2;
                else if (tempr < 2.0 / 3.0)
                    r = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempr) * 6.0;
                else
                    r = temp1;

                //Green
                if (tempg < 1.0 / 6.0)
                    g = temp1 + (temp2 - temp1) * 6.0 * tempg;
                else if (tempg < 0.5)
                    g = temp2;
                else if (tempg < 2.0 / 3.0)
                    g = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempg) * 6.0;
                else
                    g = temp1;

                //Blue
                if (tempb < 1.0 / 6.0)
                    b = temp1 + (temp2 - temp1) * 6.0 * tempb;
                else if (tempb < 0.5)
                    b = temp2;
                else if (tempb < 2.0 / 3.0)
                    b = temp1 + (temp2 - temp1) * ((2.0 / 3.0) - tempb) * 6.0;
                else
                    b = temp1;
            }

            rval = (int)(r * 255.0);
            gval = (int)(g * 255.0);
            bval = (int)(b * 255.0);
        }

        private static void OverSaturation()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int hue = 0, saturation = 0, brightness = 0;
                    int newR = 0, newG = 0, newB = 0;
                    Color pixel = original.GetPixel(column, row);

                    convertRGBToHSL(pixel.R, pixel.G, pixel.B,
                                    ref hue, ref saturation, ref brightness);

                    double gray_factor = saturation / 255.0;
                    double var_interval = 255 - saturation;

                    saturation +=  (int)(var_interval * gray_factor);

                    convertHSLToRGB(hue, saturation, brightness,
                                    ref newR, ref newG, ref newB);

                    Color newPixel = Color.FromArgb(Math.Abs(newR) % 255, Math.Abs(newG) % 255, Math.Abs(newB) % 255 );
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void UnderSaturation()
        {
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int hue = 0, saturation = 0, brightness = 0;
                    int newR = 0, newG = 0, newB = 0;
                    Color pixel = original.GetPixel(column, row);

                    convertRGBToHSL(pixel.R, pixel.G, pixel.B,
                                    ref hue, ref saturation, ref brightness);

                    double gray_factor = saturation / 255.0;
                    double var_interval = 255 - saturation;

                    saturation -= (int)(var_interval * gray_factor);

                    convertHSLToRGB(hue, saturation, brightness,
                                    ref newR, ref newG, ref newB);

                    Color newPixel = Color.FromArgb(Math.Abs(newR) % 255, Math.Abs(newG) % 255, Math.Abs(newB) % 255);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void Blur()
        {
            int blurSize = 5;
            for (int column = 0; column < original.Width; column++)
            {
                for (int row = 0; row < original.Height; row++)
                {
                    int red = 0, green = 0, blue = 0;
                    for (int blurX = column; blurX < original.Width && blurX < column + blurSize; blurX++)
                    {                  
                        for (int blurY = row; blurY < original.Height && blurY < row + blurSize; blurY++)
                        {
                            Color pix = original.GetPixel(blurX, blurY);
                            red += pix.R;
                            green += pix.G;
                            blue += pix.B;
                        }
                    }
                    red /= (blurSize * blurSize);
                    green /= (blurSize * blurSize);
                    blue /= (blurSize * blurSize);

                    Color newPixel = Color.FromArgb(red, green, blue);
                    edit.SetPixel(column, row, newPixel);
                }
            }
        }

        private static void Slit()
        {
            float noOfCubes = 100;
            float valueW = original.Width / noOfCubes;
            float valueH = original.Height / noOfCubes;
            Color slit = Color.Gray;
            edit = (Bitmap)original.Clone();
            Graphics g = Graphics.FromImage(edit);
            Pen pen = new Pen(slit);
            for (float column = 0; column < original.Width; column += valueW)
            {
                g.DrawLine(pen, column, 0, column, original.Height);
            }

            for (float row = 0; row < original.Height; row += valueH)
            {
                g.DrawLine(pen, 0, row, original.Width, row);
            }
        }

        private static void Dot()
        {
            int value = 10;
            Color slit = Color.Gray;
            edit = (Bitmap)original.Clone();
            for (int column = 0; column < original.Width; column += value)
            {
                for (int row = 0; row < original.Height; row += value)
                {
                    edit.SetPixel(column, row, slit);
                }
            }
        }

        //Caller

        private void filterCall(Action function)
        {
            if (edit != null)
                edit.Dispose();

            if (panel2.BackgroundImage != null)
                panel2.BackgroundImage.Dispose();

            if(original != null)
            {
                edit = new Bitmap(original.Width, original.Height);

                function();

                panel2.BackgroundImage = new Bitmap(edit, new Size(panel2.Width, panel2.Height));
            }
            else
            {
                panel2.BackColor = Color.Black;
            }
        }

        //UI

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (original != null)
                original.Dispose();

            if (panel1.BackgroundImage != null)
                panel1.BackgroundImage.Dispose();

            try
            {
                original = new Bitmap(new Bitmap(openFileDialog1.FileName), new Size(panel1.Width, panel1.Height));
                panel1.BackgroundImage = original;
            }
            catch(Exception exp)
            {
                original = null;
                panel1.BackColor = Color.Black;
            }
            
        }

        private void originalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => blackAndWhite());
        }

        private void withStripsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => blackAndWhiteOld());
        }

        private void reverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => reverseBlackAndWhite()); 
        }

        private void originalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => medium()); 
        }

        private void contrastToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => contrast()); 
        }

        private void reverseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => reverseColor()); 
        }

        private void rGBToRBGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Transition_RGB_RBG());
        }

        private void rGBToBRGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Transition_RGB_BRG());
        }

        private void rGBToBGRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Transition_RGB_BGR());
        }

        private void rGBToGRBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Transition_RGB_GRB());
        }

        private void rGBToGBRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Transition_RGB_GBR());
        }

        private void maxColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => MaxColourValue());
        }

        private void maxColorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => PureRGB());
        }

        private void limitedColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => PureColor()); 
        }

        private void xToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => MirrorX());
        }

        private void yToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => MirrorY());
        }

        private void upToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Brighten());
        }

        private void downToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Darken());
        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => ColorStrip());
        }

        private void valuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Strip());
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => FilterR());
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => FilterG());
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => FilterB());
        }

        private void upToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => OverSaturation());
        }

        private void downToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            filterCall(() => UnderSaturation());
        }

        private void blurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Blur());
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                original = (Bitmap)edit.Clone();
                panel1.BackgroundImage = original;
            }
            catch(Exception ex)
            { }         
        }

        private void slitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Slit());
        }

        private void dotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filterCall(() => Dot());
        }
    }
}
