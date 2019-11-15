using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccounst.Data_Transfer_Object
{
    class SavingType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Period { get; set; }
        public bool IsActive { get; set; }
        public int DateWithdraw { get; set; }
        public int MinMoney { get; set; }
        public int MinAddMoney { get; set; }
        public object InterestRate { get; set; }
        public object InterestRateMonth { get; set; }
        public int DateCanAdded { get; set; }
        public SavingType(int id, string name, int period, bool isActive, int dateWithdraw, int minMoney, int minAddMoney, double interestRate, double interestRateMonth, int dateCanAdd)
        {
            ID = id;
            Name = name;
            Period = period;
            IsActive = isActive;
            DateWithdraw = dateWithdraw;
            MinMoney = minMoney;
            MinAddMoney = minAddMoney;
            InterestRate = interestRate;
            InterestRateMonth = interestRateMonth;
            DateCanAdded = dateCanAdd;
        }
        public SavingType(DataRow row)
        {
            ID = (int)row["MaLoaiTietKiem"];
            Name = row["TenLoaiTietKiem"].ToString();
            Period = (int)row["KyHan"];
            IsActive = (bool)row["DangDung"];
            DateWithdraw = (int)row["SoNgayDuocRut"];
            MinMoney = (int)row["SoTienGuiToiThieu"];
            MinAddMoney = (int)(row["TienGuiThemToiThieu"]);
            InterestRate = ObjectTryCast(row["LaiSuat"]);
            InterestRateMonth = ObjectTryCast(row["LaiSuatThang"]);
            DateCanAdded =(int)(row["ThoiGianDuocGoiThem"]);
        }
        public SavingType() { }
        //int? IntTryCast(object o)
        //{
        //    int? result;
        //    try
        //    {
        //        result=Convert.ToInt32(o);
        //    }
        //    catch(InvalidCastException e)
        //    {
        //        result = null;
        //    }
        //    return result;
        //}
        object ObjectTryCast(object o)
        {
            object result;
            try
            {
                result = Convert.ToDouble(o);
            }
            catch
            {
                result = null;
            }
            return result;
        }
        object IntObjectTryCast(object o)
        {
            object result;
            try
            {
                result = (int)o;
            }
            catch
            {
                result = null;
            }
            return result;
        }

    }
}
