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
    public partial class fPhieuRutTien : Form
    {
        public fPhieuRutTien()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn chắc muốn rút tiền ?"), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                int accID;
                int money;
                double canWithdraw;
                try
                {
                    int temp = Convert.ToInt32(tBAccID.Text);
                    accID = WithdrawDAO.Instance.GetActiveAccount(temp);
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
                    int minMoney = WithdrawDAO.Instance.GetMin_CurrMoney(accID)[0];
                    int currMoney = WithdrawDAO.Instance.GetMin_CurrMoney(accID)[1];
                    money = Convert.ToInt32(tBMoney.Text);
                    if (money > (currMoney - minMoney))
                    {
                        MessageBox.Show("Tiền rút phải tối đa " + (currMoney - minMoney) + " VNĐ\n Vì trong sổ phải có tối thiểu " + minMoney + " VNĐ\n Nếu muốn rút tiền trên hạn mức hãy tất toán sổ");
                        return;
                    }
                    else if (money < 0)
                    {
                        MessageBox.Show("Tiền rút ra phải lớn hơn 0");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Tiền rút ra phải là một số nguyên");
                    return;
                }
                if (tBName.Text == "")
                {
                    MessageBox.Show("Tên khách hàng không được để trống");
                    return;
                }
                if (DateTime.Compare(dateWithdraw.Value.Date, DateTime.Today) > 0)
                {
                    MessageBox.Show("Thời gian rút tiền không hợp lệ");
                    return;
                }
                else
                {
                    if (WithdrawDAO.Instance.GetPeriod(accID)>0)
                    {
                        DateTime dateDue = WithdrawDAO.Instance.getDueDate(accID);
                        if (DateTime.Compare(dateDue, dateWithdraw.Value.Date)<0)
                        {
                            if (MessageBox.Show("Sổ có thời hạn chỉ có lãi khi tất toán sau ngày: " + dateDue.ToShortDateString() + "\nBạn có chắc muốn rút tiền thay vì tất toán ?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                                return;
                        }
                    }
                }
                canWithdraw = WithdrawDAO.Instance.InterestMoney_CanWithdraw(accID, dateWithdraw.Value)[1];
                if (canWithdraw == 1)
                {
                    try
                    {
                        WithdrawDAO.Instance.InsertWithdraw(new string[] { tBAccID.Text, tBName.Text, tBMoney.Text }, dateWithdraw.Value, 0);
                        MessageBox.Show("Rút tiền thành công");
                        tBID.Text = WithdrawDAO.Instance.GetMaxID().ToString();
                    }
                    catch
                    {
                        MessageBox.Show("Rút tiền không thành công\n Xin quý khách vui lòng thử lại");
                    }
                }
                else
                {
                    MessageBox.Show("Không được rút tiền trước hạn");
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            tBAccID.Clear();
            tBID.Clear();
            tBMoney.Clear();
            tBName.Clear();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            int accID;
            int money;
            double interestMoney;
            double canWithdraw;
            try
            {
                int temp = Convert.ToInt32(tBAccID.Text);
                accID = WithdrawDAO.Instance.GetActiveAccount(temp);
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
                money = WithdrawDAO.Instance.GetMin_CurrMoney(accID)[1];
            }
            catch
            {

                MessageBox.Show("Mã sổ tiết kiệm phải là một số nguyên");
                return;
            }
            if (tBName.Text == "")
            {
                MessageBox.Show("Tên người rút không được để trống");
                return;
            }
            if (DateTime.Compare(dateWithdraw.Value.Date, DateTime.Today) > 0)
            {
                MessageBox.Show("Thời gian tất toán không hợp lệ");
                return;
            }
            List<double> tempList = WithdrawDAO.Instance.InterestMoney_CanWithdraw(accID, dateWithdraw.Value);
            interestMoney = tempList[0];
            canWithdraw = tempList[1];
            if (canWithdraw == 1 )
            {
                if (MessageBox.Show(string.Format("Nếu tất toán bạn sẽ rút {0} VNĐ và lãi {1} VNĐ\nBạn có chắc muốn tất toán ?", money, interestMoney), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        WithdrawDAO.Instance.InsertWithdraw(new string[] { tBAccID.Text, tBName.Text, money.ToString() }, dateWithdraw.Value, 1);
                        tBMoney.Text = money.ToString();
                        if (WithdrawDAO.Instance.CloseAccountByID(accID))
                            MessageBox.Show("Đóng sổ thành công\n Tất toán thành công");
                        tBID.Text = WithdrawDAO.Instance.GetMaxID().ToString();
                    }
                    catch
                    {
                        MessageBox.Show("Tất toán không thành công\n Xin quý khách vui lòng thử lại");
                    }
                }
            }
            else
            {
                MessageBox.Show("Không được tất toán trước hạn");
            }
        }
    }
}
