using System;
using System.Collections.Generic;
using static System.Console;

namespace AdoNet_DZ
{
    public class StationeryFirm
    {
        private AffiliatedMode_DB db =
           new AffiliatedMode_DB(
               "Data Source=I7-4700;Initial Catalog=StationeryFirm; Integrated Security=SSPI;");
        private Action Task;

        public void MainMenu()
        {
            WriteLine("---------------------");
            WriteLine("StationeryFirm");
            WriteLine("---------------------");
            WriteLine("1.Insert\n2.Update\n3.Delete\n4.Check\nEscape.Exit\n");
            ConsoleKey ch = ReadKey().Key;
            Clear();
            switch (ch)
            {
                case ConsoleKey.D1:
                    Task = () =>
                    {
                       
                    };
                    break;
                case ConsoleKey.D2:
                    Task = () =>
                    {
                        
                    };
                    break;
                case ConsoleKey.D3:
                    Task = () =>
                    {
                        
                    };
                    break;
                case ConsoleKey.D4:
                    Task = () =>
                    {
                        WriteLine("Command: ");
                        db.MyCommand(ReadLine());
                    };
                    break;
                default:
                    MainMenu();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            Task();
            Clear();
            MainMenu();
        }

        public void InsertMenu()
        {
            WriteLine("---------------------");
            WriteLine("Insert Menu");
            WriteLine("---------------------");
            WriteLine("1.Task 1\n2.Task 2\n3.Task 3\n4.Task 4\nEscape.Exit\n");
            ConsoleKey ch = ReadKey().Key;
            Clear();
            switch (ch)
            {
                case ConsoleKey.D1:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D2:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D3:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D4:
                    Task = () =>
                    {

                    };
                    break;
                default:
                    MainMenu();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            Task();
            Clear();
            MainMenu();
        }

        public void DeleteMenu()
        {
            WriteLine("---------------------");
            WriteLine("StationeryFirm");
            WriteLine("---------------------");
            WriteLine("1.Insert\n2.Update\n3.Delete\n4.Check\nEscape.Exit\n");
            ConsoleKey ch = ReadKey().Key;
            Clear();
            switch (ch)
            {
                case ConsoleKey.D1:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D2:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D3:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D4:
                    Task = () =>
                    {
                        WriteLine("Command: ");
                        db.MyCommand(ReadLine());
                    };
                    break;
                default:
                    MainMenu();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            Task();
            Clear();
            MainMenu();
        }

        public void UpdateMenu()
        {
            WriteLine("---------------------");
            WriteLine("StationeryFirm");
            WriteLine("---------------------");
            WriteLine("1.Add Product\n2.Add Type\n3.Add Firm\n4.Add Managers\nEscape.Exit\n");
            ConsoleKey ch = ReadKey().Key;
            Clear();
            switch (ch)
            {
                case ConsoleKey.D1:
                    Task = () =>
                    {
                        string dataField;
                        WriteLine("---New Product---");
                        Write("Enter name: ");
                        dataField = ReadLine();

                    };
                    break;
                case ConsoleKey.D2:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D3:
                    Task = () =>
                    {

                    };
                    break;
                case ConsoleKey.D4:
                    Task = () =>
                    {
                        WriteLine("Command: ");
                        db.MyCommand(ReadLine());
                    };
                    break;
                default:
                    MainMenu();
                    break;
                case ConsoleKey.Escape:
                    return;
            }
            Task();
            Clear();
            MainMenu();
        }
    }
}