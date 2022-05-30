using System;

namespace LAB3
{
    class Program
    {
        public class Point
        {
            public int x;
            public int y;
        }
        class Sg
        {
            public Point a;//Початок
            public Point b;//Кінець

            public double Length()
            {
                return Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2));
            }
            public void Length(int ln)
            {
                double det = ln / (Math.Sqrt(Math.Pow(a.x - b.x, 2) + Math.Pow(a.y - b.y, 2)));
                b.x = (int)ln * (b.x - a.x) + a.x;
                b.y = (int)ln * (b.y - a.y) + a.y;
                Console.WriteLine($"Нова довжина відрізка{ln}");
            }
            public static Sg operator *(Sg sg, double ln)
            {
                Sg ss = sg;
                ss.b.x = (int)ln * (sg.b.x - sg.a.x) + sg.a.x;
                ss.b.y = (int)ln * (sg.b.y - sg.a.y) + sg.a.y;
                return ss;
            }
            public static Sg operator /(Sg sg, int ln)
            {
                Sg ss = sg;
                ss.b.x = (sg.b.x - sg.a.x)/ln + sg.a.x;
                ss.b.y = (sg.b.y - sg.a.y)/ln + sg.a.y;
                return ss;
            }
            public static bool operator ==(Sg sg1,Sg sg2 )
            {
                return sg1.Length() == sg2.Length();
            }
            public static bool operator !=(Sg sg1, Sg sg2)
            {
                return sg1.Length() == sg2.Length();
            }
            public static explicit operator double(Sg sg)
            {
                return sg.Length();
            }
        }
        class Square
        {
            public Sg s1;
            public Sg s2;
            public ConsoleColor color;

            public int Length()
            {
                return (int)s1.Length();
            }

            public static Square operator *(Square sq, double ln)
            {
                Square ss = sq;
                ss.s1 = sq.s1 * ln;
                ss.s2 = sq.s2 * ln;
                Console.WriteLine($"Збільшено розмір в {ln} разів");
                return ss;
            }
            public static Square operator /(Square sq, double ln)
            {
                Square ss = sq;
                ss.s1 = sq.s1 * ln;
                ss.s2 = sq.s2 * ln;
                Console.WriteLine($"Збільшено розмір в {ln} разів");
                return ss;
            }
            public override bool Equals(object obj)
            {
                if (obj == null)
                    return false;
                Square sq = obj as Square;
                return this.Length() == sq.Length();
            }
            public override int GetHashCode()
            {
                return this.Length() % 11;
            }
            public override string ToString()
            {
                return "color:{color}\nКоорд прямої 1: 1({s1.a.x},{s1.a.y})2({s1.b.x},{s1.b.y})\nКоорд прямої 2: 1({s2.a.x},{s2.a.y})2({s2.b.x},{s2.b.y})";
            }
            public void Rotate()
            {
                int l = (int)s1.Length();
                if (s1.a.y<s1.b.y&&s2.b.x>s2.a.x)
                {
                    s1.b.x += l;
                    s1.b.y -= l;
                    s2.b.x -= l;
                    s2.b.y -= l;
                }
                else if (s1.a.y > s1.b.y && s2.b.x > s2.a.x)
                {
                    s1.b.x -= l;
                    s1.b.y -= l;
                    s2.b.x -= l;
                    s2.b.y += l;
                }
                else if (s1.a.y > s1.b.y && s2.b.x < s2.a.x)
                {
                    s1.b.x -= l;
                    s1.b.y += l;
                    s2.b.x -= l;
                    s2.b.y += l;
                }
                else
                {
                    s1.b.x -= l;
                    s1.b.y += l;
                    s2.b.x += l;
                    s2.b.y -= l;
                }
                Console.WriteLine($"Квадрат повернуто на 90 ");
            }
            public void Colour(ConsoleColor cl)
            {
                color = cl;
                Console.WriteLine($"Колір змінено на {cl}");
            }
            public void Print()
            {
                Console.BackgroundColor = color;
                Console.WriteLine(this.ToString());
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
        static void Main(string[] args)
        {
            Point H = new Point();
            H.x = 1;
            H.y = 1;
            Point H2 = new Point();
            H2.x = 1;
            H2.y = 2;
            Point H3 = new Point();
            H3.x = 2;
            H3.y = 1;
            Sg s1 = new Sg();
            s1.a = H;
            s1.b = H2;
            Sg s2 = new Sg();
            s2.a = H;
            s2.b = H3;
            Console.WriteLine($"{H2.x},{H2.y}, Довжина відрізка збільшується в 2 рази:");
            s1 = s1 * 2;
            Console.WriteLine($"{H2.x},{H2.y}");
            s1.b.y = 2;
            Square Sq = new Square();
            Sq.s1 = s1;
            Sq.s2 = s2;
            Sq.color = ConsoleColor.DarkGray;
            Sq.Print();
            Sq.color = ConsoleColor.Black;
            Sq = Sq * 2;
            Sq.Print();
            Sq.Rotate();
            Sq.Print();
        }
    }
}
