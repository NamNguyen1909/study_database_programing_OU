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
    // Form for adding or editing a student
    public partial class StudentForm : Form
    {
        private StudentManager manager;
        private int? studentID;
        private TextBox txtFullName, txtAge, txtMajor;
        private Button btnSave, btnCancel;
        private Label lblFullName, lblAge, lblMajor;

        public StudentForm(dynamic student)
        {
            manager = new StudentManager();
            studentID = student?.StudentID;
            InitializeComponents();
            if (student != null)
            {
                txtFullName.Text = student.FullName;
                txtAge.Text = student.Age.ToString();
                txtMajor.Text = student.Major;
                this.Text = "Sửa Sinh Viên";
            }
            else
            {
                this.Text = "Thêm Sinh Viên";
            }
        }

        private void InitializeComponents()
        {
            this.Size = new System.Drawing.Size(300, 250);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Labels
            lblFullName = new Label { Text = "Họ Tên:", Location = new System.Drawing.Point(20, 20), Size = new System.Drawing.Size(80, 20) };
            lblAge = new Label { Text = "Tuổi:", Location = new System.Drawing.Point(20, 60), Size = new System.Drawing.Size(80, 20) };
            lblMajor = new Label { Text = "Chuyên Ngành:", Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(80, 20) };

            // Textboxes
            txtFullName = new TextBox { Location = new System.Drawing.Point(100, 20), Size = new System.Drawing.Size(150, 20) };
            txtAge = new TextBox { Location = new System.Drawing.Point(100, 60), Size = new System.Drawing.Size(150, 20) };
            txtMajor = new TextBox { Location = new System.Drawing.Point(100, 100), Size = new System.Drawing.Size(150, 20) };

            // Buttons
            btnSave = new Button { Text = "Lưu", Location = new System.Drawing.Point(100, 140), Size = new System.Drawing.Size(80, 30) };
            btnCancel = new Button { Text = "Hủy", Location = new System.Drawing.Point(190, 140), Size = new System.Drawing.Size(80, 30) };

            // Event Handlers
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += BtnCancel_Click;

            // Add controls
            this.Controls.AddRange(new Control[] { lblFullName, lblAge, lblMajor, txtFullName, txtAge, txtMajor, btnSave, btnCancel });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtAge.Text) || string.IsNullOrWhiteSpace(txtMajor.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Tuổi phải là số nguyên hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (studentID.HasValue)
            {
                manager.UpdateStudent(studentID.Value, txtFullName.Text, age, txtMajor.Text);
            }
            else
            {
                manager.AddStudent(txtFullName.Text, age, txtMajor.Text);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
