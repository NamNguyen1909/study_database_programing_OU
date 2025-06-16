using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManagement
{
    // Main Form for displaying students and navigation
    public partial class MainForm : Form
    {
        private StudentManager manager;
        private DataGridView dgvStudents;
        private Button btnAdd, btnUpdate, btnDelete, btnExit;
        private int selectedStudentID = -1;

        public MainForm()
        {
            manager = new StudentManager();

            InitializeComponents();
            LoadStudents();
        }

        private void InitializeComponents()
        {
            this.Text = "Quản Lý Sinh Viên";
            this.Size = new System.Drawing.Size(600, 400);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;

            // DataGridView
            dgvStudents = new DataGridView
            {
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(540, 250),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false
            };
            dgvStudents.SelectionChanged += DgvStudents_SelectionChanged;

            // Buttons
            btnAdd = new Button { Text = "Thêm Sinh Viên", Location = new System.Drawing.Point(20, 290), Size = new System.Drawing.Size(100, 30) };
            btnUpdate = new Button { Text = "Sửa Sinh Viên", Location = new System.Drawing.Point(140, 290), Size = new System.Drawing.Size(100, 30), Enabled = false };
            btnDelete = new Button { Text = "Xóa Sinh Viên", Location = new System.Drawing.Point(260, 290), Size = new System.Drawing.Size(100, 30), Enabled = false };
            btnExit = new Button { Text = "Thoát", Location = new System.Drawing.Point(460, 290), Size = new System.Drawing.Size(100, 30) };

            // Event Handlers
            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            btnExit.Click += BtnExit_Click;

            // Add controls
            this.Controls.AddRange(new Control[] { dgvStudents, btnAdd, btnUpdate, btnDelete, btnExit });
        }

        private void LoadStudents()
        {
            dgvStudents.DataSource = manager.GetStudents();
            selectedStudentID = -1;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void DgvStudents_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                selectedStudentID = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["StudentID"].Value);
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
            else
            {
                selectedStudentID = -1;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            StudentForm studentForm = new StudentForm(null); // Add mode
            if (studentForm.ShowDialog() == DialogResult.OK)
            {
                LoadStudents();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == -1) return;

            DataRow row = ((DataTable)dgvStudents.DataSource).AsEnumerable()
                .FirstOrDefault(r => r.Field<int>("StudentID") == selectedStudentID);
            if (row != null)
            {
                var student = new
                {
                    StudentID = selectedStudentID,
                    FullName = row["FullName"].ToString(),
                    Age = Convert.ToInt32(row["Age"]),
                    Major = row["Major"].ToString()
                };
                StudentForm studentForm = new StudentForm(student); // Edit mode
                if (studentForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (selectedStudentID == -1) return;

            if (MessageBox.Show("Bạn có chắc muốn xóa sinh viên này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                manager.DeleteStudent(selectedStudentID);
                LoadStudents();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
