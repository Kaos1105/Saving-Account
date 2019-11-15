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
    public partial class fMoSoTietKiem : Form
    {
        List<SavingType> listType = SavingTypeDAO.Instance.GetListActiveType();
        public fMoSoTietKiem()
        {
            InitializeComponent();
            LoadComboBoxType();
        }

        #region Methods
        void LoadComboBoxType()
        {
            listType = SavingTypeDAO.Instance.GetListActiveType();
            cBType.DataSource = listType;
            cBType.DisplayMember = "Name";
        }
        #endregion

        #region Events
        private void Button1_Click(object sender, EventArgs e)
        {
            int money;
            int cmnd;
            try
            {
                money = Convert.ToInt32(tBMoney.Text);
                int minMoney = listType[cBType.SelectedIndex].MinMoney;
                if (money < minMoney)
                {
                    MessageBox.Show("Số tiền mở sổ phải tối thiểu " + minMoney + " VNĐ");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Số tiền mở sổ phải là một số nguyên");
                return;
            }
            if (tBName.Text == "")
            {
                MessageBox.Show("Tên khách hàng không được để trống");
                return;
            }
            try
            {
                cmnd = Convert.ToInt32(tBCMND.Text);
                foreach (char c in tBCMND.Text)
                {
                    if (tBCMND.Text[0] == '0')
                    {
                        if (Math.Floor(Math.Log10(cmnd) + 2) != 9)
                        {
                            MessageBox.Show("Số CMND phải là một nguyên có 9 chữ số");
                            return;
                        }
                    }
                    else
                    {
                        if (Math.Floor(Math.Log10(cmnd) + 1) != 9)
                        {
                            MessageBox.Show("Số CMND phải là một nguyên có 9 chữ số");
                            return;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Số CMND không hợp lệ");
                return;
            }
            if (tBAddress.Text == "")
            {
                MessageBox.Show("Địa chỉ không được để trống");
                return;
            }
            if(DateTime.Compare(dateOpen.Value.Date, DateTime.Today)>0)
            {
                MessageBox.Show("Thời gian mở sổ không hợp lệ");
                return;
            }
            try
            {
                AccountDAO.Instance.InsertAccount(new int[] { listType[cBType.SelectedIndex].ID, money }, new string[] { tBName.Text, tBCMND.Text, tBAddress.Text }, dateOpen.Value);
                MessageBox.Show("Mở sổ thành công");
                tBAccountID.Text = AccountDAO.Instance.GetMaxAccountID().ToString();
            }
            catch
            {
                MessageBox.Show("Mở sổ không thành công\n Xin quý khách vui lòng thử lại");
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            tBAccountID.Clear();
            tBMoney.Clear();
            tBName.Clear();
            tBCMND.Clear();
            tBAddress.Clear();
            LoadComboBoxType();
        }
        #endregion

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
