using QuanLyQuanKaraoke.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanKaraoke
{
    public partial class FDoiMatKhau : Form
    {
        public FDoiMatKhau()
        {
            InitializeComponent();
            LoadThongTin();
        }

        void LoadThongTin()
        {
            txtTenNguoiDung.Text = CaNhanDAO.Instance1.GetTenNguoiDung();
            txtTenDangNhap.Text = CaNhanDAO.Instance1.GetTenTaiKhoan();
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDoiMatKhau_Click(object sender, EventArgs e)
        {
            if ((txtMatKhauMoi.Text == "") || (txtNhapLaiMatKhauMoi.Text == ""))
            {
                MessageBox.Show("Thieu thong tin!", "Thong bao");
            }
            else
            {
                int DoiMatKhau = CaNhanDAO.Instance1.DoiMatKhau(txtMatKhauMoi.Text, txtNhapLaiMatKhauMoi.Text);
                if (DoiMatKhau == 1)
                {
                    MessageBox.Show("Mat khau nhap lai khong dung!", "Thong bao");
                }
                else
                {
                    MessageBox.Show("Doi mat khau thanh cong!", "Thong bao");
                }
            }
        }
    }
}
