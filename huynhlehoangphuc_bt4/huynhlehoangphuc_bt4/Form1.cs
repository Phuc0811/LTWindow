using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace huynhlehoangphuc_bt4
{
    public partial class Form1 : Form
    {
        private List<NhanVienModel> employees = new List<NhanVienModel>();

        public Form1()
        {
            InitializeComponent();
            InitializeListView();
        }

        private void InitializeListView()
        {
            listView1.View = View.Details;
            listView1.Columns.Add("MSNV");
            listView1.Columns.Add("Tên nhân viên");
            listView1.Columns.Add("Lương cơ bản");
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var nhanVienForm = new NhanVien();
            if (nhanVienForm.ShowDialog() == DialogResult.OK)
            {
                var nhanVien = nhanVienForm.Nhanvien;
                employees.Add(nhanVien);
                UpdateListView();
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                var nhanVien = employees.Find(emp => emp.Id == selectedItem.Text);

                var nhanVienForm = new NhanVien(nhanVien);
                if (nhanVienForm.ShowDialog() == DialogResult.OK)
                {
                    var updatedNhanVien = nhanVienForm.Nhanvien;
                    employees.Remove(nhanVien);
                    employees.Add(updatedNhanVien);
                    UpdateListView();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sữa.");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xóa thông tin", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
            if (listView1.SelectedItems.Count > 0)
            {
                var selectedItem = listView1.SelectedItems[0];
                var nhanVien = employees.Find(emp => emp.Id == selectedItem.Text);
                employees.Remove(nhanVien);
                UpdateListView();
            }
            
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
            }
        }

        private void UpdateListView()
        {
            listView1.Items.Clear();
            foreach (var nv in employees)
            {
                var item = new ListViewItem(nv.Id);
                item.SubItems.Add(nv.Name);
                item.SubItems.Add(nv.Salary.ToString());
                listView1.Items.Add(item);
            }
        }
    }
}


