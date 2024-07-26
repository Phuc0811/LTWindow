using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace huynhlehoangphuc_bt4
{
    public partial class NhanVien : Form
    {
        public NhanVienModel Nhanvien { get; private set; }

        public NhanVien()
        {
            InitializeComponent();
        }

        public NhanVien(NhanVienModel nhanVien) : this()
        {
            Nhanvien = nhanVien;
            txtMSNV.Text = nhanVien.Id;
            txtTenNV.Text = nhanVien.Name;
            txtLuong.Text = nhanVien.Salary.ToString();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMSNV.Text) || string.IsNullOrEmpty(txtTenNV.Text) || !decimal.TryParse(txtLuong.Text, out var salary))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            Nhanvien = new NhanVienModel
            {
                Id = txtMSNV.Text,
                Name = txtTenNV.Text,
                Salary = salary
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class NhanVienModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }
}
