using QuanLyQuanKaraoke.DAO;
using QuanLyQuanKaraoke.DTO;
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
    public partial class FNhapHang : Form
    {
        public FNhapHang()
        {
            InitializeComponent();
            LoadTenDichVu();
            LoadTenKho();
        }

        public int KiemTraDoDaiNhapVao(string ThongTinNhapVao, int GioiHanKyTu, string ONhap)
        {
            if (ThongTinNhapVao.Length > GioiHanKyTu)
            {
                MessageBox.Show(ONhap + " chi co the nhap toi da " + GioiHanKyTu + " ky tu!", "Thong bao");
                return 0;
            }
            else
            {
                return 1;
            }
        }

        void LoadTenDichVu()
        {
            List<DichVu> DanhSach = DichVuDAO.Instance1.ListDichVu();
            cbbTenDichVu.DataSource = DanhSach;
            cbbTenDichVu.DisplayMember = "TenDichVu";
        }

        void LoadTenKho()
        {
            List<Kho> DanhSach = KhoDAO.Instance1.ListKho();
            cbbTenKho.DataSource = DanhSach;
            cbbTenKho.DisplayMember = "TenKho";
        }

        private void btNhap_Click(object sender, EventArgs e)
        {
            if ((txtGiaNhap.Text == "") || (txtSoLuong.Text == ""))
            {
                MessageBox.Show("Thieu thong tin!", "Thong bao");
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(cbbTenDichVu.Text, 100, "Ten dich vu") == 0) || (KiemTraDoDaiNhapVao(cbbTenKho.Text, 100, "Ten kho") == 0))
                {
                    KiemTraNhapVao = 0;
                }
                else
                {
                    KiemTraNhapVao = 1;
                }
                if (KiemTraNhapVao == 1)
                {
                    try
                    {
                        int NhapHang = DichVuDAO.Instance1.NhapHang(cbbTenDichVu.Text, cbbTenKho.Text, Convert.ToInt32(txtGiaNhap.Text), Convert.ToInt32(txtSoLuong.Text));
                        if (NhapHang == 1)
                        {
                            MessageBox.Show("Thanh cong!", "Thong bao");
                        }
                        else
                        {
                            MessageBox.Show("So luong hoac gia nhap khong dung!", "Thong bao");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("So luong hoac gia nhap khong dung!", "Thong bao");
                    }
                }
                else { }
            }
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
