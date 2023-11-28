using HTQLKhachHangThanThiet.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTQLKhachHangThanThiet.DAO
{
    public class HangDAO
    {
        private static HangDAO instance;
        public static HangDAO Instance
        {
            get { if (instance == null) instance = new HangDAO(); return instance; }
            private set { instance = value; }
        }
        private HangDAO() { }
        public List<Hang> loadDS()
        {
            List<Hang> l = new List<Hang>();
            DataTable data = DataProvider.Instance.RunQuery("SELECT*FROM Hang");
            foreach (DataRow item in data.Rows)
            {
                Hang i = new Hang(item);
                l.Add(i);
            }
            return l;
        }
        public Hang getByMa(string ma)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT*FROM Hang WHERE MaHang=N'" + ma + "'");
            foreach (DataRow item in data.Rows)
            {
                Hang l = new Hang(item);
                return l;
            }
            return null;
        }
        public Hang getByTen(string ten)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT*FROM Hang WHERE TenHang=N'" + ten + "'");
            foreach (DataRow item in data.Rows)
            {
                Hang l = new Hang(item);
                return l;
            }
            return null;
        }
        public Hang getMaxByTichLuy(int tichLuy)
        {
            DataTable data = DataProvider.Instance.RunQuery("SELECT * FROM Hang WHERE DieuKien<="+tichLuy+ " ORDER BY DieuKien DESC");
            foreach (DataRow item in data.Rows)
            {
                Hang l = new Hang(item);
                return l;
            }
            return null;
        }
        public void them(string ten,int dieuKien,int uuDai)
        {
            DataProvider.Instance.RunQuery("INSERT Hang(TenHang,DieuKien,UuDai) VALUES(N'" + ten + "',"+dieuKien+","+uuDai+")");
            KhachHangDAO.Instance.updateHang();
        }
        public void sua(string ma,string ten, int dieuKien, int uuDai)
        {
            DataProvider.Instance.RunQuery("UPDATE Hang SET TenHang=N'" + ten + "',DieuKien="+dieuKien+",UuDai="+uuDai+" WHERE MaHang=N'" + ma + "'");
            KhachHangDAO.Instance.updateHang();
        }
        public void xoa(string ma)
        {
            DataProvider.Instance.RunQuery("UPDATE KhachHang SET MaHang=N'RANK0001' WHERE MaHang=N'"+ma+"'");
            DataProvider.Instance.RunQuery("DELETE FROM Hang WHERE MaHang = N'" + ma + "'");
            KhachHangDAO.Instance.updateHang();
        }
    }
}
