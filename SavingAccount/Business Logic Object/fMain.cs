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
    public partial class fMain : Form
    {
        public fMain()
        {
            InitializeComponent();

        }
        #region Methods

        #endregion

        #region Events
        private void ToolStripButton1_Click(object sender, EventArgs e)
        {
            fMoSoTietKiem f = new fMoSoTietKiem();
            f.ShowDialog();
        }
        private void ToolStripButton2_Click(object sender, EventArgs e)
        {
            fPhieuGuiTien f = new fPhieuGuiTien();
            f.ShowDialog();
        }
        private void ToolStripButton3_Click(object sender, EventArgs e)
        {
            fPhieuRutTien f = new fPhieuRutTien();
            f.ShowDialog();
        }
        private void ToolStripButton4_Click(object sender, EventArgs e)
        {
            fDanhSachSo f = new fDanhSachSo();
            f.ShowDialog();
        }
        private void BáoCáoNgàyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBaoCaoNgay f = new fBaoCaoNgay();
            f.ShowDialog();
        }
        private void BáoCáoThángToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fBaoCaoThang f = new fBaoCaoThang();
            f.ShowDialog();
        }
        private void ToolStripButton5_Click(object sender, EventArgs e)
        {
            fQuanLy f = new fQuanLy();
            f.ShowDialog();
        }


        #endregion

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }
    }
}
