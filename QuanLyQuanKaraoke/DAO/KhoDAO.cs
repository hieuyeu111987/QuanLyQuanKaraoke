using QuanLyQuanKaraoke.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class KhoDAO
    {
        private static KhoDAO Instance;

        public static KhoDAO Instance1
        {
            get { if (Instance == null) Instance = new KhoDAO(); return KhoDAO.Instance; }
            private set { KhoDAO.Instance = value; }
        }

        private KhoDAO() { }

        public DataTable DanhSachKho()
        {
            return DataProvider.Instance1.ExecuteQuery("SELECT Kho.TenKho AS 'Ten kho', SUM(HoaDonNhapHang.SoLuong) AS 'So luong trong kho' FROM Kho LEFT OUTER JOIN HoaDonNhapHang ON Kho.TenKho = HoaDonNhapHang.TenKho GROUP BY Kho.TenKho");
        }

        public int ThemKho(string TenKho)
        {
            int KiemTraTenKho = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenKho) FROM Kho WHERE TenKho = '" + TenKho + "'"));
            if (KiemTraTenKho > 0)
            {
                return 0;
            }
            else
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_ThemKho @TenKho ", new object[] { TenKho });
                return 1;
            }
        }

        public int XoaKho(string TenKho)
        {
            int KiemTraKho = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(TenKho) FROM Kho WHERE TenKho = '" + TenKho + "'"));
            int KiemTraHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT COUNT(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE TenKho = '" + TenKho + "'"));
            if (KiemTraKho < 1)
            {
                return 0;
            }
            else if (KiemTraHoaDonNhapHang == 0)
            {
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaKho @TenKho ", new object[] { TenKho });
                return 1;
            }
            else
            {
                for (int i = 0; i < KiemTraHoaDonNhapHang; i++)
                {
                    int IDHoaDonNhapHang = Convert.ToInt32(DataProvider.Instance1.ExecuteScalar("SELECT MIN(IDHoaDonNhapHang) FROM HoaDonNhapHang WHERE TenKho = '" + TenKho + "'"));
                    DataProvider.Instance1.ExecuteNonQuery("USP_XoaHoaDonNhapHang @IDHoaDonNhapHang ", new object[] { IDHoaDonNhapHang });
                }
                DataProvider.Instance1.ExecuteNonQuery("USP_XoaKho @TenKho ", new object[] { TenKho });
                return 1;
            }
        }

        public List<Kho> ListKho()
        {
            List<Kho> DanhSach = new List<Kho>();
            DataTable Data = DataProvider.Instance1.ExecuteQuery("SELECT * FROM Kho");
            foreach (DataRow item in Data.Rows)
            {
                Kho DanhSachKho = new Kho(item);
                DanhSach.Add(DanhSachKho);
            }
            return DanhSach;
        }

        public int SoLuongDichVuTrongKho(string TenKho, string TenDichVu)
        {
            string SoLuongNhap = (DataProvider.Instance1.ExecuteScalar("SELECT SUM(SoLuong) FROM HoaDonNhapHang WHERE TenKho = '" + TenKho + "' AND TenDichVu = '" + TenDichVu + "'")).ToString();
            string SoLuongXuat = (DataProvider.Instance1.ExecuteScalar("SELECT SUM(SoLuong) FROM HoaDonDichVu WHERE TenDichVu = '" + TenDichVu + "' AND TenKho = '" + TenKho + "'")).ToString();
            
            if (SoLuongNhap == "")
            {
                SoLuongNhap = "0";
            }
            else { }
            if (SoLuongXuat == "")
            {
                SoLuongXuat = "0";
            }
            else { }
            return Convert.ToInt32(SoLuongNhap) - Convert.ToInt32(SoLuongXuat);
        }
    }
}
