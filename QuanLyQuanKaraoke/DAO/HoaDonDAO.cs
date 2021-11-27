using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class HoaDonDAO
    {
        private static HoaDonDAO Instance;

        public static HoaDonDAO Instance1
        {
            get { if (Instance == null) Instance = new HoaDonDAO(); return HoaDonDAO.Instance; }
            private set { HoaDonDAO.Instance = value; }
        }

        private HoaDonDAO() { }

        ////////////////////////////////////////////////////////////Thu/////////////////////////////////////////////////////////////

        public DataTable DanhSachHoaDonThu()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT IDHoaDon AS 'ID', b.TenNhanVien AS 'Ten nhan vien', TenPhong AS 'Ten phong', a.ThoiGianRa AS 'Ngay thanh toan' FROM HoaDon a, NhanVien b WHERE a.CMND = b.CMND");
        }

        public int TongTienTrenHoaDon(string IDHoaDon)
        {
            int KiemTraIDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDon) FROM HoaDon WHERE IDHoaDon = " + IDHoaDon));
            if (KiemTraIDHoaDon <= 0)
            {
                return 0;
            }
            else
            {
                int TienPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT b.GiaPhong FROM HoaDon a, GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.NgayApDung < a.ThoiGianRa AND b.IDGiaPhong = (SELECT MAX(b.IDGiaPhong) FROM HoaDon a, GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.NgayApDung < a.ThoiGianRa AND IDHoaDon = " + IDHoaDon + ") AND IDHoaDon = " + IDHoaDon));
                int SoLuongHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu WHERE IDHoaDon = " + IDHoaDon));
                int TienDichVu = 0;
                for (int i = 0; i < SoLuongHoaDonDichVu; i++)
                {
                    int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT IDHoaDonDichVu FROM HoaDonDichVu WHERE IDHoaDon = 5 ORDER BY IDHoaDonDichVu OFFSET " + i + " ROWS FETCH NEXT 1 ROWS ONLY"));
                    TienDichVu = TienDichVu + Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT b.SoLuong*c.GiaDichVu FROM HoaDon a, HoaDonDichVu b, GiaDichVu c WHERE a.IDHoaDon = b.IDHoaDon AND b.TenDichVu = c.TenDichVu AND c.NgayApDung < a.ThoiGianRa AND c.IDGiaDichVu = (SELECT MAX(IDGiaDichVu) FROM HoaDon a, HoaDonDichVu b, GiaDichVu c WHERE a.IDHoaDon = b.IDHoaDon AND b.TenDichVu = c.TenDichVu AND c.NgayApDung < a.ThoiGianRa AND b.IDHoaDonDichVu = " + IDHoaDonDichVu + ") AND b.IDHoaDonDichVu = " + IDHoaDonDichVu));
                }
                return TienPhong + TienDichVu;
            }
        }

        public DataTable DanhSachHoaDonThuTheoNgay(DateTime ThoiGianVao, DateTime ThoiGianRa)
        {
            string Vao = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianVao.Day).ToString(),(ThoiGianVao.Month).ToString(),(ThoiGianVao.Year).ToString());
            string Ra = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianRa.Day).ToString(), (ThoiGianRa.Month).ToString(), (ThoiGianRa.Year).ToString());
            return DataProvider.Instance1.ExecuteQuery("SELECT a.IDHoaDon AS 'ID', b.TenNhanVien AS 'Ten nhan vien', a.TenPhong AS 'Ten phong' FROM HoaDon a, NhanVien b WHERE a.CMND = b.CMND AND a.ThoiGianRa > '" + Vao + "' AND a.ThoiGianRa < '" + Ra + "'");
        }

        public int TongTienTatCaHoaDonThu(DateTime ThoiGianVao, DateTime ThoiGianRa)
        {
            string Vao = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianVao.Day).ToString(),(ThoiGianVao.Month).ToString(),(ThoiGianVao.Year).ToString());
            string Ra = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianRa.Day).ToString(), (ThoiGianRa.Month).ToString(), (ThoiGianRa.Year).ToString());
            int SoLuongHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(a.IDHoaDon) FROM HoaDon a, NhanVien b WHERE a.CMND = b.CMND AND a.ThoiGianRa > '" + Vao + "' AND a.ThoiGianRa < '" + Ra + "'"));
            int TongTien = 0;
            for (int i = 0; i < SoLuongHoaDon; i++)
            {
                int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT a.IDHoaDon FROM HoaDon a, NhanVien b WHERE a.CMND = b.CMND AND a.ThoiGianRa > '" + Vao + "' AND a.ThoiGianRa < '" + Ra + "' ORDER BY a.IDHoaDon OFFSET " + i + " ROWS FETCH NEXT 1 ROWS ONLY"));
                TongTien = TongTien + TongTienTrenHoaDon(IDHoaDon.ToString());
            }
            return TongTien;
        }

        public int XoaHoaDonThu(int IDHoaDon)
        {
            int KiemTraHoaDonThu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDon) FROM HoaDon WHERE IDHoaDon = " + IDHoaDon));
            if (KiemTraHoaDonThu <= 0)
            {
                return 0;
            }
            else
            {
                int SoHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu WHERE IDHoaDon = " + IDHoaDon));
                for (int i = 0; i < SoHoaDonDichVu; i++)
                {
                    int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonDichVu) FROM HoaDonDichVu WHERE IDHoaDon = " + IDHoaDon));
                    DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonDichVu @IDHoaDonDichVu ", new object[] { IDHoaDonDichVu });
                }
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDon @IDHoaDon ", new object[] { IDHoaDon });
                return 1;
            }
        }

        ////////////////////////////////////////////////////////////Chi/////////////////////////////////////////////////////////////

        public DataTable DanhSachHoaDonChi()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.IDHoaDonNhapHang AS 'ID', b.TenNhanVien AS 'Ten nhan vien', a.TenDichVu AS 'Ten dich vu', a.TenKho AS 'Ten kho', a.SoLuong AS 'So luong', a.GiaNhap AS 'Gia nhap', a.NgayNhap AS 'Ngay Nhap' FROM HoaDonNhapHang a, NhanVien b WHERE a.CMND = b.CMND");
        }

        public DataTable DanhSachHoaDonChiTheoNgay(DateTime ThoiGianVao, DateTime ThoiGianRa)
        {
            string Vao = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianVao.Day).ToString(), (ThoiGianVao.Month).ToString(), (ThoiGianVao.Year).ToString());
            string Ra = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianRa.Day).ToString(), (ThoiGianRa.Month).ToString(), (ThoiGianRa.Year).ToString());
            return DataProvider.Instance1.ExecuteQuery("SELECT a.IDHoaDonNhapHang AS 'ID', b.TenNhanVien AS 'Ten nhan vien', a.TenDichVu AS 'Ten dich vu', a.TenKho AS 'Ten kho', a.SoLuong AS 'So luong', a.GiaNhap AS 'Gia nhap', a.NgayNhap AS 'Ngay Nhap' FROM HoaDonNhapHang a, NhanVien b WHERE a.CMND = b.CMND AND a.NgayNhap > '" + Vao + "' AND a.NgayNhap < '" + Ra + "'");
        }

        public int TongTienTatCaHoaDonChi(DateTime ThoiGianBatDau, DateTime ThoiGianKetThuc)
        {
            string Vao = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianBatDau.Day).ToString(), (ThoiGianBatDau.Month).ToString(), (ThoiGianBatDau.Year).ToString());
            string Ra = CaNhanDAO.Instance1.ChuoiNgay((ThoiGianKetThuc.Day).ToString(), (ThoiGianKetThuc.Month).ToString(), (ThoiGianKetThuc.Year).ToString());
            return Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT SUM(a.GiaNhap) FROM HoaDonNhapHang a, NhanVien b WHERE a.CMND = b.CMND AND a.NgayNhap > '" + Vao + "' AND a.NgayNhap < '" + Ra + "'"));
        }

        public int XoaHoaDonChi(int IDHoaDon)
        {
            int KiemTraHoaDonChi = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE IDHoaDonNhapHang = " + IDHoaDon));
            if (KiemTraHoaDonChi <= 0)
            {
                return 0;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonNhapHang @IDHoaDonNhapHang ", new object[] { IDHoaDon });
                return 1;
            }
        }
    }
}
