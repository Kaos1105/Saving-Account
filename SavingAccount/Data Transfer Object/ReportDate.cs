using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavingAccount.Data_Transfer_Object
{
    class ReportDate
    {
        public string Name { get; set; }
        public long SumAdd { get; set; }
        public long SumWithdraw { get; set; }
        public long Subtract { get; set; }
        public ReportDate(string name, long sumAdd, long sumWithdraw)
        {
            Name = name;
            SumAdd = sumAdd;
            SumWithdraw = sumWithdraw;
            Subtract = Math.Abs(SumAdd-SumWithdraw);
        }
        public ReportDate(DataRow row)
        {
            Name = row["tltk_1"].ToString();
            if(Name=="")
                Name = row["tltk_2"].ToString();
            SumAdd = NullTryCast(row["TongThu"]);
            SumWithdraw = NullTryCast(row["TongChi"]);
            Subtract = Math.Abs(SumAdd - SumWithdraw);
        }
        long NullTryCast(object o)
        {
            if (o == DBNull.Value)
                 return 0;
            else return (long)o;
        }
    }
}
