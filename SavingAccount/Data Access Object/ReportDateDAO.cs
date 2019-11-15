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
    class ReportDateDAO
    {
        private static ReportDateDAO instance;

        public static ReportDateDAO Instance
        {
            get { if (instance == null) instance = new ReportDateDAO(); return instance; }
            private set => instance = value;
        }
        private ReportDateDAO() { }
        public List<ReportDate> GetListReportDate(DateTime date)
        {
            List<ReportDate> list = new List<ReportDate>();
            string strDate = date.ToString("MM/dd/yyyy");
            string query = "select * from (select TenLoaiTietKiem as tltk_1, sum(SoTienGui) as TongThu from PhieuGuiTien pgt, LoaiTietKiem ltk, SoTietKiem stk where pgt.MaSoTietKiem = stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and pgt.NgayGui = '"+strDate+"' group by TenLoaiTietKiem) t1 full outer join (select TenLoaiTietKiem as tltk_2, sum(SoTienRut) as TongChi from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk where prt.MaSoTietKiem = stk.MaSoTietKiem and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and prt.NgayRut = '"+strDate+"' group by TenLoaiTietKiem) t2 on t1.tltk_1 = t2.tltk_2";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReportDate report = new ReportDate(item);
                list.Add(report);
            }
            return list;
        }
    }
}
