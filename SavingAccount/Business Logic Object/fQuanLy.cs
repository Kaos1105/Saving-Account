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
    public partial class fQuanLy : Form
    {
        List<SavingType> listType = SavingTypeDAO.Instance.GetListType();
        public fQuanLy()
        {
            InitializeComponent();
            LoadComboBoxTypeEdit();
        }

        #region Methods
        void LoadComboBoxTypeEdit()
        {
            listType = SavingTypeDAO.Instance.GetListType();
            cbTypeEdit.DataSource = listType;
            cbTypeEdit.DisplayMember = "Name";
        }
        string CastIfNull(object o, TextBox txtB)
        {
            if (o == null)
            {
                txtB.ReadOnly = true;
                return "Không có thuộc tính";
            }
            else
            {
                txtB.ReadOnly = false;
                return o.ToString();
            }
        }
        bool CheckInputUpdate(string [] list, TextBox txtB1=null, TextBox txtB2=null)
        {
            float period;
            try
            {
                period = Convert.ToInt32(list[0]);
            }
            catch
            {
                MessageBox.Show("Kỳ hạn phải lớn hơn hoặc bằng 0");
                return false;
            }
            if(period==0)
            {
                if (txtB1 != null)
                {
                    txtB1.Clear();
                    txtB1.ReadOnly = true;
                }
                try
                {
                    if (Convert.ToDouble(list[2]) < 0)
                    {
                        MessageBox.Show("Lãi xuất không kỳ hạn phải lớn hơn 0");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Lãi xuất không kỳ hạn phải là một số thực");
                    return false;
                }
            }
            else if(period > 0)
            {
                if (txtB2 != null)
                {
                    txtB2.Clear();
                    txtB2.ReadOnly = true;
                }
                try
                {
                    if (Convert.ToDouble(list[1]) < 0)
                    {
                        MessageBox.Show("Lãi xuất có kỳ hạn phải lớn hơn 0");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Lãi xuất có kỳ hạn phải là một số thực");
                    return false;
                }
                try
                {
                    if (Convert.ToInt32(list[5]) < period)
                    {
                        MessageBox.Show("Ngày được thêm tiền vào sổ phải lớn hơn hoặc bằng kỳ hạn: " + period +" ngày");
                        return false;
                    }
                }
                catch
                {
                    MessageBox.Show("Số ngày được gởi thêm phải là một số nguyên");
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Kỳ hạn phải lớn hơn 0");
                return false;
            }
            try
            {
                if (Convert.ToInt32(list[3]) < 0)
                {
                    MessageBox.Show("Tiền mở sổ phải lớn hơn 0");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Số tiền mở sổ phải là một số nguyên");
                return false;
            }
            try
            {
                if (Convert.ToInt32(list[4]) < 0)
                {
                    MessageBox.Show("Tiền thêm vào sổ phải lớn hơn 0");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Số tiền gửi thêm phải là một số nguyên");
                return false;
            }
            try
            {
                if (Convert.ToInt32(list[5]) < 0)
                {
                    MessageBox.Show("Ngày được thêm tiền vào sổ phải lớn hơn 0");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Số ngày được gởi thêm phải là một số nguyên");
                return false;
            }
            try
            {
                if (Convert.ToInt32(list[6]) < 0)
                {
                    MessageBox.Show("Ngày được rút tiền phải lớn hơn 0");
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Số ngày được rút phải là một số nguyên");
                return false;
            }
            return true;
        }
        #endregion

        #region Events
        private void CbTypeEdit_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            listType = SavingTypeDAO.Instance.GetListType();
            tBPeriodEdit.Text = listType[cb.SelectedIndex].Period.ToString();
            tBRateEdit.Text = CastIfNull(listType[cb.SelectedIndex].InterestRate, tBRateEdit);
            tBRateMonthEdit.Text = CastIfNull(listType[cb.SelectedIndex].InterestRateMonth, tBRateMonthEdit);
            tBDateAdd.Text = listType[cb.SelectedIndex].DateCanAdded.ToString();
            tBDateWithdraw.Text = listType[cb.SelectedIndex].DateWithdraw.ToString();
            tBMoneyAddEdit.Text = listType[cb.SelectedIndex].MinAddMoney.ToString();
            tBMoneyEdit.Text = listType[cb.SelectedIndex].MinMoney.ToString();
            tBIDEdit.Text= listType[cb.SelectedIndex].ID.ToString();
            cBIsActive.Checked = Convert.ToBoolean(listType[cb.SelectedIndex].IsActive);
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn chắc muốn sửa loại tiết kiệm này?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (!CheckInputUpdate(new string[] { tBPeriodEdit.Text, tBRateEdit.Text, tBRateMonthEdit.Text, tBMoneyEdit.Text, tBMoneyAddEdit.Text, tBDateAdd.Text, tBDateWithdraw.Text }))
                    return;
                string isActive;
                if (cBIsActive.Checked == true)
                    isActive = "1";
                else isActive = "0";
                try
                {
                    SavingTypeDAO.Instance.UpdateSavingType(new string[] { cbTypeEdit.Text, tBPeriodEdit.Text, tBRateEdit.Text, tBRateMonthEdit.Text, tBMoneyEdit.Text, tBMoneyAddEdit.Text, tBDateAdd.Text, tBDateWithdraw.Text, isActive });
                    MessageBox.Show("Cập nhật loại tiết kiệm thành công\n");
                }
                catch
                {
                    MessageBox.Show("Cập nhật loại tiết kiệm không thành công\n Xin quý khách vui lòng thử lại");
                }
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn chắc muốn thêm loại tiết kiệm này?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (!CheckInputUpdate(new string[] { tBPeriodAdd.Text, tBRateAdd.Text, tBRateMonthAdd.Text, tBMoneyAdd.Text, tBMoneyAddAdd.Text, tBDateAddAdd.Text, tBDateWithdrawAdd.Text }, tBRateAdd, tBRateMonthAdd))
                    return;
                string isActive;
                if (cBisActiveAdd.Checked == true)
                    isActive = "1";
                else isActive = "0";
                try
                {
                    SavingTypeDAO.Instance.InsertSavingType(new string[] { cbTypeAdd.Text, tBPeriodAdd.Text, tBRateAdd.Text, tBRateMonthAdd.Text, tBMoneyAdd.Text, tBMoneyAddAdd.Text, tBDateAddAdd.Text, tBDateWithdrawAdd.Text, isActive });
                    MessageBox.Show("Thêm loại tiết kiệm thành công\n");
                    LoadComboBoxTypeEdit();
                }
                catch
                {
                    MessageBox.Show("Thêm loại tiết kiệm không thành công\n Xin quý khách vui lòng thử lại");
                }
            }
        }
        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn chắc muốn xóa loại tiết kiệm này?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (SavingTypeDAO.Instance.CheckSavingTypeInUse(listType[cbTypeEdit.SelectedIndex].ID) == 1)
                {
                    MessageBox.Show("Không thể xóa loại tiết kiệm này\n Hãy hủy kích hoạt thay vì xóa");
                    return;
                }
                try
                {
                    SavingTypeDAO.Instance.DeleteSavingtype(cbTypeEdit.Text);
                    MessageBox.Show("Xóa loại tiết kiệm thành công\n");
                    LoadComboBoxTypeEdit();
                }
                catch
                {
                    MessageBox.Show("Xóa loại tiết kiệm không thành công\n Xin quý khách vui lòng thử lại");
                }
            }
        }
        #endregion

        private void Button4_Click(object sender, EventArgs e)
        {
            cbTypeAdd.Clear();
            tBPeriodAdd.Clear();
            tBRateAdd.Clear();
            tBRateAdd.ReadOnly = false;
            tBRateMonthAdd.Clear();
            tBRateMonthAdd.ReadOnly = false;
            tBMoneyAdd.Clear();
            tBMoneyAddAdd.Clear();
            tBDateAddAdd.Clear();
            tBDateWithdrawAdd.Clear();
        }
    }
}
