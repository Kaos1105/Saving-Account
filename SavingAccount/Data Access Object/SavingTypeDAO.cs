using SavingAccounst.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccounst.Data_Access_Object
{
    class SavingTypeDAO
    {
        private static SavingTypeDAO instance;

        public static SavingTypeDAO Instance
        {
            get { if (instance == null) instance = new SavingTypeDAO(); return instance; }
            private set => instance = value;
        }
        private SavingTypeDAO() { }
        public List<SavingType> GetListType()
        {
            List<SavingType> list = new List<SavingType>();
            string query = "select * from LoaiTietKiem";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                SavingType type = new SavingType(item);
                list.Add(type);
            }
            return list;
        }
        public List<SavingType> GetListActiveType()
        {
            List<SavingType> list = new List<SavingType>();
            string query = "select * from LoaiTietKiem where DangDung = " + 1 ;
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                SavingType type = new SavingType(item);
                list.Add(type);
            }
            return list;
        }
        public void UpdateSavingType(string[] listString)
        {
            SavingType type = new SavingType();
            if(listString[1]=="0")
            {
                type.Name = listString[0];
                type.InterestRate = DBNull.Value;
                type.InterestRateMonth = Convert.ToDouble(listString[3]);
                type.MinMoney = Convert.ToInt32(listString[4]);
                type.MinAddMoney= Convert.ToInt32(listString[5]);
                type.DateCanAdded=Convert.ToInt32(listString[6]);
                type.DateWithdraw = Convert.ToInt32(listString[7]);
                type.IsActive = Convert.ToBoolean(Convert.ToInt32(listString[8]));
            }
            else
            {
                type.Name = listString[0];
                type.InterestRateMonth = DBNull.Value;
                type.InterestRate = Convert.ToDouble(listString[2]);
                type.MinMoney = Convert.ToInt32(listString[4]);
                type.MinAddMoney = Convert.ToInt32(listString[5]);
                type.DateCanAdded = Convert.ToInt32(listString[6]);
                type.DateWithdraw = Convert.ToInt32(listString[7]);
                type.IsActive = Convert.ToBoolean(Convert.ToInt32(listString[8]));
            }
            string query = "USP_InsertLoaiTietKiem @name , @interest , @interestMonth , @money , @moneyAdd , @dateAdd , @dateWithdraw , @isActive";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { type.Name, type.InterestRate, type.InterestRateMonth, type.MinMoney, type.MinAddMoney, type.DateCanAdded, type.DateWithdraw, type.IsActive });
        }
        public void InsertSavingType(string[] listString)
        {
            SavingType type = new SavingType();
            if (listString[1] == "0")
            {
                type.Name = listString[0];
                type.Period = Convert.ToInt32(listString[1]);
                type.InterestRate = DBNull.Value;
                type.InterestRateMonth = Convert.ToDouble(listString[3]);
                type.MinMoney = Convert.ToInt32(listString[4]);
                type.MinAddMoney = Convert.ToInt32(listString[5]);
                type.DateCanAdded = Convert.ToInt32(listString[6]);
                type.DateWithdraw = Convert.ToInt32(listString[7]);
                type.IsActive = Convert.ToBoolean(Convert.ToInt32(listString[8]));
            }
            else
            {
                type.Name = listString[0];
                type.Period = Convert.ToInt32(listString[1]);
                type.InterestRateMonth = DBNull.Value;
                type.InterestRate = Convert.ToDouble(listString[2]);
                type.MinMoney = Convert.ToInt32(listString[4]);
                type.MinAddMoney = Convert.ToInt32(listString[5]);
                type.DateCanAdded = Convert.ToInt32(listString[6]);
                type.DateWithdraw = Convert.ToInt32(listString[7]);
                type.IsActive = Convert.ToBoolean(Convert.ToInt32(listString[8]));
            }
            string query = "USP_AddLoaiTietKiem @name , @period , @interest , @interestMonth , @money , @moneyAdd , @dateAdd , @dateWithdraw , @isActive";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { type.Name, type.Period, type.InterestRate, type.InterestRateMonth, type.MinMoney, type.MinAddMoney, type.DateCanAdded, type.DateWithdraw, type.IsActive });
        }
        public void DeleteSavingtype(string str)
        {
            string query = "Delete from LoaiTietKiem where TenLoaiTietKiem = N'" + str + "'";
            DataProvider.Instance.ExecuteNonQuery(query);
        }
        public int CheckSavingTypeInUse(int id)
        {
            string query = "select MaLoaiTietKiem from LoaiTietKiem L where exists (select 1 from SoTietKiem S where L.MaLoaiTietKiem = S.MaLoaiTietKiem) and L.MaLoaiTietKiem = " + id;
            int? result =(int?)DataProvider.Instance.ExecuteScalar(query);
            if (result == null)
                return -1;
            return 1;
        }
    }
}
