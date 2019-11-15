using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Transfer_Object
{
    class ReportMonth
    {
        public string Date { get; set; }
        public int SumOpen {get; set;}
        public int SumClose { get; set; }
        public int Subtract { get; set; }
        public ReportMonth(string date, int sumOpen, int sumClose, int subtract)
        {
            Date = date;
            SumOpen = sumOpen;
            SumClose = sumClose;
            Subtract = Math.Abs(SumOpen - SumClose);
        }
        public ReportMonth(DataRow row)
        {
            Date = row["NgayMoSo"].ToString();
            if (Date == "")
                Date = row["NgayRut"].ToString();
            SumOpen = NullTryCast(row["SoMo"]);
            SumClose=NullTryCast(row["SoDong"]);
            Subtract= Math.Abs(SumOpen - SumClose);
            Date = Date.Split(' ')[0];
        }
        int NullTryCast(object o)
        {
            if (o == DBNull.Value)
                return 0;
            else return (int)o;
        }
    }
}
