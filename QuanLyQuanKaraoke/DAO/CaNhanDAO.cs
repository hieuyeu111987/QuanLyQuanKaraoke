using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DAO
{
    public class CaNhanDAO
    {
        private static CaNhanDAO Instance;

        public static CaNhanDAO Instance1
        {
            get { if (Instance == null) Instance = new CaNhanDAO(); return CaNhanDAO.Instance; }
            private set { CaNhanDAO.Instance = value; }
        }

        private CaNhanDAO() { }

        private string CMND;

        public string GetCMND()
        {
            return CMND;
        }

        public string MaHoaMatKhau(string MatKhau)
        {
            byte[] MaHoa1 = ASCIIEncoding.ASCII.GetBytes(MatKhau);
            byte[] MaHoa2 = new MD5CryptoServiceProvider().ComputeHash(MaHoa1);
            StringBuilder matKhauMaHoa = new StringBuilder();
            for (int i = 0; i < MaHoa2.Length; i++)
            {
                matKhauMaHoa.Append(MaHoa2[i].ToString("X2"));
            }
            return matKhauMaHoa.ToString();
        }

        public string DangNhap(string TaiKhoan, string MatKhau)
        {
            string MatKhauDaMaHoa = MaHoaMatKhau(MatKhau);
            int SoLuongTaiKhoan = Convert.ToInt32((DataProvider.Instance1.ExecuteScalar("USP_DangNhap @taiKhoan , @matKhau ", new object[] { TaiKhoan, MatKhauDaMaHoa })));
            if (SoLuongTaiKhoan == 1)
            {
                CMND = (DataProvider.Instance1.ExecuteScalar("SELECT CMND FROM TaiKhoan WHERE TenTaiKhoan = '" + TaiKhoan + "' AND MatKhau = '" + MatKhauDaMaHoa + "'")).ToString();
                string ChucVu = (DataProvider.Instance1.ExecuteScalar("SELECT ChucVu FROM NhanVien WHERE CMND = '" + CMND + "'")).ToString();
                return ChucVu;
            }
            else
            {
                return "0";
            }
        }

        public string ChuoiNgay(string Ngay, string Thang, string Nam)
        {
            string NgayThangNam = Nam;
            if (Thang.Length == 1)
            {
                NgayThangNam = NgayThangNam + "0" + Thang;
            }
            else
            {
                NgayThangNam = NgayThangNam + Thang;
            }
            if (Ngay.Length == 1)
            {
                NgayThangNam = NgayThangNam + "0" + Ngay;
            }
            else
            {
                NgayThangNam = NgayThangNam + Ngay;
            }
            return NgayThangNam;
        }

        public string GetTenNguoiDung()
        {
            return (DataProvider.Instance1.ExecuteScalar("SELECT TenNhanVien FROM NhanVien WHERE CMND = '" + CMND + "'")).ToString();
        }

        public string GetTenTaiKhoan()
        {
            return (DataProvider.Instance1.ExecuteScalar("SELECT TenTaiKhoan FROM TaiKhoan WHERE CMND = '" + CMND + "'")).ToString();
        }

        public int DoiMatKhau(string MatKhauMoi, string NhapLaiMatKhauMoi)
        {
            if (MatKhauMoi != NhapLaiMatKhauMoi)
            {
                return 1;
            }
            else
            {
                string MatKhauDaMaHoa = MaHoaMatKhau(MatKhauMoi);
                DataProvider.Instance1.ExecuteNonQuery("USP_DoiMatKhau @MatKhau , @CMND ", new object[] { MatKhauDaMaHoa, CMND });
                return 0;
            }
        }
    }
}
