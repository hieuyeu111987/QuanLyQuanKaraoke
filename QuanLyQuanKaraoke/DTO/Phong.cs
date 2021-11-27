using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class Phong
    {
        private string tenPhong;
        private int soNguoiToiThieu;
        private int soNguoiToiDa;

        public Phong(string TenPhong, int SoNguoiToiThieu, int SoNguoiToiDa)
        {
            this.TenPhong = TenPhong;
            this.SoNguoiToiThieu = SoNguoiToiThieu;
            this.SoNguoiToiDa = SoNguoiToiDa;
        }

        public Phong(DataRow row)
        {
            this.TenPhong = row["TenPhong"].ToString();
            this.SoNguoiToiThieu = (int)row["SoNguoiToiThieu"];
            this.SoNguoiToiDa = (int)row["SoNguoiToiDa"];
        }

        public int SoNguoiToiDa
        {
            get { return soNguoiToiDa; }
            set { soNguoiToiDa = value; }
        }

        public int SoNguoiToiThieu
        {
            get { return soNguoiToiThieu; }
            set { soNguoiToiThieu = value; }
        }

        public string TenPhong
        {
            get { return tenPhong; }
            set { tenPhong = value; }
        }
    }
}
