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
using System.Data.SqlClient;
using System.IO;

namespace NordicDatabaseDLL
{
    public class ConnectionClass
    {

        #region Class Fields

        private SqlConnection connection;
        private const int patientNumber = 1;
        private const int amountOfColumns = 5;

        #endregion

        #region Enum

        public enum DBTableEnum
        {
            HeartRate,
            XAccelero,
            YAccelero,
            ZAccelero,
            Patients
        }

        #endregion

        #region Constructor

        public ConnectionClass()
        {
            SetConnection();
        }

        #endregion

        #region Connection Methods

        private void SetConnection()
        {
            connection = new SqlConnection("Data Source=95.51.168.59,41433;" +
                "Initial Catalog=test_db;" +
                "Integrated Security=False;" +
                "User ID=arekc;" +
                "Password=Nokia9500;" +
                "Connect Timeout=15;");
        }

        public void OpenConnection()
        {
            connection.Open();
        }

        public void CloseConnection()
        {
            connection.Close();
        }

        #endregion

        #region Refactoring String For Insert/Select

        private string SwitchToColumnName(DBTableEnum type)
        {
            string tableName = string.Empty;

            if (type == DBTableEnum.HeartRate)
                tableName = "heart_m_";
            else if (type == DBTableEnum.XAccelero)
                tableName = "x_m_";
            else if (type == DBTableEnum.YAccelero)
                tableName = "y_m_";
            else if (type == DBTableEnum.ZAccelero)
                tableName = "z_m_";

            return tableName;
        }

        private string RefactorToIdString(string columnName)
        {
            int length = columnName.Length;
            string toReturn = columnName.Substring(0, length - 2);

            return toReturn + "ID";
        }

        #endregion

        #region Insert/Select

        private void Insert(float[] data, DBTableEnum type)
        {
            string tableName = type.ToString();
            string columnName = SwitchToColumnName(type);
            string[] dt = new string[data.Length];


            for (int i = 0; i < data.Length; i++)
            {
                dt[i] = data[i].ToString();
                dt[i] = dt[i].Replace(',', '.');
            }

            string data1 = dt[0];
            string data2 = dt[1];
            string data3 = dt[2];
            string data4 = dt[3];
            string data5 = dt[4];

            string sql = string.Format("INSERT INTO {0} (patient_ID, {1}1, {1}2, {1}3, {1}4, {1}5) VALUES ({2}, {3}, {4}, {5}, {6}, {7});",
                tableName, columnName, patientNumber, data1, data2, data3, data4, data5);


            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();


        }

        private DataTable Select(DBTableEnum type, int patientID)
        {
            string tableName = type.ToString();
            string columnName = SwitchToColumnName(type);
            string idColumn = RefactorToIdString(columnName);

            string sql = string.Format("SELECT TOP 1 {0}1, {0}2, {0}3, {0}4, {0}5 FROM {1} WHERE patient_ID = {2} ORDER BY {3} DESC",
                columnName, tableName, patientID, idColumn);

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            return dt;
        }

        #endregion

        #region Patients

        public DataTable SelectAllPatients()
        {
            string sql = "SELECT * FROM Patients";

            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.ExecuteNonQuery();

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            return dt;
        }

        #endregion

        #region Get ID Methods

        public int GetLastHeartRateID()
        {
            DBTableEnum type = DBTableEnum.HeartRate;
            return SelectLastID(type);
        }

        public int GetLastXAxisID()
        {
            DBTableEnum type = DBTableEnum.XAccelero;
            return SelectLastID(type);
        }        

        public int GetLastYAxisID()
        {
            DBTableEnum type = DBTableEnum.YAccelero;
            return SelectLastID(type);
        }

        public int GetLastZAxisID()
        {
            DBTableEnum type = DBTableEnum.ZAccelero;
            return SelectLastID(type);
        }

        private int SelectLastID(DBTableEnum type)
        {
            string tableName = type.ToString();
            string columnName = SwitchToColumnName(type);
            string idColumn = RefactorToIdString(columnName);

            string sql = string.Format("SELECT TOP 1 {0} FROM {1} ORDER BY {0} DESC", idColumn, tableName);
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, connection);
            dt.Load(cmd.ExecuteReader());

            int ID = Convert.ToInt32(dt.Rows[0][0]);
            return ID;
        }

        #endregion


        #region HearRate

        public void InsertDataHeartRate(float[] hearRateData)
        {
            Insert(hearRateData, DBTableEnum.HeartRate);
        }

        public float[] SelectDataHeartRate()
        {
            DataTable dt = Select(DBTableEnum.HeartRate, patientNumber);
            float[] heartRateData = new float[amountOfColumns];

            for (int i = 0; i < amountOfColumns; i++)
            {
                string column = "heart_m_" + (i + 1);
                heartRateData[i] = float.Parse(dt.Rows[0][column].ToString());
            }

            return heartRateData;
        }

        #endregion

        #region XAccelero

        public void InsertDataXAccelero(float[] xData)
        {
            Insert(xData, DBTableEnum.XAccelero);
        }

        public float[] SelectDataXAccelero()
        {
            DataTable dt = Select(DBTableEnum.XAccelero, patientNumber);
            float[] xData = new float[amountOfColumns];

            for (int i = 0; i < amountOfColumns; i++)
            {
                string column = "x_m_" + (i + 1);
                xData[i] = float.Parse(dt.Rows[0][column].ToString());
            }

            return xData;
        }

        #endregion

        #region YAccelero

        public void InsertDataYAccelero(float[] yData)
        {
            Insert(yData, DBTableEnum.YAccelero);
        }

        public float[] SelectDataYAccelero()
        {
            DataTable dt = Select(DBTableEnum.YAccelero, patientNumber);
            float[] yData = new float[amountOfColumns];

            for (int i = 0; i < amountOfColumns; i++)
            {
                string column = "y_m_" + (i + 1);
                yData[i] = float.Parse(dt.Rows[0][column].ToString());
            }

            return yData;
        }

        #endregion

        #region ZAccelero

        public void InsertDataZAccelero(float[] zData)
        {
            Insert(zData, DBTableEnum.ZAccelero);
        }

        public float[] SelectDataZAccelero()
        {
            DataTable dt = Select(DBTableEnum.ZAccelero, patientNumber);
            float[] zData = new float[amountOfColumns];

            for (int i = 0; i < amountOfColumns; i++)
            {
                string column = "z_m_" + (i + 1);
                zData[i] = float.Parse(dt.Rows[0][column].ToString());
            }

            return zData;
        }

        #endregion
    }
}
