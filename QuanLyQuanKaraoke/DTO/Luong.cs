using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class Luong
    {
        public Luong(int LuongNhanVien)
        {
            this.LuongNhanVien1 = LuongNhanVien;
        }

        public Luong(DataRow row)
        {
            this.LuongNhanVien1 = (int)row["Luong"];
        }

        private int LuongNhanVien;

        public int LuongNhanVien1
        {
            get { return LuongNhanVien; }
            set { LuongNhanVien = value; }
        }
    }
}
