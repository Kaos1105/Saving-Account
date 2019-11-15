using SavingAccounst.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccounst.Data_Access_Object
{
    class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get { if (instance == null) instance = new AccountDAO(); return instance; }
            private set => instance = value;
        }
        private AccountDAO() { }
        public List<Account> GetListAccount()
        {
            List<Account> list = new List<Account>();
            string query = "select * from SoTietKiem";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Account acc = new Account(item);
                list.Add(acc);
            }
            return list;
        }
        public List<Account> GetAccountByID(int id)
        {
            List<Account> list = new List<Account>();
            string query = "select * from SoTietKiem where MaSoTietKiem = " + id;
            DataTable data = DataProvider.Instance.ExecuteQuery(query); ;
            foreach (DataRow item in data.Rows)
            {
                Account acc = new Account(item);
                list.Add(acc);
            }
            return list;
        }
        public void InsertAccount( int [] listInt, string [] listString, DateTime date)
        {
            Account acc = new Account();
            acc.TypeID = listInt[0];
            acc.Money = listInt[1];
            acc.Name = listString[0];
            acc.CMND = listString[1];
            acc.Address = listString[2];
            acc.DateOpen = date;
            string query = " USP_InsertSoTietKiem @typeID , @money , @name , @cmnd , @address , @dateOpen";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { acc.TypeID, acc.Money, acc.Name, acc.CMND, acc.Address, acc.DateOpen });
        }
        public int GetMaxAccountID()
        {
            string query = "select max(MaSoTIetKiem) from SoTietKiem";
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar(query);
            }
            catch
            {
                return -1;
            }
        }
    }
}
