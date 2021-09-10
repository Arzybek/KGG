using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ConsoleApplication1
{
    static class Program
    {
        static void SaveImage(Bitmap bitmap, string filename)
        {
            bitmap.Save(filename);
        }

        static void DrawLines(Graphics graphics, List<Point> points)
        {
            for (int i = 0; i < points.Count - 1; i++)
                graphics.DrawLine(new Pen(Brushes.Red), points[i], points[i + 1]);
        }

        static Func<double, double> func = (x) =>
        x*Math.Sin(x*3);
        //Math.Cos(x);
        //(Math.Sin(x))+1;
        //Math.Tan(x);

        private static double A =
            -3;
        //-2 * Math.PI;
        //-Math.PI / 2;

        private static double B =
            10;
        //2 * Math.PI;
        //Math.PI / 2;

        private static int MaxXAxis = 1000;
        private static int MaxYAxis = 1000;

        static void Main(string[] args)
        {

            double stride = (B - A) / MaxXAxis;
            double current = A;

            var values = new List<(int xx, double y)>();
            for (int x = 0; x < MaxXAxis; x++)
            {
                current = A + x * (B - A) / MaxXAxis;
                values.Add((x, func(current)));
            }

            double minValue = values.Min(val => val.y);
            double maxValue = values.Max(val => val.y);

            double k = MaxYAxis / (maxValue - minValue);
            var points = values.Select(pp =>
            {
                return new Point(pp.xx, (int)((pp.y - maxValue) * MaxYAxis / (minValue - maxValue)));
            }).ToList();

            Bitmap bitmap = new Bitmap(MaxXAxis, MaxYAxis);
            var graphics = Graphics.FromImage(bitmap);

            double centerX;
            if (A <= 0 && B > 0 || A < 0 && B >= 0)
                centerX = Math.Abs(A) * MaxXAxis / (B - A);
            else
                centerX = -1;

            graphics.Clear(Color.White);

            double centerY;
            if (minValue <= 0 && maxValue > 0 || minValue < 0 && maxValue >= 0)
                centerY = -Math.Abs(maxValue) * MaxYAxis / (minValue - maxValue);
            else
                centerY = -1;

            graphics.Clear(Color.White);

            //double cur = 0;
            //var centerXLocal = centerX == -1 ? MaxXAxis/2 : centerX;
            //if (centerX == -1)
            //{
            //    for (var i = 0; i < B; i++)
            //    {
            //        cur = i * MaxXAxis / (B - A);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point((int)cur, 0), new Point((int)cur, MaxYAxis));
            //    }
            //}
            //else
            //{
            //    for (var i = (int)(A + B) / 2; i > A; i--)
            //    {
            //        cur = centerXLocal + i * MaxXAxis / (B - A);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point((int)cur, 0), new Point((int)cur, MaxYAxis));
            //    }
            //    cur = 0;
            //    for (var i = (int)(A + B) / 2; i < Math.Abs(B); i++)
            //    {
            //        cur = centerXLocal + i * MaxXAxis / (B - A);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point((int)cur, 0), new Point((int)cur, MaxYAxis));
            //    }
            //}

            //cur = 0;
            //var centerYLoc = centerY == -1 ? MaxYAxis / 2 : centerY;
            //if (centerY == -1)
            //{
            //    for (var i = 0; i < Math.Max(Math.Abs(maxValue),Math.Abs(minValue)) - Math.Min(Math.Abs(maxValue), Math.Abs(minValue)); i++)
            //    {
            //        cur = i * MaxYAxis / (maxValue-minValue);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point(0, (int)cur), new Point(MaxXAxis, (int)cur));
            //    }
            //}
            //else
            //{
            //    for (var i = (int)(minValue + maxValue) / 2; i > minValue; i--)
            //    {
            //        cur = centerYLoc + i * MaxYAxis / (minValue - maxValue);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point(0, (int)cur), new Point(MaxXAxis, (int)cur));
            //    }
            //    cur = 0;
            //    for (var i = (int)(minValue + maxValue) / 2; i < Math.Abs(maxValue); i++)
            //    {
            //        cur = centerYLoc + i * MaxYAxis / (minValue - maxValue);
            //        graphics.DrawLine(new Pen(Brushes.Gray), new Point(0, (int)cur), new Point(MaxXAxis, (int)cur));
            //    }
            //}

            DrawLines(graphics, points);

            graphics.DrawLine(new Pen(Brushes.Blue), new Point((int)centerX, 0), new Point((int)centerX, MaxYAxis));
            graphics.DrawLine(new Pen(Brushes.Blue), new Point(0, (int)centerY), new Point(MaxXAxis, (int)centerY));

            SaveImage(bitmap, "myimage.png");
        }
    }
}