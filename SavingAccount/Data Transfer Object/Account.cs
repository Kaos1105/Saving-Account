using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccounst.Data_Transfer_Object
{
    class Account
    {
        public int ID { get; set; }
        public int TypeID { get; set; }
        public string Name { get; set; }
        public string CMND { get; set; }
        public string Address { get; set; }
        public DateTime DateOpen { get; set; }
        public int Money { get; set; }
        public bool Status { get; set; }
        public string DateDue { get; set; }
        public DateTime DateCanWithDraw { get; set; }
        public DateTime DateCanAdd { get; set; }
        public Account(int id, int typeID, string name, string cmnd, string address, DateTime dateOpen, DateTime dateDue, DateTime dateCanWithDraw, DateTime dateCanAdd, int money, bool status)
        {
            ID = id;
            TypeID = typeID;
            Name = name;
            CMND = cmnd;
            Address = address;
            DateOpen = dateOpen;
            DateDue = dateDue.ToString("MM/dd/yyyy");
            DateCanWithDraw = dateCanWithDraw;
            DateCanAdd = dateCanAdd;
            Money = money;
            Status = status;
        }
        public Account(DataRow row)
        {
            ID = (int)row["MaSoTietKiem"];
            TypeID = (int)row["MaLoaiTietKiem"];
            Name = row["TenKhachHang"].ToString();
            CMND = row["SoCMND"].ToString();
            Address = row["DiaChi"].ToString();
            DateOpen = (DateTime)row["NgayMoSo"];
            DateDue = ObjectTryCast(row["NgayDaoHan"]);
            DateCanWithDraw = (DateTime)row["NgayRutTien"];
            DateCanAdd = (DateTime)row["NgayGoiThem"];
            Money = Convert.ToInt32(row["SoDu"]);
            Status = (bool)row["DongSo"];
        }
        public Account() { }
        string ObjectTryCast(object o)
        {
            string result;
            try
            {
                DateTime date = (DateTime)o;
                result = date.ToString("MM/dd/yyyy");
            }
            catch
            {
                result = "Không kì hạn";
            }
            return result;
        }
    }
}
