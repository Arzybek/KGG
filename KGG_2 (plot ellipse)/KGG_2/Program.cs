using System;
using System.Drawing;

namespace KGG_2
{
    internal class Program
    {
        public static int x0, y0, a, b, width, height;
        public static SolidBrush brush = new SolidBrush(Color.Black);
        
        public static void MakePrompt()
        {
            Console.WriteLine("Введите x0 и y0 через пробел: ");
            var newCenterEntredCoords = Console.ReadLine().Split(' ');
            x0 = int.Parse(newCenterEntredCoords[0]);
            y0 = int.Parse(newCenterEntredCoords[1]);
            Console.WriteLine("Введите a и b через пробел: ");
            var canonParameters = Console.ReadLine().Split(' ');
            a = int.Parse(canonParameters[0]);
            b = int.Parse(canonParameters[1]);
            Console.WriteLine("Введите ширину и высоту картинки через пробел: ");
            var widthAndHeight = Console.ReadLine().Split(' ');
            width = int.Parse(widthAndHeight[0]);
            height = int.Parse(widthAndHeight[1]);
        }
        
        public static void InitConsts()
        {
            x0 = 10;
            y0 = 200;
            a = 10;
            b = 200;
            width = 750;
            height = 550;
        }
        
        public static void SetPixel(int x, int y, Graphics g) 
        {
            g.FillRectangle(brush, x, y, 1, 1);
        }

        public static void Pixel4(int x1, int y1, int x2, int y2, Graphics g) 
        {
            SetPixel(x1 + x2, y1 + y2, g);
            SetPixel(x1 + x2, y1 - y2, g);
            SetPixel(x1 - x2, y1 - y2, g);
            SetPixel(x1 - x2, y1 + y2, g);
        }
        
        public static void DrawAxes(Graphics graphics)
        {
            graphics.Clear(Color.White);
            graphics.DrawLine(new Pen(Brushes.Black), new Point(width/2, 0), new Point(width/2, height));
            graphics.DrawLine(new Pen(Brushes.Black), new Point(0, height/2), new Point(width, height/2));
        }

        public static void SaveFile(Bitmap bitmap)
        {
            bitmap.Save("output.png");
            Console.WriteLine("Ваша картинка была сохранена в output.png");
        }
        
        public static void DrawEllipse(int x, int y, Graphics g)
        {
            int dx = 0; //Компонента x
            int dy = b; //Компонента y
            int a_sqr = a * a; // a^2 
            int b_sqr = b * b; // b^2 
            int delta = 4 * b_sqr * ((dx + 1) * (dx + 1)) + a_sqr * ((2 * dy - 1) * (2 * dy - 1)) - 4 * a_sqr * b_sqr;
                        //Функция координат точки (x+1, y-1/2)

            while(a_sqr * (2 * dy - 1) > 2 * b_sqr * (dx + 1)) //Первая часть дуги
            {
                Pixel4(x, y, dx, dy, g);
                if (delta < 0)// Переход по горизонтали
                {
                    dx++;
                    delta += 4 * b_sqr * (2 * dx + 3);
                }
                else //Переход по диагонали
                {
                    dx++;
                    delta += 4 * b_sqr * (2 * dx + 3) - 8 * a_sqr * (dy - 1);
                    dy--;
                }
            }
            
            delta = b_sqr * ((2 * dx + 1) * (2 * dx + 1)) + 4 * a_sqr * ((dy + 1) * (dy + 1)) - 4 * a_sqr * b_sqr;
                    //Функция координат точки (x+1/2, y-1)
            while(dy + 1 != 0) //Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y-1) <= 2b^2(x+1)
            {
                Pixel4(x,y,dx,dy, g);
                if (delta < 0)//Переход по вертикали
                {
                    dy--;
                    delta += 4 * a_sqr * (2 * dy + 3);
                }
                else //Переход по диагонали
                {
                    dy--;
                    delta = delta - 8 * b_sqr * (dx + 1) + 4 * a_sqr * (2 * dy + 3);
                    dx++;
                }
            }
        }
        
        public static void Main(string[] args)
        {
//            MakePrompt();
            InitConsts();
            Bitmap bitmap = new Bitmap(width, height);
            var graphics = Graphics.FromImage(bitmap);
            DrawAxes(graphics);
            DrawEllipse(width/2+x0, height/2-y0, graphics);
            SaveFile(bitmap);
        }
    }
}