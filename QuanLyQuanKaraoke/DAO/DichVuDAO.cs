using QuanLyQuanKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class DichVuDAO
    {
        private static DichVuDAO Instance;

        public static DichVuDAO Instance1
        {
            get { if (Instance == null) Instance = new DichVuDAO(); return DichVuDAO.Instance; }
            private set { DichVuDAO.Instance = value; }
        }

        private DichVuDAO() { }

        public DataTable DanhSachDichVu()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenDichVu AS 'Ten dich vu', b.GiaDichVu AS 'Gia dich vu'  FROM dbo.DichVu a, dbo.GiaDichVu b WHERE a.TenDichVu = b.TenDichVu AND b.GiaMoiNhat = 1");
        }

        public int ThemDichVu(string TenDichVu, int GiaDichVu)
        {
            int KiemTraTenDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenDichVu) FROM dbo.DichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            if (KiemTraTenDichVu > 0)
            {
                return 0;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemDichVu @TenDichVu ", new object[] { TenDichVu });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemGiaDichVu @GiaDichVu , @TenDichVu ", new object[] { GiaDichVu, TenDichVu });
                return 1;
            }
        }

        public DataTable DanhSachGiaDichVu()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT IDGiaDichVu AS 'ID', TenDichVu AS 'Ten dich vu', GiaDichVu AS 'Gia dich vu', NgayApDung AS 'Ngay ap dung' FROM GiaDichVu");
        }

        public DataTable TimGiaDichVu(string TenDichVu)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT IDGiaDichVu AS 'ID', TenDichVu AS 'Ten dich vu', GiaDichVu AS 'Gia dich vu', NgayApDung AS 'Ngay ap dung' FROM GiaDichVu WHERE dbo.fuConvertToUnsign1(TenDichVu) LIKE N'%' + dbo.fuConvertToUnsign1(N'" + TenDichVu + "') + '%'");
        }

        public void SuaDichVu(string TenDichVu, int GiaDichVu)
        {
            int KiemTraGiaDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaDichVu) FROM dbo.GiaDichVu WHERE TenDichVu = '" + TenDichVu + "' AND GiaMoiNhat = 1 AND GiaDichVu = " + GiaDichVu));
            if (KiemTraGiaDichVu <= 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_SuaGiaDichVu @TenDichVu ", new object[] { TenDichVu });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemGiaDichVu @GiaDichVu , @TenDichVu ", new object[] { GiaDichVu, TenDichVu });
            }
        }

        public int XoaGiaDichVu(int IDGiaDichVu, string TenDichVu)
        {
            int KiemTraGiaDichVuCu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaDichVu) FROM GiaDichVu WHERE TenDichVu = '" + TenDichVu + "' AND GiaMoiNhat = 0"));
            int KiemTraGiaDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaDichVu) FROM dbo.GiaDichVu WHERE IDGiaDichVu = " + IDGiaDichVu));
            int KiemTraGiaDichVuMoiNhat = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaDichVu) FROM dbo.GiaDichVu WHERE GiaMoiNhat = 1 AND IDGiaDichVu = " + IDGiaDichVu));
            if (KiemTraGiaDichVu < 1)
            {
                return 2;
            }
            else if (KiemTraGiaDichVuCu < 1)
            {
                return 3;
            }
            else if (KiemTraGiaDichVuMoiNhat > 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaDichVuMoiNhat @IDGiaDichVu , @TenDichVu ", new object[] { IDGiaDichVu, TenDichVu });
                return 1;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaDichVu @IDGiaDichVu ", new object[] { IDGiaDichVu });
                return 1;
            }
        }

        public int XoaDichVu(string TenDichVu)
        {
            int KiemTraDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenDichVu) FROM DichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            int KiemTraHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            int KiemTraGiaDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaDichVu) FROM GiaDichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            int KiemTraHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE TenDichVu = '" + TenDichVu + "'"));
            if (KiemTraDichVu < 1)
            {
                return 0;
            }
            else
            {
                if (KiemTraHoaDonDichVu > 0)
                {
                    for (int i = 0; i < KiemTraHoaDonDichVu; i++)
                    {
                        int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonDichVu) FROM HoaDonDichVu WHERE TenDichVu = '" + TenDichVu + "'"));
                        DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonDichVu @IDHoaDonDichVu ", new object[] { IDHoaDonDichVu });
                    }
                }
                else { }
                if (KiemTraGiaDichVu > 0)
                {
                    for (int i = 0; i < KiemTraGiaDichVu; i++)
                    {
                        int IDGiaDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDGiaDichVu) FROM GiaDichVu WHERE TenDichVu = '" + TenDichVu + "'"));
                        DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaDichVu @IDGiaDichVu ", new object[] { IDGiaDichVu });
                    }
                }
                else { }
                if (KiemTraHoaDonNhapHang > 0)
                {
                    for (int i = 0; i < KiemTraGiaDichVu; i++)
                    {
                        int IDHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE TenDichVu = '" + TenDichVu + "'"));
                        DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonNhapHang @IDHoaDonNhapHang ", new object[] { IDHoaDonNhapHang });
                    }
                }
                else { }
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaDichVu @TenDichVu ", new object[] { TenDichVu });
                return 1;
            }
        }

        public List<DichVu> ListDichVu()
        {
            List<DichVu> DanhSach = new List<DichVu>();
            DataTable Data = DataProvider.Instance1.ExecuteQuery("SELECT * FROM DichVu");
            foreach (DataRow item in Data.Rows)
            {
                DichVu DanhSachDichVu = new DichVu(item);
                DanhSach.Add(DanhSachDichVu);
            }
            return DanhSach;
        }

        public int GiaDichVu(string TenDichVu)
        {
            return Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT GiaDichVu FROM GiaDichVu WHERE GiaMoiNhat = 1 AND TenDichVu = '" + TenDichVu + "'"));
        }

        public int NhapHang(string TenDichVu, string TenKho, int GiaNhap, int SoLuong)
        {
            if ((GiaNhap <= 0) || (SoLuong <= 0))
            {
                return 0;
            }
            else
            {
                int CMND = Convert.ToInt32(CaNhanDAO.Instance1.GetCMND());
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemHoaDonNhapHang @TenDichVu , @TenKho , @GiaNhap , @SoLuong , @CMND ", new object[] { TenDichVu, TenKho, GiaNhap, SoLuong, CMND });
                return 1;
            }
        }

        public DataTable DanhSachThemDichVu(string TenPhong)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenDichVu AS 'Ten dich vu', a.SoLuong AS 'So luong' FROM HoaDonDichVu a, HoaDon b WHERE a.IDHoaDon = b.IDHoaDon AND b.TenPhong = '" + TenPhong + "'");
        }

        public int ThemDichVuChoPhong(string TenPhong, string TenDichVu, int SoLuong, string TenKho)
        {
            int KiemTraPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM Phong WHERE DangSuDung = 1 AND TenPhong = '"+TenPhong+"'"));
            int KiemTraDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenDichVu) FROM DichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            if ((SoLuong <= 0) || (KiemTraPhong <= 0) || (KiemTraDichVu <= 0))
            {
                return 0;
            }
            else
            {
                int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "'"));
                int KiemTraHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu a, HoaDon b WHERE a.IDHoaDon = b.IDHoaDon AND b.IDHoaDon = " + IDHoaDon + " AND a.TenDichVu = '" + TenDichVu + "'"));
                if (KiemTraHoaDonDichVu <= 0)
                {

                    DataProvider.Instance1.ExecuteNonQuery("USP_ThemHoaDonDichVu @IDHoaDon , @TenDichVu , @SoLuong , @TenKho ", new object[] { IDHoaDon, TenDichVu, SoLuong, TenKho });
                }
                else
                {
                    int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT IDHoaDonDichVu FROM HoaDonDichVu a, HoaDon b WHERE a.IDHoaDon = b.IDHoaDon AND b.IDHoaDon = " + IDHoaDon + " AND a.TenDichVu = '" + TenDichVu + "'"));
                    DataProvider.Instance1.ExecuteNonQuery("USP_ThemDichVuCoSan @IDHoaDonDichVu , @SoLuong ", new object[] { IDHoaDonDichVu, SoLuong });
                }
                return 1;
            }
        }

        public int TraDichVuChoPhong(string TenPhong, string TenDichVu, int SoLuong)
        {
            int KiemTraPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM Phong WHERE DangSuDung = 1 AND TenPhong = '" + TenPhong + "'"));
            int KiemTraDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenDichVu) FROM DichVu WHERE TenDichVu = '" + TenDichVu + "'"));
            if ((SoLuong <= 0) || (KiemTraPhong <= 0) || (KiemTraDichVu <= 0))
            {
                return 0;
            }
            else
            {
                int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "'"));
                int KiemTraHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu a, HoaDon b WHERE a.IDHoaDon = b.IDHoaDon AND b.IDHoaDon = " + IDHoaDon + " AND a.TenDichVu = '" + TenDichVu + "'"));
                if (KiemTraHoaDonDichVu <= 0)
                {
                    return 0;
                }
                else
                {
                    int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT IDHoaDonDichVu FROM HoaDonDichVu a, HoaDon b WHERE a.IDHoaDon = b.IDHoaDon AND b.IDHoaDon = " + IDHoaDon + " AND a.TenDichVu = '" + TenDichVu + "'"));
                    int SoLuongDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT SoLuong FROM HoaDonDichVu WHERE IDHoaDonDichVu = " + IDHoaDonDichVu));
                    if (SoLuong > SoLuongDichVu)
                    {
                        return 2;
                    }
                    else
                    {
                        DataProvider.Instance1.ExecuteNonQuery("USP_TraDichVuCoSan @IDHoaDonDichVu , @SoLuong ", new object[] { IDHoaDonDichVu, SoLuong });
                        return 1;
                    }
                }
            }
        }

        public DataTable DanhSachHoaDonDichVu(string TenPhong)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenDichVu AS 'Ten dich vu', a.SoLuong AS 'So luong', a.SoLuong*b.GiaDichVu AS 'Tong gia' FROM HoaDonDichVu a, GiaDichVu b WHERE a.TenDichVu = b.TenDichVu AND b.GiaMoiNhat = 1 AND IDHoaDon = (SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "')");
        }

        public int TongTienDichVu(string TenPhong)
        {
            int KiemTraSoLuongDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(a.SoLuong*b.GiaDichVu) FROM HoaDonDichVu a, GiaDichVu b WHERE a.TenDichVu = b.TenDichVu AND b.GiaMoiNhat = 1 AND IDHoaDon = (SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "')"));
            if (KiemTraSoLuongDichVu <= 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT SUM(a.SoLuong*b.GiaDichVu) FROM HoaDonDichVu a, GiaDichVu b WHERE a.TenDichVu = b.TenDichVu AND b.GiaMoiNhat = 1 AND IDHoaDon = (SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "')"));
            }
        }
    }
}
