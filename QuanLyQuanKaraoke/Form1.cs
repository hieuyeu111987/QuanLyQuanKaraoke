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
    public partial class FDangNhap : Form
    {
        public FDangNhap()
        {
            InitializeComponent();
        }

        private void btHienThi_Click(object sender, EventArgs e)
        {
            if (txtMatKhau.UseSystemPasswordChar == true)
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            if ((txtTaiKhoan.Text == "") || (txtMatKhau.Text == ""))
            {
                MessageBox.Show("Thong tin trong!", "Thong bao");
            }
            else
            {
                string ChucVu = (CaNhanDAO.Instance1.DangNhap(txtTaiKhoan.Text, txtMatKhau.Text)).ToString();
                if (ChucVu == "Quan ly")
                {
                    FQuanLy QuanLy = new FQuanLy();
                    this.Hide();
                    QuanLy.ShowDialog();
                    this.Show();
                }
                else if (ChucVu == "Nhan Vien")
                {
                    FNhanVien NhanVien = new FNhanVien();
                    this.Hide();
                    NhanVien.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai ten tai khoan hoac mat khau!", "Thong bao");
                }
            }
        }
    }
}
