using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class DaNghi
    {
        public DaNghi(bool NhanVienDaNghi)
        {
            this.NhanVienDaNghi1 = NhanVienDaNghi;
        }

        public DaNghi(DataRow row)
        {
            this.NhanVienDaNghi1 = (bool)row["DaNghi"];
        }

        private bool NhanVienDaNghi;

        public bool NhanVienDaNghi1
        {
            get { return NhanVienDaNghi; }
            set { NhanVienDaNghi = value; }
        }
    }
}
