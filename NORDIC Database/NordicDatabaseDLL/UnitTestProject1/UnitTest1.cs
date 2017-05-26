using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NordicDatabaseDLL;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Detect_Fall()
        {
            //arrange
            float[] X = { 0.24F, 0.13F, 0.13F, 0.13F, 0.13F };
            float[] Y = { 0.13F, 0.13F, 0.13F, 0.43F, 0.91F };
            float[] Z = { 2.45F, 0.13F, 0.13F, 0.13F, 0.43F };
            int amount = 5;
            float delta = 1;
            FallChecker check = new FallChecker();

            //act
            bool result = check.CheckIfFall(X, Y, Z, delta, amount);

            //assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void Can_Select_All_Patients()
        {
            //arrange
            ConnectionClass conn = new ConnectionClass();

            //act
            conn.OpenConnection();
            DataTable resutl = conn.SelectAllPatients();
            conn.CloseConnection();

            //assert    
            Assert.AreEqual(1, resutl.Rows[0][0]);
            Assert.AreEqual(2, resutl.Rows[1][0]);
        }

        [TestMethod]
        public void Can_Select_Last_X_ID()
        {
            //arrange
            ConnectionClass conn = new ConnectionClass();

            //act
            conn.OpenConnection();
            int result = conn.GetLastXAxisID();

            //assert
            Assert.AreEqual(2040, result);
        }
    }
}
