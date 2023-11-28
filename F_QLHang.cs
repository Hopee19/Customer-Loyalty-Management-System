using HTQLKhachHangThanThiet.DAO;
using HTQLKhachHangThanThiet.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTQLKhachHangThanThiet
{
    public partial class F_QLHang : Form
    {
        public F_QLHang(bool isAdmin)
        {
            InitializeComponent();
            load();
            setButton(isAdmin);
        }

        private void setButton(bool isAdmin)
        {
            btThem.Enabled = isAdmin;
            btXoa.Enabled = isAdmin;
            btCapNhat.Enabled = isAdmin;
        }
        private void load()
        {
            List<Hang> l = HangDAO.Instance.loadDS();
            dgvHang.Rows.Clear();
            int stt = 0;
            foreach (Hang i in l)
            {
                int soKH = KhachHangDAO.Instance.demByMaHang(i.Ma);
                stt++;
                dgvHang.Rows.Add(stt, i.Ma, i.Ten, DataProvider.Instance.getDinhDanhHangNghin(i.DieuKien) + " VNĐ", i.UuDai, soKH);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hang h = HangDAO.Instance.getByMa(tbMa.Text);
            if (h == null)
            {
                MessageBox.Show("Hãy chọn hạng cần xóa !");
                return;
            }
            if (h.Ma=="RANK0001")
            {
                MessageBox.Show("Không thể xóa hạng này !");
                return;
            }
            if (MessageBox.Show("Xác nhận xóa hạng ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                HangDAO.Instance.xoa(tbMa.Text);
                load();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(tbTen.Text))
            {
                MessageBox.Show("Tên hạng không được để trống !");
                return;
            }
            if(HangDAO.Instance.getByTen(tbTen.Text)!=null)
            {
                MessageBox.Show("Tên hạng đã tồn tại !");
                return;
            } 
            HangDAO.Instance.them(tbTen.Text,(int)nudDieuKien.Value,(int)nudUuDai.Value);
            load();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hang h = HangDAO.Instance.getByMa(tbMa.Text);
            if (h==null)
            {
                MessageBox.Show("Hãy chọn hạng cần cập nhật !");
                return;
            }
            if (h.Ma == "RANK0001")
            {
                nudDieuKien.Value = 0;
            }
            if (string.IsNullOrEmpty(tbTen.Text))
            {
                MessageBox.Show("Tên hạng không được để trống !");
                return;
            }
            Hang h1 = HangDAO.Instance.getByTen(tbTen.Text);
            if(h1!=null&&h1.Ma!=h.Ma)
            {
                MessageBox.Show("Tên hạng đã tồn tại !");
                return;
            }
            HangDAO.Instance.sua(h.Ma, tbTen.Text, (int)nudDieuKien.Value, (int)nudUuDai.Value);
            load();
        }

        private void dgvHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridViewRow r = dgvHang.Rows[e.RowIndex];
                tbMa.Text = r.Cells[1].Value.ToString();
                Hang h = HangDAO.Instance.getByMa(tbMa.Text);
                if (h == null)
                    return;
                tbTen.Text = h.Ten;
                nudUuDai.Value = h.UuDai;
                nudDieuKien.Value = h.DieuKien;
            }catch (Exception ex) { }
        }
    }
}
