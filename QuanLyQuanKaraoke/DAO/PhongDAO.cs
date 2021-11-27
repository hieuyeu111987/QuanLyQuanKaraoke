using QuanLyQuanKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class PhongDAO
    {
        private static PhongDAO Instance;

        public static PhongDAO Instance1
        {
            get { if (Instance == null) Instance = new PhongDAO(); return PhongDAO.Instance; }
            private set { PhongDAO.Instance = value; }
        }

        private PhongDAO() { }

        public DataTable DanhSachPhong()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenPhong AS 'Ten phong', a.SoNguoiToiThieu AS 'So nguoi toi thieu', a.SoNguoiToiDa AS 'So nguoi toi da', b.GiaPhong AS 'Gia phong'  FROM dbo.Phong a, dbo.GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.GiaMoiNhat = 1");
        }

        public int ThemPhong(string TenPhong, int SoNguoiToiThieu, int SoNguoiToiDa, int GiaPhong)
        {
            int KiemTraTenPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM dbo.Phong WHERE TenPhong = '" + TenPhong + "'"));
            if (KiemTraTenPhong > 0)
            {
                return 0;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemPhong @TenPhong , @SoNguoiToiThieu , @SoNguoiToiDa ", new object[] { TenPhong, SoNguoiToiThieu, SoNguoiToiDa });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemGiaPhong @GiaPhong , @TenPhong ", new object[] { GiaPhong, TenPhong });
                return 1;
            }
        }

        public DataTable DanhSachGiaPhong()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT IDGiaPhong AS 'ID', TenPhong AS 'Ten phong', GiaPhong AS 'Gia phong', NgayApDung AS 'Ngay ap dung' FROM GiaPhong");
        }

        public DataTable TimGiaPhong(string TenPhong)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT IDGiaPhong AS 'ID', TenPhong AS 'Ten phong', GiaPhong AS 'Gia phong', NgayApDung AS 'Ngay ap dung' FROM GiaPhong WHERE dbo.fuConvertToUnsign1(TenPhong) LIKE N'%' + dbo.fuConvertToUnsign1(N'" + TenPhong + "') + '%'");
        }

        public void SuaPhong(string TenPhong, int SoNguoiToiThieu, int SoNguoiToiDa, int GiaPhong)
        {
            int KiemTraGiaPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaPhong) FROM dbo.GiaPhong WHERE TenPhong = '" + TenPhong + "' AND GiaMoiNhat = 1 AND GiaPhong = " + GiaPhong));
            if (KiemTraGiaPhong > 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_SuaPhong @TenPhong , @SoNguoiToiThieu , @SoNguoiToiDa ", new object[] { TenPhong, SoNguoiToiThieu, SoNguoiToiDa });
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_SuaPhong @TenPhong , @SoNguoiToiThieu , @SoNguoiToiDa ", new object[] { TenPhong, SoNguoiToiThieu, SoNguoiToiDa });
                DataProvider.Instance1.ExecuteNonQuery("USP_SuaGiaPhong @TenPhong ", new object[] { TenPhong });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemGiaPhong @GiaPhong , @TenPhong ", new object[] { GiaPhong, TenPhong });
            }
        }

        public int XoaGiaPhong(int IDGiaPhong, string TenPhong)
        {
            int KiemTraGiaPhongCu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaPhong) FROM GiaPhong WHERE TenPhong = '" + TenPhong + "' AND GiaMoiNhat = 0"));
            int KiemTraGiaPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaPhong) FROM dbo.GiaPhong WHERE IDGiaPhong = " + IDGiaPhong));
            int KiemTraGiaPhongMoiNhat = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaPhong) FROM dbo.GiaPhong WHERE GiaMoiNhat = 1 AND IDGiaPhong = " + IDGiaPhong));
            if (KiemTraGiaPhong < 1)
            {
                return 2;
            }
            else if (KiemTraGiaPhongCu < 1)
            {
                return 3;
            }
            else if (KiemTraGiaPhongMoiNhat > 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaPhongMoiNhat @IDGiaPhong , @TenPhong ", new object[] { IDGiaPhong, TenPhong });
                return 1;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaPhong @IDGiaPhong ", new object[] { IDGiaPhong });
                return 1;
            }
        }

        public int XoaPhong(string TenPhong)
        {
            int KiemTraPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM Phong WHERE TenPhong = '" + TenPhong + "'"));
            int KiemTraGiaPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDGiaPhong) FROM GiaPhong WHERE TenPhong = '" + TenPhong + "'"));
            int KiemTraHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM HoaDon WHERE TenPhong = '" + TenPhong + "'"));
            if (KiemTraPhong < 1)
            {
                return 0;
            }
            else
            {
                if (KiemTraHoaDon > 0)
                {
                    for (int i = 0; i < KiemTraHoaDon; i++)
                    {
                        int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "'"));
                        int SoHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonDichVu) FROM HoaDonDichVu WHERE IDHoaDon = " + IDHoaDon));
                        for (int j = 0; j < SoHoaDonDichVu; j++)
                        {
                            int IDHoaDonDichVu = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonDichVu) FROM HoaDonDichVu WHERE IDHoaDon = " + IDHoaDon));
                            DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonDichVu @IDHoaDonDichVu ", new object[] { IDHoaDonDichVu });
                        }
                            DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDon @IDHoaDon ", new object[] { IDHoaDon });
                    }
                }
                else { }
                if (KiemTraGiaPhong > 0)
                {
                    for (int i = 0; i < KiemTraGiaPhong; i++)
                    {
                        int IDGiaPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDGiaPhong) FROM GiaPhong WHERE TenPhong = '" + TenPhong + "'"));
                        DataProvider.Instance1.ExecuteNonQuery("USP_XoaGiaPhong @IDGiaPhong ", new object[] { IDGiaPhong });
                    }
                }
                else { }
                
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaPhong @TenPhong ", new object[] { TenPhong });
                return 1;
            }
        }

        public List<Phong> ListPhong()
        {
            List<Phong> DanhSach = new List<Phong>();
            DataTable Data = DataProvider.Instance1.ExecuteQuery("SELECT * FROM Phong WHERE DangSuDung = 1");
            foreach (DataRow item in Data.Rows)
            {
                Phong DanhSachPhong = new Phong(item);
                DanhSach.Add(DanhSachPhong);
            }
            return DanhSach;
        }

        public DataTable DanhSachDatPhong()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenPhong AS 'Ten phong', a.SoNguoiToiThieu AS 'So nguoi toi thieu', a.SoNguoiToiDa AS 'So nguoi toi da', b.GiaPhong AS 'Gia phong'  FROM dbo.Phong a, dbo.GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.GiaMoiNhat = 1 AND a.DangSuDung = 0");
        }

        public DataTable TimDanhSachDatPhong(string TenPhong)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenPhong AS 'Ten phong', a.SoNguoiToiThieu AS 'So nguoi toi thieu', a.SoNguoiToiDa AS 'So nguoi toi da', b.GiaPhong AS 'Gia phong'  FROM dbo.Phong a, dbo.GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.GiaMoiNhat = 1 AND a.DangSuDung = 0 AND dbo.fuConvertToUnsign1(a.TenPhong) LIKE N'%' + dbo.fuConvertToUnsign1(N'" + TenPhong + "') + '%'");
        }

        public int DatPhong(string TenPhong)
        {
            int KiemTraPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM Phong WHERE TenPhong = '" + TenPhong + "'"));
            if (KiemTraPhong <= 0)
            {
                return 0;
            }
            else
            {
                string CMND = CaNhanDAO.Instance1.GetCMND();
                DataProvider.Instance1.ExecuteNonQuery("USP_DatPhong @TenPhong ", new object[] { TenPhong });
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemHoaDon @CMND , @TenPhong ", new object[] { CMND, TenPhong });
                return 1;
            }
        }

        public DataTable DanhSachThanhToanPhong()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenPhong AS 'Ten phong', b.GiaPhong AS 'Gia phong'  FROM dbo.Phong a, dbo.GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.GiaMoiNhat = 1 AND a.DangSuDung = 1");
        }

        public DataTable TimDanhSachThanhToanPhong(string TenPhong)
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT a.TenPhong AS 'Ten phong', b.GiaPhong AS 'Gia phong'  FROM dbo.Phong a, dbo.GiaPhong b WHERE a.TenPhong = b.TenPhong AND b.GiaMoiNhat = 1 AND a.DangSuDung = 1 AND dbo.fuConvertToUnsign1(a.TenPhong) LIKE N'%' + dbo.fuConvertToUnsign1(N'" + TenPhong + "') + '%'");
        }

        public string ThoiGianVao(string TenPhong)
        {
            DateTime NgayGio = Convert.ToDateTime(DataProvider.Instance1.ExecuteScalar("SELECT ThoiGianVao FROM HoaDon WHERE IDHoaDon = (SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "') AND TenPhong = '" + TenPhong + "'"));
            return (NgayGio.Hour).ToString() + "h" + (NgayGio.Minute).ToString() + " - " + (NgayGio.Day).ToString() + "/" + (NgayGio.Month).ToString() + "/" + (NgayGio.Year).ToString();
            
        }

        public string ThoiGianRa()
        {
            DateTime NgayGio = Convert.ToDateTime(DataProvider.Instance1.ExecuteScalar("SELECT GETDATE()"));
            return (NgayGio.Hour).ToString() + "h" + (NgayGio.Minute).ToString() + " - " + (NgayGio.Day).ToString() + "/" + (NgayGio.Month).ToString() + "/" + (NgayGio.Year).ToString();
        }

        public int ThoiGianSuDung(string TenPhong)
        {
            return Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT DATEDIFF(MINUTE,ThoiGianVao,GETDATE()) FROM HoaDon WHERE IDHoaDon = (SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "') AND TenPhong = '" + TenPhong + "'"));
        }

        public int ThanhToanPhong(string TenPhong)
        {
            int KiemTraPhong = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenPhong) FROM Phong WHERE DangSuDung = 1 AND TenPhong = '" + TenPhong + "'"));
            if (KiemTraPhong <= 0)
            {
                return 0;
            }
            else
            {
                int IDHoaDon = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MAX(IDHoaDon) FROM HoaDon WHERE TenPhong = '" + TenPhong + "'"));
                DataProvider.Instance1.ExecuteNonQuery("USP_ThanhToan @IDHoaDon ", new object[] { IDHoaDon });
                DataProvider.Instance1.ExecuteNonQuery("USP_TraPhong @TenPhong ", new object[] { TenPhong });
                return 1;
            }
        }
    }
}
