using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AdoNet_DZ
{
    class Program
    {
        static void Main(string[] args)
        {
            FruitsVeg fv = new FruitsVeg();
            fv.Menu();

            Console.ReadKey();
        }
    }
}
