using SavingAccounst.Data_Access_Object;
using SavingAccounst.Data_Transfer_Object;
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
    public partial class fDanhSachSo : Form
    {
        public fDanhSachSo()
        {
            InitializeComponent();
            LoadDataGridView();
        }

        #region Methods
        void LoadDataGridView()
        {
            //dGVAccount.AutoGenerateColumns = true;
            //dGVAccount.Update();
            //dGVAccount.Refresh();
            List<Account> list = AccountDAO.Instance.GetListAccount();
            dGVAccount.DataSource = list;
            dGVAccount.Columns[0].HeaderText = "Mã sổ";
            dGVAccount.Columns[1].HeaderText = "Mã loại tiết kiệm";
            dGVAccount.Columns[2].HeaderText = "Tên khách hàng";
            dGVAccount.Columns[3].HeaderText = "Số CMND";
            dGVAccount.Columns[4].HeaderText = "Địa chỉ";
            dGVAccount.Columns[5].HeaderText = "Ngày mở sổ";
            dGVAccount.Columns[6].HeaderText = "Số dư";
            dGVAccount.Columns[7].HeaderText = "Trạng thái";
            dGVAccount.Columns[8].HeaderText = "Ngày đáo hạn";
            dGVAccount.Columns[9].HeaderText = "Ngày được rút tiền";
            dGVAccount.Columns[10].HeaderText = "Ngày được gửi thêm tiền";
            //foreach(DataGridViewRow row in dGVAccount.Rows)
            //{
            //    if (row.Cells[8].Value == null)
            //        row.Cells[8].Value = "Không kì hạn";
            //}
        }
        void LoadDataGridViewByID(int id)
        { 
            //Account acc = AccountDAO.Instance.GetAccountByID(id);
            //dGVAccount.DataSource = acc;
            List<Account> list = AccountDAO.Instance.GetAccountByID(id);
            dGVAccount.DataSource = list;
        }
        #endregion

        #region Events
        private void Button2_Click(object sender, EventArgs e)
        {
            LoadDataGridView();
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(tBIDAccount.Text);
                LoadDataGridViewByID(id);
            }
            catch
            {
                MessageBox.Show("Mã sổ tiết kiệm phải là một số nguyên");
            }
        }
        #endregion

        private void tBIDAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
