using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class NamSinh
    {
        public NamSinh(int NamSinhNhanVien)
        {
            this.NamSinhNhanVien1 = NamSinhNhanVien;
        }

        public NamSinh(DataRow row)
        {
            this.NamSinhNhanVien1 = (int)row["NamSinh"];
        }

        private int NamSinhNhanVien;

        public int NamSinhNhanVien1
        {
            get { return NamSinhNhanVien; }
            set { NamSinhNhanVien = value; }
        }
    }
}
