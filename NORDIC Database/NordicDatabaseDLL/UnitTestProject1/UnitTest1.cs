/*
 * Copyright (c) 2015, Arkadiusz Chudy, Bartłomiej Cerek.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
1. Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
2. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
3. Neither the name of the copyright holder nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

*/

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
            float[] X = { 0.24F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
                0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 2.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            5.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F};

            float[] Y = { 0.24F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
                0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F};

            float[] Z = { 0.24F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
                0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F,
            0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F, 0.13F};

            int amount = 30;
            float delta = 0.5F;
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
