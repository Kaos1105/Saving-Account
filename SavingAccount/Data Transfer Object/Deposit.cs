using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Transfer_Object
{
    class Deposit
    {
        public int ID { get; set; }
        public int AccountID { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        public DateTime DateAdd { get; set; }
        public Deposit(int id, int accountID, string name, int money, DateTime dateAdd )
        {
            ID = id;
            AccountID = accountID;
            Name = name;
            Money = money;
            DateAdd = dateAdd;
        }
        public Deposit(DataRow row)
        {
            ID = (int)row["MaPhieuGuiTien"];
            AccountID = (int)row["MaSoTietKiem"];
            Name = row["TenKhachHang"].ToString();
            Money = Convert.ToInt32(row["SoTien"]);
            DateAdd = (DateTime)row["NgayGui"];
        }
        public Deposit() { }
    }
}
