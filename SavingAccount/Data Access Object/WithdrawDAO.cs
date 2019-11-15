using SavingAccounst.Data_Access_Object;
using SavingAccounst.Data_Transfer_Object;
using SavingAccount.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Access_Object
{
    class WithdrawDAO
    {
        private static WithdrawDAO instance;
        List<Account> list;
        public static WithdrawDAO Instance
        {
            get { if (instance == null) instance = new WithdrawDAO(); return instance; }
            private set => instance = value;
        }
        private WithdrawDAO() { }
        public int GetActiveAccount(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            if (list.Count == 0)
                return -1;
            if (list[0].Status == false)
                return -2;
            return list[0].ID;
        }
        public void InsertWithdraw(string[] listStr, DateTime date, int isClose)
        {
            Withdraw withdraw  = new Withdraw();
            withdraw.AccountID = Convert.ToInt32(listStr[0]);
            withdraw.Name = listStr[1];
            withdraw.Money = Convert.ToInt32(listStr[2]);
            withdraw.DateWithdraw = date;
            string query = "USP_InsertPhieuRutTien @accID , @name , @money , @dateWithdraw , @tatToan";
            DataProvider.Instance.ExecuteNonQuery(query, new object[] { withdraw.AccountID, withdraw.Name, withdraw.Money, withdraw.DateWithdraw, isClose});
        }
        public List<int> GetMin_CurrMoney(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            int typeID = list[0].TypeID;
            string query = "select SoTienGuiToiThieu from LoaiTietKiem where MaLoaiTietKiem = " + typeID;
            int minMoney = (int)DataProvider.Instance.ExecuteScalar(query);
            int currMoney = list[0].Money;
            List<int> listInt = new List<int>();
            listInt.Add(minMoney);
            listInt.Add(currMoney);
            return listInt;
        }
        public int GetMaxID()
        {
            string query = "select max(MaPhieuRutTien) from PhieuRutTien";
            try
            {
                return (int)DataProvider.Instance.ExecuteScalar(query);
            }
            catch
            {
                return -1;
            }
        }
        public bool CloseAccountByID(int id)
        {
            try
            {
                string query = "update SoTietKiem set DongSo=0 where MaSoTietKiem = " + id;
                DataProvider.Instance.ExecuteNonQuery(query);
                return true;
            }
            catch
            {
                return false;
            }

        }

        public List<double> InterestMoney_CanWithdraw(int id, DateTime dateWithdraw)
        {
            List<double> listReturn = new List<double>();
            double interestMoney = 0;
            int canWithdraw = 0;
            list = AccountDAO.Instance.GetAccountByID(id);
            int period = WithdrawDAO.Instance.GetPeriod(id);
            int money = list[0].Money;
            int typeId = list[0].TypeID;
            DateTime dateCanWithdraw = list[0].DateCanWithDraw;
            if (DateTime.Compare(dateCanWithdraw.Date, dateWithdraw.Date)<=0)
            {
                if(period>0)
                {
                    string query = "select LaiSuat from LoaiTietKiem where MaLoaiTietKiem = " + typeId;
                    double interestRate= (double)DataProvider.Instance.ExecuteScalar(query);
                    if (DateTime.Compare((Convert.ToDateTime(list[0].DateDue)), dateWithdraw.Date) <=0)
                    {
                        interestMoney = money * (interestRate / 100);
                    }
                    else
                        interestMoney = 0;
                    
                }
                else
                {
                    string query = "select LaiSuatThang from LoaiTietKiem where MaLoaiTietKiem = " + typeId;
                    double interestRate = (double)DataProvider.Instance.ExecuteScalar(query);
                    int dateSub = dateWithdraw.Subtract(dateCanWithdraw).Days;
                    interestMoney = money * (dateSub / 30) * (interestRate/100);
                }
                canWithdraw = 1;
            }
            else
                canWithdraw = 0;
            listReturn.Add(interestMoney);
            listReturn.Add(canWithdraw);
            return listReturn;
        }
        public DateTime getDueDate(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            return Convert.ToDateTime(list[0].DateDue);
        }
        public int GetPeriod(int id)
        {
            list = AccountDAO.Instance.GetAccountByID(id);
            int typeID = list[0].TypeID;
            string query = "select KyHan from LoaiTietKiem where MaLoaiTietKiem = " + typeID;
            int periodDate = (int)DataProvider.Instance.ExecuteScalar(query);
            return periodDate;
        }
    }
}
