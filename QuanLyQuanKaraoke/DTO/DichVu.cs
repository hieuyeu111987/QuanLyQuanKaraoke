using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class DichVu
    {
        public DichVu(string TenDichVu)
        {
            this.TenDichVu = TenDichVu;
        }

        public DichVu(DataRow row)
        {
            this.TenDichVu = row["TenDichVu"].ToString();
        }

        private string tenDichVu;

        public string TenDichVu
        {
            get { return tenDichVu; }
            set { tenDichVu = value; }
        }
    }
}
