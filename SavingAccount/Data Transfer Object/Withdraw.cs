using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Transfer_Object
{
    class Withdraw
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public DateTime DateWithdraw { get; set; }
        public Withdraw(int id, int accountID, string name, int money, DateTime dateAdd)
        {
            ID = id;
            AccountID = accountID;
            Name = name;
            Money = money;
            DateWithdraw = dateAdd;
        }
        public Withdraw(DataRow row)
        {
            ID = (int)row["MaPhieuRutTien"];
            AccountID = (int)row["MaSoTietKiem"];
            Name = row["TenKhachHang"].ToString();
            Money = Convert.ToInt32(row["SoTienRut"]);
            DateWithdraw = (DateTime)row["NgayRut"];
        }
        public Withdraw() { }
    }
}
