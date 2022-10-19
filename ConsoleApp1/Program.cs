using System;
using System.Collections.Generic;
using System.IO;

namespace practic4
{
    class Program
    {
        static List<DateList> Catalogs = new List<DateList>();
        static int CatallogId = 0;
        static int PageId = 0;
        static int PageId2 = 0;
        static bool CatalogOpen = false;
        static int Price = 0;
        static List<string> ListCase = new List<string>();

        static void Main(string[] args)
        {
            Load();
            Open();
        }
        static void Load()
        {
            Catalogs.Add(new DateList(
                new List<Button>() {
                    new Button(
                        "Форма торта","",new List<Button2>
                        {
                            new Button2("Круг",500),
                            new Button2("Квадрат",500),
                            new Button2("Прямогульник",500),
                            new Button2("Сердечко",500)
                        }
                    ),
                    new Button(
                        "Размер торта","",new List<Button2>
                        {
                            new Button2("Маленький (Диаметр - 16 см, 8 порций)",1000),
                            new Button2("Маленький (Диаметр - 18 см, 10 порций)",1200),
                            new Button2("Маленький (Диаметр - 28 см, 24 порций)",2000)
                        }
                    ),
                    new Button(
                        "Вкус коржей","",new List<Button2>
                        {
                            new Button2("Ванильный",100),
                            new Button2("Шоколадный",150),
                            new Button2("Карамельный",160),
                            new Button2("Ягодный",200),
                            new Button2("Кокосовый",250)
                        }
                    ),
                    new Button(
                        "Количество коржей","",new List<Button2>
                        {
                            new Button2("1 корж",200),
                            new Button2("2 коржа",400),
                            new Button2("3 коржа",600),
                            new Button2("4 коржа",800),
                        }
                    ),
                    new Button(
                        "Глазурь","",new List<Button2>
                        {
                            new Button2("Шоколад",100),
                            new Button2("Крем",150),
                            new Button2("Бизе",200),
                            new Button2("Драже",250),
                            new Button2("Ягоды",300),
                        }
                    ),
                    new Button(
                        "Декор","",new List<Button2>
                        {
                            new Button2("Шоколадная",150),
                            new Button2("Ягодная",150),
                            new Button2("Кремовая",150),
                        }
                    ),
                    new Button( "Конец заказа","save",new List<Button2>() )
                }
            ));
        }
        static void Open()
        {
            Console.Clear();
            DateList list = Catalogs[CatallogId];
            if (!CatalogOpen)
            {
                Console.WriteLine($"Заказ тортов в ГЛАЗУРЬКА, торты на ваш выбор!");
                Console.WriteLine($"Выберите параметр торта");
                Console.WriteLine($"------------------------------");

                for (int i = 0; i < list.Pages.Count; i++) Console.WriteLine($"  {list.Pages[i].Name}");
                Console.WriteLine("");
                Console.WriteLine($"Цена: {Price}р");
                Console.WriteLine($"Заказ: \n {String.Join(",\n ", ListCase.ToArray())}");
                
                int next = setCursor(list.Pages.Count, PageId, 3);
                if(next != -1) {
                    PageId = next;
                    Open();
                }
            }
            else
            {
                Console.WriteLine($"Для выхода нажмите ESC");
                Console.WriteLine($"Выбирите пункт из меню:");
                Console.WriteLine($"------------------------------");

                for (int i = 0; i < list.Pages[PageId].Buttons.Count; i++) Console.WriteLine($"  {list.Pages[PageId].Buttons[i].Name} - {list.Pages[PageId].Buttons[i].Price}р"); 
                
                int next = setCursor(list.Pages[PageId].Buttons.Count, PageId2, 3);
                if (next != -1) {
                    PageId2 = next;
                    Open();
                }
            }
        }

        static int setCursor(int count, int type, int start)
        {
            Console.SetCursorPosition(0, type + start);
            Console.WriteLine("->");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.UpArrow)
            {
                if (type + start != start) type -= 1;
                else type = count - 1;
                Console.SetCursorPosition(0, type + start);
                Console.WriteLine("->");
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                if (type != count - 1) type += 1;
                else type = 0;
                Console.SetCursorPosition(0, type + start);
                Console.WriteLine("->");
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                if (Catalogs[CatallogId].Pages[PageId].Type == "save")
                {
                    saveButton();
                    return -1;
                }
                else if (!CatalogOpen) CatalogOpen = true;
                else
                {
                    ListCase.Add($"'{Catalogs[CatallogId].Pages[PageId].Name}': {Catalogs[CatallogId].Pages[PageId].Buttons[PageId2].Name}");
                    Price += Catalogs[CatallogId].Pages[PageId].Buttons[PageId2].Price;
                }
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                if (CatalogOpen)
                {
                    CatalogOpen = false;
                    type = 0;
                }
            }
            return type;
        }

        static void saveButton()
        {
            Console.Clear();
            string path = $"{AppDomain.CurrentDomain.BaseDirectory}save.txt";
            File.WriteAllText(path, $"Заказ от: {DateTime.Now}\nЦена: {Price}р\nЗаказ: \n {String.Join(",\n ", ListCase.ToArray())}");
            Console.WriteLine("Спасибо за заказ! Если хотите сделать ещё один, нажмите на клавишу Esc");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.Key == ConsoleKey.Escape)
            {
                ListCase = new List<string>();
                Price = 0;
                PageId = 0;
                Open();
            }
        }
    }
}
