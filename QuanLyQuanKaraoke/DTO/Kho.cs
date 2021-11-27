using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyQuanKaraoke.DTO
{
    public class Kho
    {
        public Kho(string TenKho)
        {
            this.TenKho = TenKho;
        }

        public Kho(DataRow row)
        {
            this.TenKho = row["TenKho"].ToString();
        }

        private string tenKho;

        public string TenKho
        {
            get { return tenKho; }
            set { tenKho = value; }
        }
    }
}
