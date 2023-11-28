using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;

namespace HTQLKhachHangThanThiet.DTO
{
    public class Hang
    {
        private string ma;
        private string ten;
        private int dieuKien;
        private int uuDai;
        public Hang() { }
        public Hang(DataRow d)
        {
            Ma = d["MaHang"].ToString();
            Ten = d["TenHang"].ToString();
            DieuKien = (int)d["DieuKien"];
            UuDai = (int)d["UuDai"];
        }

        public string Ma { get => ma; set => ma = value; }
        public string Ten { get => ten; set => ten = value; }
        public int DieuKien { get => dieuKien; set => dieuKien = value; }
        public int UuDai { get => uuDai; set => uuDai = value; }
    }
}
