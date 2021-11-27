using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class ChucVu
    {
        public ChucVu(string TenChucVu, int SoLuong)
        {
            this.TenChucVu1 = TenChucVu;
        }

        public ChucVu(DataRow row)
        {
            this.TenChucVu1 = row["ChucVu"].ToString();
        }

        private string TenChucVu;

        public string TenChucVu1
        {
            get { return TenChucVu; }
            set { TenChucVu = value; }
        }
    }
}
