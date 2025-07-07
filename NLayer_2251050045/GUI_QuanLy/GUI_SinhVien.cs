using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DTO_QuanLy;
using BUS_QuanLy;

namespace GUI_QuanLy
{
    public partial class GUI_SinhVien : Form
    {
        BUS_SinhVien busSV = new BUS_SinhVien();
        public GUI_SinhVien()
        {
            InitializeComponent();
            this.Load += GUI_SinhVien_Load;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text != "" && txtName.Text != "" && txtSDT.Text != "")
            {
                // Tao DTO
                DTO_SinhVien tv = new DTO_SinhVien(0, txtName.Text, txtSDT.Text, txtEmail.Text); // Vi ID tu tang nen de ID so gi cung dc
                // Them
                if (busSV.themSinhVien(tv))
                {
                    MessageBox.Show("Thêm thành công");
                    dgvSV.DataSource = busSV.getSinhVien(); // refresh datagridview
                    txtEmail.Clear();
                    txtName.Clear();
                    txtSDT.Clear();
                }
                else
                {
                    MessageBox.Show("Thêm ko thành công");
                }
            }
            else
            {
                MessageBox.Show("Xin hãy nhập đầy đủ");
            }
        }
        private void GUI_SinhVien_Load(object sender, EventArgs e)
        {
            dgvSV.DataSource = busSV.getSinhVien(); // get Sinh viên
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có chọn table rồi
            if (dgvSV.SelectedRows.Count > 0)
            {
                if (txtEmail.Text != "" && txtName.Text != "" && txtSDT.Text != "")
                {
                    // Lấy row hiện tại
                    DataGridViewRow row = dgvSV.SelectedRows[0];
                    int ID = Convert.ToInt16(row.Cells[0].Value.ToString());
                    // Tạo DTO
                    DTO_SinhVien tv = new DTO_SinhVien(ID, txtName.Text, txtSDT.Text, txtEmail.Text);
                    // Sửa
                    if (busSV.suaSinhVien(tv))
                    {
                        MessageBox.Show("Sửa thành công");
                        dgvSV.DataSource = busSV.getSinhVien(); // refresh datagridview
                    }
                    else
                    {
                        MessageBox.Show("Sửa ko thành công");
                    }
                }
                else
                {
                    MessageBox.Show("Xin hãy nhập đầy đủ");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn thành viên muốn sửa");
            }
        }

        private void dgvSV_Click(object sender, EventArgs e)
        {
            // Lấy row hiện tại
            DataGridViewRow row = dgvSV.SelectedRows[0];
            // Chuyển giá trị lên form
            txtName.Text = row.Cells[1].Value.ToString();
            txtSDT.Text = row.Cells[2].Value.ToString();
            txtEmail.Text = row.Cells[3].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu có chọn table rồi
            if (dgvSV.SelectedRows.Count > 0)
            {
                // Lấy row hiện tại
                DataGridViewRow row = dgvSV.SelectedRows[0];
                int ID = Convert.ToInt16(row.Cells[0].Value.ToString());
                // Xóa
                if (busSV.xoaSinhVien(ID))
                {
                    MessageBox.Show("Xóa thành công");
                    dgvSV.DataSource = busSV.getSinhVien(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Xóa ko thành công");
                }
            }
            else
            {
                MessageBox.Show("Hãy chọn thành viên muốn xóa");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            DataTable dt = busSV.getSinhVien();

            if (!string.IsNullOrEmpty(keyword))
            {
                // Lọc dữ liệu theo tên, sdt hoặc email
                var filteredRows = dt.AsEnumerable()
                    .Where(row =>
                        row.Field<string>("SV_NAME").ToLower().Contains(keyword) ||
                        row.Field<string>("SV_PHONE").ToLower().Contains(keyword) ||
                        row.Field<string>("SV_EMAIL").ToLower().Contains(keyword)
                    );

                if (filteredRows.Any())
                    dgvSV.DataSource = filteredRows.CopyToDataTable();
                else
                    dgvSV.DataSource = null;
            }
            else
            {
                dgvSV.DataSource = dt;
            }
        }

        private void dgvSV_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSV.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dgvSV.SelectedRows[0];
                if (row.Cells[1].Value != null) txtName.Text = row.Cells[1].Value.ToString();
                if (row.Cells[2].Value != null) txtSDT.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value != null) txtEmail.Text = row.Cells[3].Value.ToString();
            }
        }
    }
}
