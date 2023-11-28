using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTQLKhachHangThanThiet.DTO
{
    public class KhachHang
    {
        private string sdt;
        private string maHang;
        private string tenKH;
        private string diaChi;
        private int tichLuy;
        public KhachHang()
        {

        }
        public KhachHang(DataRow d)
        {
            TenKH = d["TenKH"].ToString().Trim();
            MaHang = d["MaHang"].ToString();
            Sdt = d["SDT"].ToString();
            DiaChi = d["DiaChi"].ToString().Trim();
            TichLuy = (int)d["TichLuy"];
        }

        public string Sdt { get => sdt; set => sdt = value; }
        public string MaHang { get => maHang; set => maHang = value; }
        public string TenKH { get => tenKH; set => tenKH = value; }
        public string DiaChi { get => diaChi; set => diaChi = value; }
        public int TichLuy { get => tichLuy; set => tichLuy = value; }
    }
}
