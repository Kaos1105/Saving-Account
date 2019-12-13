using SavingAccount.Data_Access_Object;
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
    public partial class fPhieuGuiTien : Form
    {
        public fPhieuGuiTien()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn chắc muốn gửi tiền ?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int accID;
                int money;
                try
                {
                    int temp = Convert.ToInt32(tbAccID.Text);
                    accID = DepositDAO.Instance.GetActiveAccount(temp);
                    if (accID == -1)
                    {
                        MessageBox.Show("Mã sổ tiết kiệm không tồn tại");
                        return;
                    }
                    if (accID == -2)
                    {
                        MessageBox.Show("Sổ tiết kiệm đã hết hạn hoặc đã khóa");
                        return;
                    }
                }
                catch
                {

                    MessageBox.Show("Mã sổ tiết kiệm phải là một số nguyên");
                    return;
                }
                try
                {
                    int minMoney = DepositDAO.Instance.GetMinMoney(accID);
                    money = Convert.ToInt32(tBMoney.Text);
                    if (money < minMoney)
                    {
                        MessageBox.Show("Tiền gửi thêm phải tối thiểu" + minMoney + " VNĐ");
                        return;
                    }
                    else if (money < 0)
                    {
                        MessageBox.Show("Tiền gửi thêm phải lớn hơn 0");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Tiền gửi thêm phải là một số nguyên");
                    return;
                }
                if (tBName.Text == "")
                {
                    MessageBox.Show("Tên người gửi không được để trống");
                    return;
                }
                if (DateTime.Compare(dateAdd.Value.Date, DateTime.Today) < 0)
                {
                    MessageBox.Show("Thời gian gửi tiền không hợp lệ");
                    return;
                }
                else 
                {
                    
                    try
                    {
                        DateTime minDateAdd = DepositDAO.Instance.GetDateAdd(accID);
                        if (DateTime.Compare(dateAdd.Value.Date, minDateAdd.Date) <0)
                        {
                            MessageBox.Show("Chỉ được gửi thêm tiền sau ngày: " + minDateAdd.ToShortDateString());
                            return;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Tải ngày không thành công");
                        return;
                    }

                }
                try
                {
                    DepositDAO.Instance.InsertDeposit(new string[] { tbAccID.Text, tBName.Text, tBMoney.Text }, dateAdd.Value);
                    MessageBox.Show("Gửi thêm tiền thành công");
                    tBID.Text = DepositDAO.Instance.GetMaxID().ToString();
                }
                catch
                {
                    MessageBox.Show("Gửi tiền không thành công\n Xin quý khách vui lòng thử lại");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tbAccID.Clear();
            tBID.Clear();
            tBMoney.Clear();
            tBName.Clear();
        }
    }
}
