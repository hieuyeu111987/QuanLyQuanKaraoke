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
    public partial class FNhanVien : Form
    {

        BindingSource DanhSachPhongThanhToan = new BindingSource();
        BindingSource DanhSachPhong = new BindingSource();
        BindingSource DanhSachDichVu = new BindingSource();
        BindingSource DanhSachDichVuThanhToan = new BindingSource();

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

        public FNhanVien()
        {
            InitializeComponent();
            LoadDichVu();
            LoadPhong();
            LoadThanhToan();
        }



        #region ThanhToan

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LoadThanhToan()
        {
            LoadDanhSachPhongThanhToan();
            BindingDanhSachPhongThanhToan();
        }

        public void LoadDanhSachPhongThanhToan()
        {
            DanhSachPhongThanhToan.DataSource = PhongDAO.Instance1.DanhSachThanhToanPhong();
            dtgvDanhSachPhongThanhToan.DataSource = DanhSachPhongThanhToan;
        }

        public void BindingDanhSachPhongThanhToan()
        {
            txtTenPhongThanhToan.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongThanhToan.DataSource, "Ten phong", true, DataSourceUpdateMode.Never));
            txtGiaPhongThanhToan.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongThanhToan.DataSource, "Gia phong", true, DataSourceUpdateMode.Never));
        }

        public void LoadDanhSachDichVuThanhToan()
        {
            DanhSachDichVuThanhToan.DataSource = DichVuDAO.Instance1.DanhSachHoaDonDichVu(txtTenPhongThanhToan.Text);
            dtgvDanhSachDichVuThanhToan.DataSource = DanhSachDichVuThanhToan;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Even///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void dtgvDanhSachPhongThanhToan_Click(object sender, EventArgs e)
        {
            if (txtTenPhongThanhToan.Text != "")
            {
                LoadDanhSachDichVuThanhToan();
                txtThoiGianVaoThanhToan.Text = PhongDAO.Instance1.ThoiGianVao(txtTenPhongThanhToan.Text);
                txtThoiGianRaThanhToan.Text = PhongDAO.Instance1.ThoiGianRa();
                txtThoiGianSuDungThanhToan.Text = (PhongDAO.Instance1.ThoiGianSuDung(txtTenPhongThanhToan.Text) / 60).ToString() + "h" + (PhongDAO.Instance1.ThoiGianSuDung(txtTenPhongThanhToan.Text) % 60).ToString();
                int TienPhong = PhongDAO.Instance1.ThoiGianSuDung(txtTenPhongThanhToan.Text) * (Convert.ToInt32(txtGiaPhongThanhToan.Text) / 60);
                int TienDichVu = DichVuDAO.Instance1.TongTienDichVu(txtTenPhongThanhToan.Text);
                txtTienPhongThanhToan.Text = (TienPhong).ToString();
                txtTienDichVuThanhToan.Text = (TienDichVu).ToString();
                txtTongTienThanhToan.Text = (((TienPhong + TienDichVu)/1000)*1000).ToString();
            }
            else { }
        }

        private void btThanhToanThanhToan_Click(object sender, EventArgs e)
        {
            if (txtTenPhongThanhToan.Text == "")
            {
                MessageBox.Show("Thieu thong tin!", "Thong bao");
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenPhongThanhToan.Text, 100, "Ten phong") == 0))
                {
                    KiemTraNhapVao = 0;
                }
                else
                {
                    KiemTraNhapVao = 1;
                }
                if (KiemTraNhapVao == 1)
                {
                    int ThanhToan = PhongDAO.Instance1.ThanhToanPhong(txtTenPhongThanhToan.Text);
                    if (ThanhToan == 0)
                    {
                        MessageBox.Show("Thong tin sai!", "Thong bao");
                    }
                    else
                    {
                        MessageBox.Show("Thanh cong!", "Thong bao");
                        LoadDanhSachPhongThanhToan();
                        LoadDanhSachPhong();
                        txtThoiGianVaoThanhToan.Clear();
                        txtThoiGianRaThanhToan.Clear();
                        txtThoiGianSuDungThanhToan.Clear();
                        txtTienPhongThanhToan.Clear();
                        txtTienDichVuThanhToan.Clear();
                        txtTongTienThanhToan.Clear();
                    }
                }
                else { }
            }
        }

        #endregion





        #region Phong

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LoadPhong()
        {
            LoadDanhSachPhong();
            BindingDanhSachPhong();
        }

        public void LoadDanhSachPhong()
        {
            DanhSachPhong.DataSource = PhongDAO.Instance1.DanhSachDatPhong();
            dtgvDanhSachPhongPhong.DataSource = DanhSachPhong;
        }

        public void BindingDanhSachPhong()
        {
            txtTenPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "Ten phong", true, DataSourceUpdateMode.Never));
            txtSoNguoiToiThieuPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "So nguoi toi thieu", true, DataSourceUpdateMode.Never));
            txtSoNguoiToiDaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "So nguoi toi da", true, DataSourceUpdateMode.Never));
            txtGiaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "Gia phong", true, DataSourceUpdateMode.Never));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void txtTimPhong_Click(object sender, EventArgs e)
        {
            DanhSachPhong.DataSource = PhongDAO.Instance1.TimDanhSachDatPhong(txtTenPhong.Text);
        }

        private void btDatPhongPhong_Click(object sender, EventArgs e)
        {
            if (txtTenPhong.Text == "")
            {
                MessageBox.Show("Khong du thong tin", "Thong bao");
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenPhong.Text, 100, "Ten phong") == 0))
                {
                    KiemTraNhapVao = 0;
                }
                else
                {
                    KiemTraNhapVao = 1;
                }
                if (KiemTraNhapVao == 1)
                {
                    int DatPhong = PhongDAO.Instance1.DatPhong(txtTenPhong.Text);
                    if (DatPhong == 0)
                    {
                        MessageBox.Show("Phong khong ton tai!", "Thong bao");
                    }
                    else
                    {
                        MessageBox.Show("Dat phong thanh cong!", "Thong bao");
                        LoadDanhSachPhong();
                        LoadDanhSachPhongThanhToan();
                        LoadCBBTenPhongDichVu();
                    }
                }
                else { }
            }
        }

        #endregion





        #region DichVu

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        void LoadDichVu()
        {
            LoadCBBTenPhongDichVu();
            LoadCBBDichVuDichVu();
            LoadCBBTenKhoDichVu();
        }

        public void LoadDanhSachDichVu()
        {
            DanhSachDichVu.DataSource = DichVuDAO.Instance1.DanhSachThemDichVu(cbbTenPhongDichVu.Text);
            dtgvDanhSachDichVuDichVu.DataSource = DanhSachDichVu;
            txtTonKhoDichVu.Text = (KhoDAO.Instance1.SoLuongDichVuTrongKho(cbbKhoDichVu.Text, cbbTenDichVuDichVu.Text)).ToString();
        }

        void LoadCBBTenPhongDichVu()
        {
            List<Phong> DanhSach = PhongDAO.Instance1.ListPhong();
            cbbTenPhongDichVu.DataSource = DanhSach;
            cbbTenPhongDichVu.DisplayMember = "TenPhong";
        }

        void LoadCBBDichVuDichVu()
        {
            List<DichVu> DanhSach = DichVuDAO.Instance1.ListDichVu();
            cbbTenDichVuDichVu.DataSource = DanhSach;
            cbbTenDichVuDichVu.DisplayMember = "TenDichVu";
        }

        void LoadCBBTenKhoDichVu()
        {
            List<Kho> DanhSach = KhoDAO.Instance1.ListKho();
            cbbKhoDichVu.DataSource = DanhSach;
            cbbKhoDichVu.DisplayMember = "TenKho";
        }



        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Evev////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btNhapHangDichVu_Click(object sender, EventArgs e)
        {
            FNhapHang NhapHang = new FNhapHang();
            this.Hide();
            NhapHang.ShowDialog();
            this.Show();
        }

        private void cbbTenDichVuDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtGiaDichVu.Text = DichVuDAO.Instance1.GiaDichVu(cbbTenDichVuDichVu.Text).ToString();
            txtTonKhoDichVu.Text = (KhoDAO.Instance1.SoLuongDichVuTrongKho(cbbKhoDichVu.Text, cbbTenDichVuDichVu.Text)).ToString();
        }

        private void cbbKhoDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtTonKhoDichVu.Text = (KhoDAO.Instance1.SoLuongDichVuTrongKho(cbbKhoDichVu.Text, cbbTenDichVuDichVu.Text)).ToString();
        }

        private void cbbTenPhongDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbTenPhongDichVu.Text != "")
            {
                LoadDanhSachDichVu();
            }
            else { }
        }

        private void btThemDichVu_Click(object sender, EventArgs e)
        {
            if ((cbbTenPhongDichVu.Text == "") || (cbbTenDichVuDichVu.Text == "") || (txtSoLuongThemDichVu.Text == ""))
            {
                MessageBox.Show("Thieu thong tin!", "Thong bao");
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(cbbTenPhongDichVu.Text, 100, "Ten phong") == 0) || (KiemTraDoDaiNhapVao(cbbTenDichVuDichVu.Text, 100, "Ten dich vu") == 0))
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
                        if (Convert.ToInt32(txtSoLuongThemDichVu.Text) > Convert.ToInt32(txtTonKhoDichVu.Text))
                        {
                            MessageBox.Show("So luong khong du!", "Thong bao");
                        }
                        else
                        {
                            int ThemDichVu = DichVuDAO.Instance1.ThemDichVuChoPhong(cbbTenPhongDichVu.Text, cbbTenDichVuDichVu.Text, Convert.ToInt32(txtSoLuongThemDichVu.Text), cbbKhoDichVu.Text);
                            if (ThemDichVu == 0)
                            {
                                MessageBox.Show("Thong tin sai!", "Thong bao");
                            }
                            else
                            {
                                MessageBox.Show("Them thanh cong!", "Thong bao");
                                LoadDanhSachDichVu();
                            }
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Thong tin khong dung!", "Thong bao");
                    }
                }
                else { }
            }
        }

        private void btTraDichVu_Click(object sender, EventArgs e)
        {
            if ((cbbTenPhongDichVu.Text == "") || (cbbTenDichVuDichVu.Text == "") || (txtSoLuongTraDichVu.Text == ""))
            {
                MessageBox.Show("Thieu thong tin!", "Thong bao");
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(cbbTenPhongDichVu.Text, 100, "Ten phong") == 0) || (KiemTraDoDaiNhapVao(cbbTenDichVuDichVu.Text, 100, "Ten dich vu") == 0))
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
                        int TraDichVu = DichVuDAO.Instance1.TraDichVuChoPhong(cbbTenPhongDichVu.Text, cbbTenDichVuDichVu.Text, Convert.ToInt32(txtSoLuongTraDichVu.Text));
                        if (TraDichVu == 0)
                        {
                            MessageBox.Show("Thong tin sai!", "Thong bao");
                        }
                        else if (TraDichVu == 2)
                        {
                            MessageBox.Show("So luong tra khong dung!", "Thong bao");
                        }
                        else
                        {
                            MessageBox.Show("Thanh cong!", "Thong bao");
                            LoadDanhSachDichVu();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Thong tin khong dung!", "Thong bao");
                    }
                }
                else { }
            }
        }

        #endregion



        

        #region CaNhan

        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void doiMatKhauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FDoiMatKhau DoiMatKhau = new FDoiMatKhau();
            this.Hide();
            DoiMatKhau.ShowDialog();
            this.Show();
        }

        #endregion

        

        

        

        

        

        

        

        

        

        
    }
}
