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
    class ReportMonthDAO
    {
        private static ReportMonthDAO instance;

        public static ReportMonthDAO Instance
        {
            get { if (instance == null) instance = new ReportMonthDAO(); return instance; }
            private set => instance = value;
        }
        private ReportMonthDAO() { }
        public List<ReportMonth> GetListReportMonth(int month, string name)
        {
            List<ReportMonth> list = new List<ReportMonth>();
            string query = "select * from (select stk.NgayMoSo, count(stk.MaLoaiTietKiem) as SoMo from SoTietKiem stk, LoaiTietKiem ltk where MONTH(stk.NgayMoSo) = " + month + " and ltk.TenLoaiTietKiem = N'" + name + "' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem group by stk.NgayMoSo) t1 full outer join (select prt.NgayRut, count(prt.MaPhieuRutTien) as SoDong from PhieuRutTien prt, LoaiTietKiem ltk, SoTietKiem stk where MONTH(prt.NgayRut) =" + month + " and prt.TatToan=1 and ltk.TenLoaiTietKiem = N'" + name + "' and stk.MaLoaiTietKiem = ltk.MaLoaiTietKiem and stk.MaSoTietKiem = prt.MaSoTietKiem group by prt.NgayRut) t2 on t1.NgayMoSo = t2.NgayRut order by NgayRut asc, NgayMoSo asc";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                ReportMonth report = new ReportMonth(item);
                list.Add(report);
            }
            return list;
        }

    }
}
