using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeShop.Data_Access_Object
{
    class DataProvider
    {
        private static DataProvider instance;
        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set => instance = value;
        }
        private DataProvider()
        {
        }
        private string connectionStr = @"Data Source=.\sqlexpress;Initial Catalog=CoffeShop;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null) //chạy  query với nhiều parameter 
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionStr))// dùng using để tự hủy SqlConnection sau khi kết nối đóng
            {
                connection.Open();                                             // mở kết nối
                SqlCommand command = new SqlCommand(query, connection);
                if(parameter != null)
                {
                    string[] listPara = query.Split(' ');                      //chia query ra " EXEC dbo.USP_GetAccountByUserName @userName @abcxyz @dsadas"
                    int i = 0; 
                    foreach(string item in listPara)
                    {
                        if (item.Contains('@'))                                // @userName
                        {
                            command.Parameters.AddWithValue(item, parameter[i]); //AddWithValue(@userName, "KaosLord");
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(data);
                connection.Close();
            }
            return data;
        }
        public int ExecuteNonQuery(string query, object[] parameter = null) //không xuất Query chỉ xuất ra số dòng thành công 
        {
            int data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        }
        public object ExecuteScalar(string query, object[] parameter = null) //chỉ xuất ra kết quả đầu tiên của Query, thường sử dụng cho các tính toán (AVG, MAX, MIN) 
        {
            object data = 0;

            using (SqlConnection connection = new SqlConnection(connectionStr))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        }

    }
}
