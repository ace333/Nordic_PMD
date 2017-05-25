using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NordicDatabaseDLL;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionClass conn = new ConnectionClass();
            conn.OpenConnection();

            if (conn.InsertDataXAccelero(new float[] { 1, 1, 5, 5, 5 }))
                Console.WriteLine("GIT");
            else
                Console.WriteLine("NOPE");

            
            conn.CloseConnection();
        }
    }
}
