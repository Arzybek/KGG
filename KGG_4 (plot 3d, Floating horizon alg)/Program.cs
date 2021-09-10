using System;
using System.Drawing;

namespace FloatingHorizonAlgorithm
{
    internal class Program
    {
        static Func<double, double, double> func = (x, y) => Math.Cos(x * y); // Функция 
        static int width = 800; // ширина изображения
        static int height = 600; // высота изображения
        static double x1 = -3, x2 = 3, y1 = -3, y2 = 3; // диапазоны x и y

        static double coord_x(double x, double y, double z)
        {
            return (y - x) * Math.Sqrt(3.0) / 2;
        }

        static double coord_y(double x, double y, double z)
        {
            return (x + y) / 2 - z;
        }

        public static void Main(string[] args)
        {
            Bitmap bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.White);
            
            double x, y, z, xx, yy, maxx, maxy, minx, miny;
            int n = 50, m = width * 2;
            int[] top = new int[width+1];
            int[] bottom = new int[width+1];
            minx = 10000;
            maxx = -minx;
            miny = minx;
            maxy = maxx;
            for (int i = 0; i <= n; ++i)
            {
                x = x2 + i * (x1 - x2) / n;
                for (int j = 0; j <= n; ++j)
                {
                    y = y2 + j * (y1 - y2) / n;
                    z = func(x, y);
                    xx = coord_x(x, y, z);
                    yy = coord_y(x, y, z);
                    if (xx > maxx) maxx = xx;
                    if (yy > maxy) maxy = yy;
                    if (xx < minx) minx = xx;
                    if (yy < miny) miny = yy;
                }
            }

            for (int i = 0; i < width; ++i)
            {
                top[i] = height;
                bottom[i] = 0;
            }

            for (int i = 0; i <= n; ++i)
            {
                x = x2 + i * (x1 - x2) / n;
                for (int j = 0; j <= m; ++j)
                {
                    y = y2 + j * (y1 - y2) / m;
                    z = func(x, y);
                    xx = coord_x(x, y, z);
                    yy = coord_y(x, y, z);
                    xx = (xx - minx) / (maxx - minx) * width;
                    yy = (yy - miny) / (maxy - miny) * height;
                    if (yy > bottom[(int)xx])
                    {
                        graphics.FillRectangle((Brush)Brushes.Black, (int)xx, (int)yy, 1, 1);
                        bottom[(int) xx] = (int)yy;
                    }

                    if (yy < top[(int) xx])
                    {
                        graphics.FillRectangle((Brush)Brushes.Black, (int)xx, (int)yy, 2, 2);
                        top[(int) xx] = (int)yy;
                    }
                }
            }
            
            bitmap.Save("myimage.png");

            Console.WriteLine("Файл myimage.png был сохранен");
        }
    }
}