using System;
using System.Data.SqlClient;

using static System.Console;

namespace ConsoleAdo
{
    class Program
    {
        const string connectionString =
            "Data Source=10.6.1.191;Initial Catalog=Onischenko;Persist Security Info=True;User ID=pv011;Password=147852";
        private static SqlConnection connection;
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            IAsyncResult asyncResult = null;

            WriteLine("Start connection...");
            connection.Open();
            SqlCommand command = new SqlCommand("waitfor delay '00:00:05'; select * from Doctors;", connection);
            WriteLine("Begin read");
            asyncResult = command.BeginExecuteReader(ReadCallBack, command);
            WriteLine("Continuos Main()");

            WriteLine("\nWait for result");
            while (!asyncResult.IsCompleted)
            {
                Write(".");
                System.Threading.Thread.Sleep(1000);
            }
            WriteLine("End Main(), press ENTER");
            Console.ReadLine();
        }

        static void ReadCallBack(IAsyncResult ar)
        {
            WriteLine("\nBegin ReadCallBack...");
            SqlCommand cmd = (ar.AsyncState as SqlCommand);
            SqlDataReader reader = cmd.EndExecuteReader(ar);

            for (int i = 0; i < reader.FieldCount; i++)
            {
                Write(reader.GetName(i) + "\t");
            }
            Write("\n");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Write(reader[i] + "\t");
                }
                Write("\n");
            }
            reader.Close();
            cmd.Connection.Close();
            WriteLine("\nEnd ReadCallBack...");
        }
    }
}
