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
    public partial class F_QLDonHang : Form
    {
        private bool check;
        public F_QLDonHang()
        {
            InitializeComponent();
            check = false;
            load();
            check = true;
            loadDS();
        }
        private void load()
        {
            loadCbKhachHang();
            loadCbTime();
        }
        private void loadCbKhachHang()
        {
            List<KhachHang> l = KhachHangDAO.Instance.loadDS();
            cbKhachHang.Items.Clear();
            cbKhachHang.Items.Add("Tất cả");
            cbKhachHang.Items.Add("Khách vãn lai");
            foreach (KhachHang i in l)
            {
                cbKhachHang.Items.Add(i.TenKH + " - " + i.Sdt);
            }
            cbKhachHang.Text = "Tất cả";
        }
        private void loadCbTime()
        {
            cbTime.Items.Clear();
            cbTime.Items.Add("Tất cả");
            cbTime.Items.Add("Từ " + dateDuoi.Text + " đến " + dateTren.Text);
            cbTime.Text = "Từ " + dateDuoi.Text + " đến " + dateTren.Text;
        }
        private void loadDS()
        {
            if (check == false)
                return;
            string s = "";
            if (cbKhachHang.Text != "Tất cả")
            {
                if (cbKhachHang.Text != "Khách vãn lai")
                {
                    string kh = cbKhachHang.Text;
                    string sdt = "";
                    for (int i = kh.Length - 1; i >= 0; i--)
                    {
                        if (kh[i] == ' ')
                            break;
                        sdt = kh[i] + sdt;
                    }
                    s += " DonHang.SDTKH=N'" + sdt + "'";
                }
                else s += " DonHang.SDTKH IS NULL";
            }
            if (cbTime.Text != "Tất cả")
            {
                if (string.IsNullOrEmpty(s) == false)
                    s += " AND ";
                s += " DonHang.ThoiGian>='" + DataProvider.Instance.getDateSql((DateTime)dateDuoi.Value) + "'" +
                    " AND DonHang.ThoiGian<='" + DataProvider.Instance.getDateSql((DateTime)dateTren.Value) + "'";
            }
            if (string.IsNullOrEmpty(s) == false)
                s = "WHERE " + s;
            dgvDonHang.Rows.Clear();
            tbMaDH.Text = "";
            int stt = 0;
            List<DonHang> l = DonHangDAO.Instance.loadDSTheoDieuKien(s);
            int tongHD = 0, tongGiamGia=0;
            foreach (DonHang i in l)
            {
                DataGridViewRow row = (DataGridViewRow)dgvDonHang.Rows[0].Clone();
                stt++;
                row.Cells[0].Value = stt + "";
                row.Cells[1].Value = i.MaDH;
                KhachHang kh = KhachHangDAO.Instance.getBySDT(i.SdtKH);
                if (kh == null)
                {
                    row.Cells[2].Value = "Trống";
                    row.Cells[3].Value = "Khách vãn lai";
                }
                else
                {
                    row.Cells[2].Value = kh.Sdt;
                    row.Cells[3].Value = kh.TenKH;
                }
                string maNV = i.MaNV;
                if (string.IsNullOrEmpty(maNV))
                    maNV = "admin";
                row.Cells[4].Value = maNV;
                row.Cells[5].Value = i.ThoiGian.ToShortDateString();
                row.Cells[6].Value = DataProvider.Instance.getDinhDanhHangNghin(i.TongTien) + " VNĐ";
                row.Cells[7].Value = i.GiamGia;
                row.Cells[8].Value = i.TrangThai;
                dgvDonHang.Rows.Add(row);
                tongHD += i.TongTien;
                tongGiamGia += i.GiamGia;
            }
            tbSoDon.Text = l.Count + "";
            tbTongGiamGia.Text = DataProvider.Instance.getDinhDanhHangNghin(tongGiamGia) + " VNĐ";
            tbTongDoanhThu.Text = DataProvider.Instance.getDinhDanhHangNghin(tongHD-tongGiamGia)+" VNĐ";
            tbTongHoaDon.Text = DataProvider.Instance.getDinhDanhHangNghin(tongHD)+" VNĐ";
        }
        private void button11_Click(object sender, EventArgs e)
        {
            DonHang dh = DonHangDAO.Instance.getByMa(tbMaDH.Text);
            if (dh == null)
            {
                MessageBox.Show("Hãy chọn đơn hàng cần hủy trước !", "Nhắc nhở");
                return;
            }
            if (MessageBox.Show("Xác nhận hủy đơn hàng " + dh.MaDH + " ?", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DonHangDAO.Instance.huy(dh.MaDH);
                loadDS();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            DonHang dh = DonHangDAO.Instance.getByMa(tbMaDH.Text);
            if (dh == null)
            {
                MessageBox.Show("Hãy chọn đơn hàng xem chi tiết trước !", "Nhắc nhở");
                return;
            }
            F_Bill f = new F_Bill(dh.MaDH);
            f.ShowDialog();
        }

        private void dgvDonHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                DataGridViewRow row = new DataGridViewRow();
                row = dgvDonHang.Rows[e.RowIndex];
                tbMaDH.Text = Convert.ToString(row.Cells[1].Value);
                DonHang dh = DonHangDAO.Instance.getByMa(tbMaDH.Text);
            }
            catch (Exception)
            { }
        }

        private void dateDuoi_ValueChanged(object sender, EventArgs e)
        {
            loadCbTime();
            loadDS();
        }

        private void dateTren_ValueChanged(object sender, EventArgs e)
        {
            loadCbTime();
            loadDS();
        }

        private void cbSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDS();
        }

        private void cbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDS();
        }

        private void cbTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDS();
        }
    }
}
