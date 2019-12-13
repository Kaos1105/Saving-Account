using SavingAccount.Data_Access_Object;
using SavingAccount.Data_Transfer_Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SavingAccounst
{
    public partial class fBaoCaoNgay : Form
    {
        public fBaoCaoNgay()
        {
            InitializeComponent();
        }
        void LoadListViewReport()
        {
            lVReportDate.Items.Clear();
            List<ReportDate> listReport = ReportDateDAO.Instance.GetListReportDate(dateReport.Value.Date);
            int i = 1;
            foreach (ReportDate report in listReport)
            {
                ListViewItem lsvItem = new ListViewItem(i++.ToString());
                lsvItem.SubItems.Add(report.Name.ToString());
                lsvItem.SubItems.Add(report.SumAdd.ToString());
                lsvItem.SubItems.Add(report.SumWithdraw.ToString());
                lsvItem.SubItems.Add(report.Subtract.ToString());
                lVReportDate.Items.Add(lsvItem);
            }
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(dateReport.Value.Date, DateTime.Today) < 0)
            {
                MessageBox.Show("Thời gian lập báo cáo không hợp lệ");
                return;
            }
            try
            {
                LoadListViewReport();
                MessageBox.Show("Tải báo cáo thành công");
            }
            catch
            {
                MessageBox.Show("Tải báo cáo không thành công");
            }
        }
    }
}
