using QuanLyQuanKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class NhanVienDAO
    {
        private static NhanVienDAO Instance;

        public static NhanVienDAO Instance1
        {
            get { if (Instance == null) Instance = new NhanVienDAO(); return NhanVienDAO.Instance; }
            private set { NhanVienDAO.Instance = value; }
        }

        private NhanVienDAO() { }

        public DataTable DanhSachNhanVien()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT TenNhanVien AS 'Ten nhan vien', CMND, ChucVu AS 'Chuc vu', SDT, NamSinh AS 'Nam sinh', Luong, NgayVaoLam AS 'Ngay vao lam' FROM NhanVien");
        }

        public int ThemNhanVien(string TenNhanVien, string CMND, string ChucVu, string SDT, int NamSinh, string NgayVaoLam, int Luong, string TenTaiKhoan)
        {
            int KiemTraCMND = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(CMND) FROM NhanVien WHERE CMND = '" + CMND + "'"));
            int KiemTraSDT = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(SDT) FROM NhanVien WHERE SDT = '" + SDT + "'"));
            int KiemTraTaiKhoan = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenTaiKhoan) FROM TaiKhoan WHERE TenTaiKhoan = '" + TenTaiKhoan + "'"));
            if ((KiemTraCMND > 0) || (KiemTraSDT > 0) || (KiemTraTaiKhoan > 0))
            {
                return 0;
            }
            else
            {
                string MatKhauDaMaHoa = CaNhanDAO.Instance1.MaHoaMatKhau("1");
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemNhanVien @TenNhanVien , @CMND , @ChucVu , @SDT , @NamSinh , @NgayVaoLam , @Luong ", new object[] { TenNhanVien, CMND, ChucVu, SDT, NamSinh, NgayVaoLam, Luong });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemTaiKhoan @TenTaiKhoan , @MatKhau , @CMND ", new object[] { TenTaiKhoan, MatKhauDaMaHoa, CMND });
                return 1;
            }
        }

        public DataTable TimNhanVien(string TenNhanVien)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT TenNhanVien AS 'Ten nhan vien', CMND, ChucVu AS 'Chuc vu', SDT, NamSinh AS 'Nam sinh', Luong, NgayVaoLam AS 'Ngay vao lam' FROM NhanVien WHERE dbo.fuConvertToUnsign1(TenNhanVien) LIKE N'%' + dbo.fuConvertToUnsign1(N'" + TenNhanVien + "') + '%'");
        }

        public string BindingTaiKhoan(string CMND)
        {
            return (DataProvider.Instance1.ExecuteScalar("SELECT TenTaiKhoan FROM TaiKhoan WHERE CMND = '" + CMND + "'")).ToString();
        }

        public int ResetMatKhau(string CMND)
        {
            string MatKhauDaMaHoa = CaNhanDAO.Instance1.MaHoaMatKhau("1");
            int SoLuongCMND = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenTaiKhoan) FROM dbo.TaiKhoan WHERE CMND = '" + CMND + "'"));
            if (SoLuongCMND > 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_DoiMatKhau @MatKhau , @CMND ", new object[] { MatKhauDaMaHoa, CMND });
                return 0;
            }
            else
            {
                return 1;
            }
        }
        
        public int SuaThongTinNhanVien(string TenNhanVien, string CMND, string ChucVu, string SDT, int NamSinh, string NgayVaoLam, int Luong)
        {
            int KiemTraSDT = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(SDT) FROM NhanVien WHERE SDT = '" + SDT + "' AND CMND != '" + CMND + "'"));
            if ((KiemTraSDT > 0) || (SDT.Length > 11))
            {
                return 0;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_SuaNhanVien @TenNhanVien , @CMND , @ChucVu , @SDT , @NamSinh , @NgayVaoLam , @Luong ", new object[] { TenNhanVien, CMND, ChucVu, SDT, NamSinh, NgayVaoLam, Luong });
                return 1;
            }
        }

        public int XoaNhanVien(string CMND)
        {
            int KiemTraNhanVien = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(CMND) FROM NhanVien WHERE CMND = " + CMND));
            int KiemTraTaiKhoan = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenTaiKhoan) FROM TaiKhoan WHERE CMND = " + CMND));
            int KiemTraHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDon) FROM HoaDon WHERE CMND = " + CMND));
            int KiemTraHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE CMND = " + CMND));
            if (KiemTraNhanVien < 1)
            {
                return 0;
            }
            else
            {
                if (KiemTraHoaDon > 0)
                {
                    for (int i = 0; i < KiemTraHoaDon; i++)
                    {
                        int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDon) FROM HoaDon WHERE CMND = " + CMND));
                        HoaDonDAO.Instance1.XoaHoaDonThu(IDHoaDon);
                    }
                }
                else { }
                if (KiemTraHoaDonNhapHang > 0)
                {
                    for (int i = 0; i < KiemTraHoaDonNhapHang; i++)
                    {
                        int IDHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE CMND = " + CMND));
                        HoaDonDAO.Instance1.XoaHoaDonChi(IDHoaDonNhapHang);
                    }
                }
                else { }
                if (KiemTraTaiKhoan > 0)
                {
                    for (int i = 0; i < KiemTraTaiKhoan; i++)
                    {
                        string TenTaiKhoan = (DataProvider.Instance1.ExecuteScalar("SELECT TenTaiKhoan FROM TaiKhoan WHERE CMND = " + CMND)).ToString();
                        DataProvider.Instance1.ExecuteNonQuery("USP_XoaTaiKhoan @TenTaiKhoan ", new object[] { TenTaiKhoan });
                    }
                }
                else { }

                DataProvider.Instance1.ExecuteNonQuery("USP_XoaNhanVien @CMND ", new object[] { CMND });
                return 1;
            }
        }
    }
}
