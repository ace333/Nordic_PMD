using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NordicDatabaseDLL;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionClass conn = new ConnectionClass();

            conn.OpenConnection();

            //conn.InsertDataXAccelero(new float[] { 0, 0, 0, 1, 2 });
            //conn.InsertDataYAccelero(new float[] { -1, -1, -1, 0, 1 });
            //conn.InsertDataZAccelero(new float[] { 1, 1, 2, 2, 2 });

            conn.InsertDataHeartRate(new float[] { 7, 7, 7, 7, 7 });

            Console.ReadKey();

            conn.CloseConnection();

            
        }
    }
}
