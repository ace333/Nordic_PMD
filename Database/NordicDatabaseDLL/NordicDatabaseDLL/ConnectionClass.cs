using System;
using System.Data;
using System.Data.SqlClient;

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

        enum DBTableEnum
        {
            Temperature,
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
            connection = new SqlConnection("Data Source=192.168.11.234;" +
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
            else if (type == DBTableEnum.Temperature)
                tableName = "temp_m_";
            else if (type == DBTableEnum.XAccelero)
                tableName = "x_m_";
            else if (type == DBTableEnum.YAccelero)
                tableName = "y_m_";
            else if (type == DBTableEnum.ZAccelero)
                tableName = "x_m_";

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

            float data1 = data[0];
            float data2 = data[1];
            float data3 = data[2];
            float data4 = data[3];
            float data5 = data[4];

            string sql = string.Format("INSERT INTO {0} (patient_ID, {1}1, {1}2, {1}3, {1}4, {1}5) VALUES ({2}, {3}, {4}, {5}, {6}, {7});",
                tableName, columnName, patientNumber, data1, data2, data3, data4, data5);

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, connection, transaction);
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                }
                catch(Exception)
                {
                    transaction.Rollback();
                }
            }
                
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

        #region Temperature

        public void InsertDataTemperature(float[] temperatureData)
        {
            Insert(temperatureData, DBTableEnum.Temperature);
        }

        public float[] SelectDataTemperature()
        {
            DataTable dt = Select(DBTableEnum.Temperature, patientNumber);
            float[] tempData = new float[amountOfColumns];

            for (int i = 0; i < amountOfColumns; i++)
            {
                string column = "temp_m_" + (i + 1);
                tempData[i] = float.Parse(dt.Rows[0][column].ToString());
            }

            return tempData;

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
