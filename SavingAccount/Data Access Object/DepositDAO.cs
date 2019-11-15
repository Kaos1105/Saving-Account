using SavingAccounst.Data_Access_Object;
using SavingAccounst.Data_Transfer_Object;
using SavingAccount.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Access_Object
{
    class DepositDAO
    {
        private static DepositDAO instance;
        List<Account> list;
        public static DepositDAO Instance
        {
            get { if (instance == null) instance = new DepositDAO(); return instance; }
            private set => instance = value;
        }
        private DepositDAO() { }
        public int GetActiveAccount(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            if (list.Count == 0)
                return -1;
            if (list[0].Status == false)
                return -2;
            return list[0].ID;
        }
        public void InsertDeposit(string[]listStr, DateTime date)
        {
            Deposit deposit = new Deposit();
            deposit.AccountID = Convert.ToInt32(listStr[0]);
            deposit.Name = listStr[1];
            deposit.Money = Convert.ToInt32(listStr[2]);
            deposit.DateAdd = date;
            string query = "USP_InsertPhieuGuiTien @accID , @name , @money , @dateAdd";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] {deposit.AccountID, deposit.Name, deposit.Money, deposit.DateAdd});
        }
        public int GetMinMoney(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            int typeID = list[0].TypeID;
            string query = "select TienGuiThemToiThieu from LoaiTietKiem where MaLoaiTietKiem = " + typeID;
            int minMoney = (int)DataProvider.Instance.ExecuteScalar(query);
            return minMoney;
        }
        public int GetMaxID()
        {
            string query = "select max(MaPhieuGuiTien) from PhieuGuiTien";
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar(query);
            }
            catch
            {
                return -1;
            }

        }
        public DateTime GetDateAdd(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            DateTime dateCanAdd = list[0].DateCanAdd;
            return dateCanAdd;
        }
    }
}
