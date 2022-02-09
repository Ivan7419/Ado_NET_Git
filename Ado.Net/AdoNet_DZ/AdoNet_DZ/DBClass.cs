using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using static System.Console;

namespace AdoNet_DZ 

{
    public class DB_Operations
    {
        private SqlConnection connection { get; set; }
        private SqlCommand command { get; set; }

        public DB_Operations(string conString)
        {
            connection = new SqlConnection(conString);
        }

        public List<string> Command_For_Reading(string sql, bool IsConsole = true)
        {
            try
            {
                SqlDataReader reader;
                List<string> res = new List<string>();
                connection.Open();

                SqlCommand command = new SqlCommand(sql, connection);
                reader = command.ExecuteReader();
                bool firstLine = true;
                while (reader.Read())
                {
                    if (firstLine)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            if (IsConsole) res.Add(reader.GetName(i) + "\t");
                            else res.Add(reader.GetName(i));
                        }

                        if(IsConsole)res.Add("\n");
                        firstLine = false;
                    }
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        if (IsConsole) res.Add(reader[i] + "\t");
                        else res.Add(reader[i].ToString());
                    }
                    if (IsConsole)res.Add("\n");
                }
                reader.Close();
                connection.Close();
                return res;
            }
            catch (Exception e)
            {
                WriteLine(e);
                throw;
            }
        }

        public int Command_For_DATA_Change(string sql)
        {
            try
            {
                connection.Open();
                command = new SqlCommand(sql, connection);
                int n = command.ExecuteNonQuery();
                connection.Close();
                return n;
            }
            catch (Exception e)
            {
                WriteLine(e);
                throw;
            }
        }
    }

    public class FruitsVeg
    {
        private DB_Operations db =
            new DB_Operations(
                "Data Source=10.6.1.191;Initial Catalog=Onischenko;Persist Security Info=True;User ID=pv011;Password=147852");

        public FruitsVeg(bool withSeed = false)
        {
            if (withSeed) Seed();
        }

        private void Seed()
        {
            Console.WriteLine($"затронуто {db.Command_For_DATA_Change("create table Info (Id int not null identity primary key, Name nvarchar(20) not null, Type nvarchar(20) not null, Color nvarchar(20) not null, Calories int not null)")} строк");
            Console.WriteLine($"затронуто {db.Command_For_DATA_Change("insert into Info([Name], [Type], Color, Calories) values ('Яблоко','Фрукт','Красный','52'),('Груша','Фрукт','Зелёный','57'),('Апельсин','Фрукт','Оранжевый','47'),('Огурец','Овощь','Зелёный','16 '),('Морковка','Овощь','Оранжевый','35 ')")} строк");
        }

        private Action Task;

        public void Menu()
        {
            WriteLine("---------------------");
            WriteLine("FRUITS AND VEGETABLES");
            WriteLine("---------------------");
            WriteLine("1.Task 1\n2.Task 2\n3.Task 3\n4.Task 4\n5.Task 5\n6.Task 6\n7.Task 7\n8.Task 8\n9.Task 9\n(Num Lock)1.Task 10\n(Num Lock)2.Task 11\n(Num Lock)3.Task 12\n(Num Lock)4.Task 13\n(Num Lock)5.Task 14\nEscape.Exit\n");
            ConsoleKey ch = ReadKey().Key;
            List<string> table = new List<string>();
            Clear();
            switch (ch)
            {
                case ConsoleKey.D1:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select * from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D2:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select [Name] from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D3:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select distinct Color from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D4:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Max(Calories) as 'Max Calories' from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D5:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Min(Calories) as 'Min Calories' from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D6:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Avg(Calories) as 'Avg Calories' from Info");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D7:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Count(Id) as 'Number of vegetables' from Info where [Type] = 'Овощь'");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D8:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Count(Id)as 'Number of Fruits' from Info where [Type] = 'Фрукт'");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.D9:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Color, Count(Name) as 'Count' from Info where Color = 'Красный' group by Color");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.NumPad1:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select distinct Color, Count(Name) as 'Count' from Info group by Color");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.NumPad2:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Name from Info where Calories < 15");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.NumPad3:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Name from Info where Calories > 15");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.NumPad4:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Name from Info where Calories between 10 and 20");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                case ConsoleKey.NumPad5:
                    Task = () =>
                    {
                        table = db.Command_For_Reading("select Name from Info where Color = 'Красный' or Color = 'Оранжевый'");
                        foreach (var t in table)
                        {
                            Write(t);
                        }
                        ReadKey();
                        Clear();
                    };
                    break;
                default:
                    Menu();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            Task();
            Menu();
        }

        public void MyCommand()
        {
            string com = ReadLine();
            int n = db.Command_For_DATA_Change(com);
            if(n>=0) Console.WriteLine($"затронуто {n} строк");
            else
            {
                List<string> table = db.Command_For_Reading(com);
                foreach (var t in table)
                {
                    Write(t);
                }
                ReadKey();
            }
        }
    }
}