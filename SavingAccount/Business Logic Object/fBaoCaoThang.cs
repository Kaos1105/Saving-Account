using SavingAccounst.Data_Access_Object;
using SavingAccounst.Data_Transfer_Object;
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
    public partial class fBaoCaoThang : Form
    {
        public fBaoCaoThang()
        {
            InitializeComponent();
            LoadComboBoxType();
        }
        void LoadListViewReport()
        {
            lVReportMonth.Items.Clear();
            List<ReportMonth> listReport = ReportMonthDAO.Instance.GetListReportMonth(dateReport.Value.Month, cBType.Text);
            int i = 1;
            foreach (ReportMonth report in listReport)
            {
                ListViewItem lsvItem = new ListViewItem(i++.ToString());
                lsvItem.SubItems.Add(report.Date.ToString());
                lsvItem.SubItems.Add(report.SumOpen.ToString());
                lsvItem.SubItems.Add(report.SumClose.ToString());
                lsvItem.SubItems.Add(report.Subtract.ToString());
                lVReportMonth.Items.Add(lsvItem);
            }
        }
        void LoadComboBoxType()
        {
            List<SavingType> listType = SavingTypeDAO.Instance.GetListType();
            cBType.DataSource = listType;
            cBType.DisplayMember = "Name";
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (DateTime.Compare(dateReport.Value.Date, DateTime.Today) > 0)
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
