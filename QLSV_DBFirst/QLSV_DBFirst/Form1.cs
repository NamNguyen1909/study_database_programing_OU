using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.Entity;   

namespace QLSV_DBFirst
{
    public partial class Form1 : Form
    {
        QLSVEntities1 db = new QLSVEntities1();

        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            dgvSinhvien.DataSource = db.SinhVien.ToList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            SinhVien sv = new SinhVien
            {
                SV_Name = txtName.Text,
                SV_Phone = txtPhone.Text,
                SV_Email = txtEmail.Text
            };
            db.SinhVien.Add(sv);
            db.SaveChanges();
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvSinhvien.CurrentRow == null)
                return;

            int id = Convert.ToInt32(dgvSinhvien.CurrentRow.Cells["SV_ID"].Value);
            SinhVien sv = db.SinhVien.Find(id);
            if (sv != null)
            {
                sv.SV_Name = txtName.Text;
                sv.SV_Phone = txtPhone.Text;
                sv.SV_Email = txtEmail.Text;
                db.SaveChanges();
                LoadData();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSinhvien.CurrentRow == null)
                return;

            int id = Convert.ToInt32(dgvSinhvien.CurrentRow.Cells["SV_ID"].Value);
            SinhVien sv = db.SinhVien.Find(id);
            if (sv != null)
            {
                db.SinhVien.Remove(sv);
                db.SaveChanges();
                LoadData();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dgvSinhvien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSinhvien.CurrentRow != null)
            {
                txtName.Text = dgvSinhvien.CurrentRow.Cells["SV_Name"].Value?.ToString();
                txtPhone.Text = dgvSinhvien.CurrentRow.Cells["SV_Phone"].Value?.ToString();
                txtEmail.Text = dgvSinhvien.CurrentRow.Cells["SV_Email"].Value?.ToString();
            }
        }
    }
}
