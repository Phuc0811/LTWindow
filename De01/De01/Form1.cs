using De01.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace De01
{
    public partial class frmSinhvien : Form
    {
        public frmSinhvien()
        {
            InitializeComponent();
        }

        private QuanLySVContext dbContext;

        private void frmSinhvien_Load(object sender, EventArgs e)
        {
            dbContext = new QuanLySVContext();
            LoadDanhSachSinhvien();
            LoadDanhSachLop();
        }

        private void LoadDanhSachSinhvien()
        {
            var sinhvienList = dbContext.Sinhviens.Include(sv => sv.Lop).ToList();
            lvSinhvien.Items.Clear();
            foreach (var sv in sinhvienList)
            {
                ListViewItem item = new ListViewItem(sv.MaSV);
                item.SubItems.Add(sv.HoTenSV);
                item.SubItems.Add(sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : string.Empty);
                item.SubItems.Add(sv.Lop?.TenLop ?? "");
                lvSinhvien.Items.Add(item);
            }
        }

        private void LoadDanhSachLop()
        {
            var lopList = dbContext.Lops.ToList();
            cboLop.Items.Clear();
            foreach (var lop in lopList)
            {
                cboLop.Items.Add(lop.TenLop);
            }
        }

        private void btLuu_Click(object sender, EventArgs e)
        {
            var sinhvien = dbContext.Sinhviens.Find(txtMaSV.Text);
            if (sinhvien == null)
            {
                var selectedLop = dbContext.Lops.FirstOrDefault(l => l.TenLop == cboLop.SelectedItem.ToString());
                sinhvien = new Sinhvien
                {
                    MaSV = txtMaSV.Text,
                    HoTenSV = txtHotenSV.Text,
                    NgaySinh = dtNgaysinh.Value,
                    MaLop = selectedLop?.MaLop
                };

                dbContext.Sinhviens.Add(sinhvien);
                dbContext.SaveChanges();
                LoadDanhSachSinhvien();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Mã sinh viên này đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            var sinhvien = dbContext.Sinhviens.FirstOrDefault(sv => sv.MaSV == txtMaSV.Text);
            if (sinhvien != null)
            {
                var selectedLop = dbContext.Lops.FirstOrDefault(l => l.TenLop == cboLop.SelectedItem.ToString());

                sinhvien.HoTenSV = txtHotenSV.Text;
                sinhvien.NgaySinh = dtNgaysinh.Value;
                sinhvien.MaLop = selectedLop.MaLop;
                sinhvien.Lop = selectedLop;

                dbContext.SaveChanges();
                LoadDanhSachSinhvien();
                ClearForm();
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này không?", "Xác nhận xóa", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var sinhvien = dbContext.Sinhviens.FirstOrDefault(sv => sv.MaSV == txtMaSV.Text);
                if (sinhvien != null)
                {
                    dbContext.Sinhviens.Remove(sinhvien);
                    dbContext.SaveChanges();
                    LoadDanhSachSinhvien();
                    ClearForm();
                }
            }
        }

        private void btTim_Click(object sender, EventArgs e)
        {
            var searchTerm = txtSearch.Text.ToLower();
            var filteredList = dbContext.Sinhviens
                                .Include(sv => sv.Lop)
                                .Where(sv => sv.HoTenSV.ToLower().Contains(searchTerm))
                                .ToList();

            lvSinhvien.Items.Clear();
            foreach (var sv in filteredList)
            {
                ListViewItem item = new ListViewItem(sv.MaSV);
                item.SubItems.Add(sv.HoTenSV);
                item.SubItems.Add(sv.NgaySinh.HasValue ? sv.NgaySinh.Value.ToString("dd/MM/yyyy") : string.Empty);
                item.SubItems.Add(sv.Lop?.TenLop ?? "");
                lvSinhvien.Items.Add(item);
            }
        }

        private void btThem_Click(object sender, EventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(txtMaSV.Text) ||
                string.IsNullOrWhiteSpace(txtHotenSV.Text) ||
                cboLop.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedLop = dbContext.Lops.FirstOrDefault(l => l.TenLop == cboLop.SelectedItem.ToString());
            var sinhVien = new Sinhvien
            {
                MaSV = txtMaSV.Text,
                HoTenSV = txtHotenSV.Text,
                NgaySinh = dtNgaysinh.Value,
                MaLop = selectedLop?.MaLop
            };

      
            var existingSinhVien = dbContext.Sinhviens.FirstOrDefault(sv => sv.MaSV == sinhVien.MaSV);
            if (existingSinhVien != null)
            {
                MessageBox.Show("Mã sinh viên này đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            
            dbContext.Sinhviens.Add(sinhVien);
            dbContext.SaveChanges();

           
            LoadDanhSachSinhvien();
            ClearForm();
        }

        private void btKhong_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void lvSinhvien_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvSinhvien.SelectedItems.Count > 0)
            {
                var selectedItem = lvSinhvien.SelectedItems[0];
                txtMaSV.Text = selectedItem.SubItems[0].Text;
                txtHotenSV.Text = selectedItem.SubItems[1].Text;
                if (DateTime.TryParseExact(selectedItem.SubItems[2].Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime ngaysinh))
                {
                    dtNgaysinh.Value = ngaysinh;
                }
                else
                {
                    dtNgaysinh.Value = DateTime.Now;
                }
                cboLop.SelectedItem = selectedItem.SubItems[3].Text;
            }
        }

        private void ClearForm()
        {
            txtMaSV.Clear();
            txtHotenSV.Clear();
            dtNgaysinh.Value = DateTime.Now;
            cboLop.SelectedIndex = -1;
        }

        private void btThoat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
       "Bạn có chắc chắn muốn đóng ứng dụng không?",
       "Xác nhận thoát",
       MessageBoxButtons.YesNo,
       MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
         
                this.Close();
            }
            
        }
    }
}
