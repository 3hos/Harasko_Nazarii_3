using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace LAB3._2
{
    public class Article
    {
        public string name { get; set; }
        public int price { get; set; }
        public bool available { get; set; }
        public string type { get; set; }//Winter//Summer//Food//Clothes//Fitnes equipment

        public static List<Article> Obj = new List<Article>();
        public static List<Article> Sorted = new List<Article>();

        public Article(string Name, int Price,string Type, bool Available)
        {
            name = Name;
            price = Price;
            type = Type;
            available = Available;
            Obj.Add(this);
        }
        public void Print()
        {
            string A = "Not Available";
            if (available) A = "Available";
            Console.WriteLine($"{name}, Price:{price} UAN, Department:{type}, {A}");
        }
        public static void Save()
        {
            string Js = JsonSerializer.Serialize(Article.Obj);
            File.WriteAllText("ART.json", Js);
        }
        public static void Open()
        {
            var Js = File.ReadAllText("ART.json");
            List<Article> FF = JsonSerializer.Deserialize<List<Article>>(Js);
            Article.Sorted = Article.Obj;
        }
        public static void Sort()
        {
            Sorted.Sort(delegate (Article x, Article y)
            {
                return x.price.CompareTo(y.price);
            });
        }
        public static void Type(string t)
        {
            Console.WriteLine($"Department: {t}");
            Sorted.RemoveAll(x=>x.type!=t);
        }
        public static void Price(int min,int max)
        {
            Console.WriteLine($"Price:{min}-{max}");
            Sorted.RemoveAll(x => x.price > max);
            Sorted.RemoveAll(x => x.price < min);
        }
        public static void Clear()
        {
            Sorted.Clear();
            Open();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string range_str;
            string[] range;
            Article.Open();
            Console.WriteLine("Commands:\nShow- List selected items\nLow - Price: Low to High\nHigh - Price: High to Low\nDepartment - Filter by department\nPrice - Filter by price\nClear- Clear filters\nAdd, Dell, Save");
            while (true)
            {
                string str = Console.ReadLine();
                switch (str)
                {
                    case "Low":
                        Article.Sort();
                        Console.WriteLine("Price: Low to High");
                        break;
                    case "Show":
                        Print();
                        break;
                    case "High":
                        Article.Sort();
                        Article.Sorted.Reverse();
                        Console.WriteLine("Price: High to Low");
                        break;
                    case "Department":
                        Console.WriteLine("Enter Department");
                        Article.Type(Console.ReadLine());
                        break;
                    case "Price":
                        Console.WriteLine("Enter price (min-max)");
                        range_str = Console.ReadLine();
                        range = range_str.Split('-');
                        Article.Price(Convert.ToInt32(range[0]),Convert.ToInt32(range[1]));
                        break;
                    case "Clear":
                        Console.WriteLine("Filters removed");
                        Article.Clear();
                        break;
                    case "Add":
                        Console.WriteLine("Name, Price, Type, Available");
                        range_str = Console.ReadLine();
                        range = range_str.Split(", ");
                        new Article(range[0],Convert.ToInt32(range[1]), range[2], Convert.ToBoolean(range[3]));
                        break;
                    case "Dell":
                        Console.WriteLine("Enter name");
                        Article.Obj.RemoveAll(x => x.name.Contains(Console.ReadLine()));
                        break;
                    case "Save":
                        Article.Save();
                        break;
                }    
            }
            void Print()
            {
                foreach (var o in Article.Sorted)
                {
                    o.Print();
                }
            }
        }
    }
}
