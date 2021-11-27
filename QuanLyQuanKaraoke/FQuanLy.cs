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
    public partial class FQuanLy : Form
    {

        #region Bien

        BindingSource DanhSachNhanVien = new BindingSource();
        BindingSource DanhSachPhong = new BindingSource();
        BindingSource DanhSachDichVu = new BindingSource();
        BindingSource DanhSachKho = new BindingSource();
        BindingSource DanhSachGiaPhong = new BindingSource();
        BindingSource DanhSachGiaDichVu = new BindingSource();
        BindingSource DanhSachHoaDonThu = new BindingSource();
        BindingSource DanhSachHoaDonChi = new BindingSource();

        string ThongBao = "Thong bao!";
        string KhongDuThongTin = "Khong du thong tin!";
        string ThongTinNhapVaoKhongDung = "Thong tin nhap vao khong dung!";
        string ThongTinTrungLap = "Thong tin trung lap!";
        string ThemThanhCong = "Them thanh cong!";
        string SuaThanhCong = "Sua thanh cong!";
        string XoaThanhCong = "Xoa thanh cong!";
        string ThongTinCanXoaKhongTonTai = "Thong tin can xoa khong ton tai";
        string ThaoTacNaySeXoaTatCaCacThongTinLienQuan = "Thao tac nay se xoa tat ca cac thong tin lien quan!";

        public int KiemTraDoDaiNhapVao(string ThongTinNhapVao, int GioiHanKyTu, string ONhap)
        {
            if (ThongTinNhapVao.Length > GioiHanKyTu)
            {
                MessageBox.Show(ONhap + " chi co the nhap toi da " + GioiHanKyTu + " ky tu!", ThongBao);
                return 0;
            }
            else 
            {
                return 1;
            }
        }

        #endregion

        public FQuanLy()
        {
            InitializeComponent();
            LoadNhanVien();
            LoadPhong();
            LoadDichVu();
            LoadKho();
            LoadGiaPhong();
            LoadGiaDichVu();
            LoadThu();
            LoadChi();
        }

        #region NhanVien

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadNhanVien()
        {
            LoadDanhSachNhanVien();
            BindingDanhSachNhanVien();
        }

        public void LoadDanhSachNhanVien()
        {
            DanhSachNhanVien.DataSource = NhanVienDAO.Instance1.DanhSachNhanVien();
            dtgvDanhSachNhanVienNhanVien.DataSource = DanhSachNhanVien;
        }

        public void BindingDanhSachNhanVien()
        {
            txtTenNhanVienNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "Ten nhan vien", true, DataSourceUpdateMode.Never));
            txtCMNDNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "CMND", true, DataSourceUpdateMode.Never));
            txtSDTNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "SDT", true, DataSourceUpdateMode.Never));
            txtChucVuNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "Chuc vu", true, DataSourceUpdateMode.Never));
            txtNamSinhNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "Nam sinh", true, DataSourceUpdateMode.Never));
            txtLuongNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "Luong", true, DataSourceUpdateMode.Never));
            dtpNgayVaoLamNhanVien.DataBindings.Add(new Binding("Text", dtgvDanhSachNhanVienNhanVien.DataSource, "Ngay vao lam", true, DataSourceUpdateMode.Never));
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Even///////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btThemNhanVien_Click(object sender, EventArgs e)
        {
            if ((txtTenNhanVienNhanVien.Text == "") || (txtCMNDNhanVien.Text == "") || (txtSDTNhanVien.Text == "") || (txtChucVuNhanVien.Text == "") || (txtNamSinhNhanVien.Text == "") || (txtLuongNhanVien.Text == "") || (txtTaiKhoanNhanVien.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenNhanVienNhanVien.Text, 100, "Ten nhan vien") == 0) || (KiemTraDoDaiNhapVao(txtCMNDNhanVien.Text, 100, "CMND") == 0) || (KiemTraDoDaiNhapVao(txtSDTNhanVien.Text, 11, "SDT") == 0) || (KiemTraDoDaiNhapVao(txtChucVuNhanVien.Text, 100, "Chuc vu") == 0) || (KiemTraDoDaiNhapVao(txtTaiKhoanNhanVien.Text, 100, "Tai khoan") == 0))
                {
                    KiemTraNhapVao = 0;
                }
                else
                {
                    KiemTraNhapVao = 1;
                }
                if (KiemTraNhapVao == 1)
                {
                    string NgayVaoLam = (CaNhanDAO.Instance1.ChuoiNgay((dtpNgayVaoLamNhanVien.Value.Day).ToString(), (dtpNgayVaoLamNhanVien.Value.Month).ToString(), (dtpNgayVaoLamNhanVien.Value.Year).ToString())).ToString();
                    try
                    {
                        int ThemNhanVien = NhanVienDAO.Instance1.ThemNhanVien(txtTenNhanVienNhanVien.Text, txtCMNDNhanVien.Text, txtChucVuNhanVien.Text, txtSDTNhanVien.Text, Convert.ToInt32(txtNamSinhNhanVien.Text), NgayVaoLam, Convert.ToInt32(txtLuongNhanVien.Text), txtTaiKhoanNhanVien.Text);
                        if (ThemNhanVien == 1)
                        {
                            MessageBox.Show(ThemThanhCong, ThongBao);
                            LoadDanhSachNhanVien();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinTrungLap, ThongBao);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btNhanVienNhanVien_Click(object sender, EventArgs e)
        {
            FNhanVien NhanVien = new FNhanVien();
            this.Hide();
            NhanVien.ShowDialog();
            this.Show();
            LoadDanhSachKho();
        }

        private void btTimNhanVien_Click(object sender, EventArgs e)
        {
            DanhSachNhanVien.DataSource = NhanVienDAO.Instance1.TimNhanVien(txtTenNhanVienNhanVien.Text);
        }

        private void dtgvDanhSachNhanVienNhanVien_Click(object sender, EventArgs e)
        {
            if (txtCMNDNhanVien.Text == "")
            {
                txtTaiKhoanNhanVien.Text = "";
            }
            else
            {
                txtTaiKhoanNhanVien.Text = NhanVienDAO.Instance1.BindingTaiKhoan(txtCMNDNhanVien.Text);
            }
        }

        private void doiMatKhauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FDoiMatKhau DoiMatKhau = new FDoiMatKhau();
            this.Hide();
            DoiMatKhau.ShowDialog();
            this.Show();
        }

        private void dangXuatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btThoatNhanVien_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btResetMatKhauNhanVien_Click(object sender, EventArgs e)
        {
            int ResetMatKhau = NhanVienDAO.Instance1.ResetMatKhau(txtCMNDNhanVien.Text);
            if (ResetMatKhau == 0)
            {
                MessageBox.Show("Thanh cong!", ThongBao);
            }
            else
            {
                MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
            }
        }

        private void btSuaNhanVien_Click(object sender, EventArgs e)
        {
            if ((txtTenNhanVienNhanVien.Text == "") || (txtCMNDNhanVien.Text == "") || (txtSDTNhanVien.Text == "") || (txtChucVuNhanVien.Text == "") || (txtNamSinhNhanVien.Text == "") || (txtLuongNhanVien.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenNhanVienNhanVien.Text, 100, "Ten nhan vien") == 0) || (KiemTraDoDaiNhapVao(txtCMNDNhanVien.Text, 100, "CMND") == 0) || (KiemTraDoDaiNhapVao(txtSDTNhanVien.Text, 11, "SDT") == 0) || (KiemTraDoDaiNhapVao(txtChucVuNhanVien.Text, 100, "Chuc vu") == 0))
                {
                    KiemTraNhapVao = 0;
                }
                else
                {
                    KiemTraNhapVao = 1;
                }
                if (KiemTraNhapVao == 1)
                {
                    string NgayVaoLam = (CaNhanDAO.Instance1.ChuoiNgay((dtpNgayVaoLamNhanVien.Value.Day).ToString(), (dtpNgayVaoLamNhanVien.Value.Month).ToString(), (dtpNgayVaoLamNhanVien.Value.Year).ToString())).ToString();
                    try
                    {
                        int SuaNhanVien = NhanVienDAO.Instance1.SuaThongTinNhanVien(txtTenNhanVienNhanVien.Text, txtCMNDNhanVien.Text, txtChucVuNhanVien.Text, txtSDTNhanVien.Text, Convert.ToInt32(txtNamSinhNhanVien.Text), NgayVaoLam, Convert.ToInt32(txtLuongNhanVien.Text));
                        if (SuaNhanVien == 1)
                        {
                            MessageBox.Show(SuaThanhCong, ThongBao);
                            LoadDanhSachNhanVien();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinTrungLap, ThongBao);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btXoaNhanVien_Click(object sender, EventArgs e)
        {
            if (txtCMNDNhanVien.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    int KiemTraCMND = Convert.ToInt32(txtCMNDNhanVien.Text);
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaNhanVien = NhanVienDAO.Instance1.XoaNhanVien(txtCMNDNhanVien.Text);
                        if (XoaNhanVien == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachNhanVien();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion

        



        #region Phong

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadPhong()
        {
            LoadDanhSachPhong();
            BindingDanhSachPhong();
        }

        public void LoadDanhSachPhong()
        {
            DanhSachPhong.DataSource = PhongDAO.Instance1.DanhSachPhong();
            dtgvDanhSachPhongPhong.DataSource = DanhSachPhong;
        }

        public void BindingDanhSachPhong()
        {
            txtTenPhongPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "Ten phong", true, DataSourceUpdateMode.Never));
            txtSoNguoiToiThieuPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "So nguoi toi thieu", true, DataSourceUpdateMode.Never));
            txtSoNguoiToiDaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "So nguoi toi da", true, DataSourceUpdateMode.Never));
            txtGiaPhongPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachPhongPhong.DataSource, "Gia phong", true, DataSourceUpdateMode.Never));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btThemPhong_Click(object sender, EventArgs e)
        {
            if ((txtTenPhongPhong.Text == "") || (txtSoNguoiToiThieuPhong.Text == "") || (txtSoNguoiToiDaPhong.Text == "") || (txtGiaPhongPhong.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenPhongPhong.Text, 100, "Ten phong") == 0))
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
                        int ThemPhong = PhongDAO.Instance1.ThemPhong(txtTenPhongPhong.Text, Convert.ToInt32(txtSoNguoiToiThieuPhong.Text), Convert.ToInt32(txtSoNguoiToiDaPhong.Text), Convert.ToInt32(txtGiaPhongPhong.Text));
                        if (ThemPhong == 1)
                        {
                            MessageBox.Show(ThemThanhCong, ThongBao);
                            LoadDanhSachPhong();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinTrungLap, ThongBao);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btSuaPhong_Click(object sender, EventArgs e)
        {
            if ((txtTenPhongPhong.Text == "") || (txtSoNguoiToiThieuPhong.Text == "") || (txtSoNguoiToiDaPhong.Text == "") || (txtGiaPhongPhong.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenPhongPhong.Text, 100, "Ten nhan vien") == 0))
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
                        PhongDAO.Instance1.SuaPhong(txtTenPhongPhong.Text, Convert.ToInt32(txtSoNguoiToiThieuPhong.Text), Convert.ToInt32(txtSoNguoiToiDaPhong.Text), Convert.ToInt32(txtGiaPhongPhong.Text));
                        MessageBox.Show(SuaThanhCong, ThongBao);
                        LoadDanhSachPhong();
                        LoadDanhSachGiaPhong();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btXoaPhong_Click(object sender, EventArgs e)
        {
            if (txtTenPhongPhong.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaPhong = PhongDAO.Instance1.XoaPhong(txtTenPhongPhong.Text);
                        if (XoaPhong == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachPhong();
                            LoadDanhSachGiaPhong();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion

        



        #region DichVu

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadDichVu()
        {
            LoadDanhSachDichVu();
            BindingDanhSachDichVu();
        }

        public void LoadDanhSachDichVu()
        {
            DanhSachDichVu.DataSource = DichVuDAO.Instance1.DanhSachDichVu();
            dtgvDanhSachDichVuDichVu.DataSource = DanhSachDichVu;
        }

        public void BindingDanhSachDichVu()
        {
            txtTenDichVuDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachDichVuDichVu.DataSource, "Ten dich vu", true, DataSourceUpdateMode.Never));
            txtGiaDichVuDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachDichVuDichVu.DataSource, "Gia dich vu", true, DataSourceUpdateMode.Never));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btThemDichVu_Click(object sender, EventArgs e)
        {
            if ((txtTenDichVuDichVu.Text == "") || (txtGiaDichVuDichVu.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenDichVuDichVu.Text, 100, "Ten dich vu") == 0))
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
                        int ThemDichVu = DichVuDAO.Instance1.ThemDichVu(txtTenDichVuDichVu.Text, Convert.ToInt32(txtGiaDichVuDichVu.Text));
                        if (ThemDichVu == 1)
                        {
                            MessageBox.Show(ThemThanhCong, ThongBao);
                            LoadDanhSachDichVu();
                            LoadDanhSachGiaDichVu();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinTrungLap, ThongBao);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btSuaDichVu_Click(object sender, EventArgs e)
        {
            if ((txtTenDichVuDichVu.Text == "") || (txtGiaDichVuDichVu.Text == ""))
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenDichVuDichVu.Text, 100, "Ten nhan vien") == 0))
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
                        DichVuDAO.Instance1.SuaDichVu(txtTenDichVuDichVu.Text, Convert.ToInt32(txtGiaDichVuDichVu.Text));
                        MessageBox.Show(SuaThanhCong, ThongBao);
                        LoadDanhSachDichVu();
                        LoadDanhSachGiaDichVu();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btXoaDichVu_Click(object sender, EventArgs e)
        {
            if (txtTenDichVuDichVu.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaDichVu = DichVuDAO.Instance1.XoaDichVu(txtTenDichVuDichVu.Text);
                        if (XoaDichVu == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachDichVu();
                            LoadDanhSachGiaDichVu();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion





        #region Kho

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public void LoadKho()
        {
            LoadDanhSachKho();
            BindingDanhSachKho();
        }

        public void LoadDanhSachKho()
        {
            DanhSachKho.DataSource = KhoDAO.Instance1.DanhSachKho();
            dtgvDanhSachTrongKhoKho.DataSource = DanhSachKho;
        }

        public void BindingDanhSachKho()
        {
            txtTenKhoKho.DataBindings.Add(new Binding("Text", dtgvDanhSachTrongKhoKho.DataSource, "Ten kho", true, DataSourceUpdateMode.Never));
            txtSoLuongKho.DataBindings.Add(new Binding("Text", dtgvDanhSachTrongKhoKho.DataSource, "So luong trong kho", true, DataSourceUpdateMode.Never));
        }

        

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void btThemKho_Click(object sender, EventArgs e)
        {
            if (txtTenKhoKho.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                int KiemTraNhapVao = 1;
                if ((KiemTraDoDaiNhapVao(txtTenKhoKho.Text, 100, "Ten nhan vien") == 0))
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
                        int ThemKho = KhoDAO.Instance1.ThemKho(txtTenKhoKho.Text);
                        if (ThemKho == 1)
                        {
                            MessageBox.Show(ThemThanhCong, ThongBao);
                            LoadDanhSachKho();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinTrungLap, ThongBao);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                    }
                }
                else { }
            }
        }

        private void btXoaKho_Click(object sender, EventArgs e)
        {
            if (txtTenKhoKho.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaKho = KhoDAO.Instance1.XoaKho(txtTenKhoKho.Text);
                        if (XoaKho == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachKho();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion





        #region Gia

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// Gia phong

        public void LoadGiaPhong()
        {
            LoadDanhSachGiaPhong();
            BindingDanhSachGiaPhong();
        }

        public void LoadDanhSachGiaPhong()
        {
            DanhSachGiaPhong.DataSource = PhongDAO.Instance1.DanhSachGiaPhong();
            dtgvDanhSachGiaPhongGiaPhong.DataSource = DanhSachGiaPhong;
        }

        public void BindingDanhSachGiaPhong()
        {
            txtIDGiaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaPhongGiaPhong.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenGiaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaPhongGiaPhong.DataSource, "Ten phong", true, DataSourceUpdateMode.Never));
            txtGiaGiaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaPhongGiaPhong.DataSource, "Gia phong", true, DataSourceUpdateMode.Never));
            dtpNgayApDungGiaPhong.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaPhongGiaPhong.DataSource, "Ngay ap dung", true, DataSourceUpdateMode.Never));
        }

        /// Gia dich vu

        public void LoadGiaDichVu()
        {
            LoadDanhSachGiaDichVu();
            BindingDanhSachGiaDichVu();
        }

        public void LoadDanhSachGiaDichVu()
        {
            DanhSachGiaDichVu.DataSource = DichVuDAO.Instance1.DanhSachGiaDichVu();
            dtgvDanhSachGiaDichVuDichVu.DataSource = DanhSachGiaDichVu;
        }

        public void BindingDanhSachGiaDichVu()
        {
            txtIDGiaDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaDichVuDichVu.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenGiaDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaDichVuDichVu.DataSource, "Ten dich vu", true, DataSourceUpdateMode.Never));
            txtGiaGiaDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaDichVuDichVu.DataSource, "Gia dich vu", true, DataSourceUpdateMode.Never));
            dtpNgayApDungGiaDichVu.DataBindings.Add(new Binding("Text", dtgvDanhSachGiaDichVuDichVu.DataSource, "Ngay ap dung", true, DataSourceUpdateMode.Never));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // Gia phong

        private void btTimGiaPhong_Click(object sender, EventArgs e)
        {
            DanhSachGiaPhong.DataSource = PhongDAO.Instance1.TimGiaPhong(txtTenGiaPhong.Text);
        }

        private void btXoaGiaPhong_Click(object sender, EventArgs e)
        {
            if (txtIDGiaPhong.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    int XoaGiaPhong = PhongDAO.Instance1.XoaGiaPhong(Convert.ToInt32(txtIDGiaPhong.Text), txtTenGiaPhong.Text);
                    if (XoaGiaPhong == 1)
                    {
                        MessageBox.Show(XoaThanhCong, ThongBao);
                        LoadDanhSachGiaPhong();
                        LoadDanhSachPhong();
                    }
                    else if (XoaGiaPhong == 2)
                    {
                        MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                    }
                    else
                    {
                        MessageBox.Show("Phong chi co mot gia!", ThongBao);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        // Gia dich vu

        private void btTimGiaDichVu_Click(object sender, EventArgs e)
        {
            DanhSachGiaDichVu.DataSource = DichVuDAO.Instance1.TimGiaDichVu(txtTenGiaDichVu.Text);
        }

        private void btXoaGiaDichVu_Click(object sender, EventArgs e)
        {
            if (txtIDGiaDichVu.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    int XoaGiaDichVu = DichVuDAO.Instance1.XoaGiaDichVu(Convert.ToInt32(txtIDGiaDichVu.Text), txtTenGiaDichVu.Text);
                    if (XoaGiaDichVu == 1)
                    {
                        MessageBox.Show(XoaThanhCong, ThongBao);
                        LoadDanhSachGiaDichVu();
                        LoadDanhSachDichVu();
                    }
                    else if (XoaGiaDichVu == 2)
                    {
                        MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                    }
                    else
                    {
                        MessageBox.Show("Dich vu chi co mot gia!", ThongBao);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion





        #region ThuChi

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////Method///////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////Thu/////////////////////////////////////////////////////////////

        public void LoadThu()
        {
            LoadDanhSachHoaDonThu();
            BindingHoaDonThu();
        }

        public void LoadDanhSachHoaDonThu()
        {
            DanhSachHoaDonThu.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonThu();
            dtgvDanhSachHoaDonThu.DataSource = DanhSachHoaDonThu;
        }

        public void BindingHoaDonThu()
        {
            txtIDHoaDonThu.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonThu.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenPhongThu.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonThu.DataSource, "Ten phong", true, DataSourceUpdateMode.Never));
            txtTenNhanVienThu.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonThu.DataSource, "Ten nhan vien", true, DataSourceUpdateMode.Never));
        }

        private void dtgvDanhSachHoaDonThu_Click(object sender, EventArgs e)
        {
            if (txtIDHoaDonThu.Text == "")
            {
                txtSoTienHoaDonThu.Text = "";
            }
            else
            {
                txtSoTienHoaDonThu.Text = (HoaDonDAO.Instance1.TongTienTrenHoaDon(txtIDHoaDonThu.Text)).ToString();
            }
        }

        ////////////////////////////////////////////////////////////Chi/////////////////////////////////////////////////////////////

        public void LoadChi()
        {
            LoadDanhSachHoaDonChi();
            BindingHoaDonChi();
        }

        public void LoadDanhSachHoaDonChi()
        {
            DanhSachHoaDonChi.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonChi();
            dtgvDanhSachHoaDonChi.DataSource = DanhSachHoaDonChi;
        }

        public void BindingHoaDonChi()
        {
            txtIDHoaDonChi.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonChi.DataSource, "ID", true, DataSourceUpdateMode.Never));
            txtTenDichVuChi.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonChi.DataSource, "Ten dich vu", true, DataSourceUpdateMode.Never));
            txtTenNhanVienChi.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonChi.DataSource, "Ten nhan vien", true, DataSourceUpdateMode.Never));
            txtSoLuongChi.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonChi.DataSource, "So luong", true, DataSourceUpdateMode.Never));
            txtSoTienHoaDonChi.DataBindings.Add(new Binding("Text", dtgvDanhSachHoaDonChi.DataSource, "Gia nhap", true, DataSourceUpdateMode.Never));
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////Even////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        ////////////////////////////////////////////////////////////Thu/////////////////////////////////////////////////////////////

        private void btXemThu_Click(object sender, EventArgs e)
        {
            DanhSachHoaDonThu.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonThuTheoNgay(dtpkNgayBatDauThu.Value, dtpkNgayKetThucThu.Value);
            txtTongTienThu.Text = (HoaDonDAO.Instance1.TongTienTatCaHoaDonThu(dtpkNgayBatDauThu.Value, dtpkNgayKetThucThu.Value)).ToString();
        }

        private void btXemTatCaThu_Click(object sender, EventArgs e)
        {
            DanhSachHoaDonThu.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonThu();
        }

        private void btXoaThu_Click(object sender, EventArgs e)
        {
            if (txtIDHoaDonThu.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaHoaDonThu = HoaDonDAO.Instance1.XoaHoaDonThu(Convert.ToInt32(txtIDHoaDonThu.Text));
                        if (XoaHoaDonThu == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachHoaDonThu();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else { }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        ////////////////////////////////////////////////////////////Chi/////////////////////////////////////////////////////////////

        private void btXemChi_Click(object sender, EventArgs e)
        {
            DanhSachHoaDonChi.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonChiTheoNgay(dtpkNgayBatDauChi.Value, dtpkNgayKetThucChi.Value);
            txtTongTienChi.Text = (HoaDonDAO.Instance1.TongTienTatCaHoaDonChi(dtpkNgayBatDauThu.Value, dtpkNgayKetThucThu.Value)).ToString();
        }

        private void btXemTatCaChi_Click(object sender, EventArgs e)
        {
            dtgvDanhSachHoaDonChi.DataSource = HoaDonDAO.Instance1.DanhSachHoaDonChi();
        }

        private void btXoaChi_Click(object sender, EventArgs e)
        {
            if (txtIDHoaDonChi.Text == "")
            {
                MessageBox.Show(KhongDuThongTin, ThongBao);
            }
            else
            {
                try
                {
                    if (MessageBox.Show(ThaoTacNaySeXoaTatCaCacThongTinLienQuan, ThongBao, MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        int XoaHoaDonChi = HoaDonDAO.Instance1.XoaHoaDonChi(Convert.ToInt32(txtIDHoaDonChi.Text));
                        if (XoaHoaDonChi == 1)
                        {
                            MessageBox.Show(XoaThanhCong, ThongBao);
                            LoadDanhSachHoaDonChi();
                        }
                        else
                        {
                            MessageBox.Show(ThongTinCanXoaKhongTonTai, ThongBao);
                        }
                    }
                    else { }
                }
                catch (Exception)
                {
                    MessageBox.Show(ThongTinNhapVaoKhongDung, ThongBao);
                }
            }
        }

        #endregion

    }
}
