using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Bai2
{
    public partial class Form1 : Form
    {
        private string connectionString;
        private DataTable dataTable;

        public Form1()
        {
            InitializeComponent();
            ConfigureDataGridView();
            InitializeDatabase();
            LoadData();
            PopulateComboBoxes();
            dataGridView1.SelectionChanged += new EventHandler(dataGridView1_SelectionChanged);
            txtTimtenSP.TextChanged += new EventHandler(txtTimtenSP_TextChanged);
        }

        private void ConfigureDataGridView()
        {
            // Đảm bảo các cột đã được định nghĩa trong Designer được liên kết đúng với dữ liệu
            colProductID.DataPropertyName = "ProductID";
            colProductName.DataPropertyName = "ProductName";
            colUnitPrice.DataPropertyName = "UnitPrice";
            colQuantityPerUnit.DataPropertyName = "QuantityPerUnit";
            colCategoryName.DataPropertyName = "CategoryName";
            colCompanyName.DataPropertyName = "CompanyName";

            // Tắt tự động tạo cột để tránh thêm cột mới
            dataGridView1.AutoGenerateColumns = false;
        }

        private void InitializeDatabase()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["QLSP"]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("Không tìm thấy chuỗi kết nối 'QLSP' trong tệp cấu hình.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khởi tạo kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        private void LoadData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        SELECT p.ProductID, p.ProductName, p.UnitPrice, p.QuantityPerUnit, 
                               c.CategoryName, s.CompanyName
                        FROM Products p
                        JOIN Categories c ON p.CategoryID = c.CategoryID
                        JOIN Suppliers s ON p.SupplierID = s.SupplierID";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGridView1.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PopulateComboBoxes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Populate Categories combo box
                    string categoryQuery = "SELECT CategoryID, CategoryName FROM Categories";
                    SqlDataAdapter categoryAdapter = new SqlDataAdapter(categoryQuery, connection);
                    DataTable categoryTable = new DataTable();
                    categoryAdapter.Fill(categoryTable);
                    cbLoaiSP.DataSource = categoryTable;
                    cbLoaiSP.DisplayMember = "CategoryName";
                    cbLoaiSP.ValueMember = "CategoryID";
                    cbLoaiSP.SelectedIndex = -1;

                    // Populate Suppliers combo box
                    string supplierQuery = "SELECT SupplierID, CompanyName FROM Suppliers";
                    SqlDataAdapter supplierAdapter = new SqlDataAdapter(supplierQuery, connection);
                    DataTable supplierTable = new DataTable();
                    supplierAdapter.Fill(supplierTable);
                    comboBox1.DataSource = supplierTable;
                    comboBox1.DisplayMember = "CompanyName";
                    comboBox1.ValueMember = "SupplierID";
                    comboBox1.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu vào combobox: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenSP.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSoluong.Text) || !int.TryParse(txtSoluong.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ (số nguyên dương).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoluong.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDongia.Text) || !decimal.TryParse(txtDongia.Text, out decimal unitPrice) || unitPrice <= 0)
            {
                MessageBox.Show("Vui lòng nhập đơn giá hợp lệ (số dương).", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDongia.Focus();
                return false;
            }

            if (cbLoaiSP.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbLoaiSP.Focus();
                return false;
            }

            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBox1.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"
                        INSERT INTO Products (ProductName, UnitPrice, QuantityPerUnit, CategoryID, SupplierID)
                        VALUES (@ProductName, @UnitPrice, @QuantityPerUnit, @CategoryID, @SupplierID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductName", txtTenSP.Text.Trim());
                        command.Parameters.AddWithValue("@UnitPrice", decimal.Parse(txtDongia.Text));
                        command.Parameters.AddWithValue("@QuantityPerUnit", int.Parse(txtSoluong.Text));
                        command.Parameters.AddWithValue("@CategoryID", cbLoaiSP.SelectedValue);
                        command.Parameters.AddWithValue("@SupplierID", comboBox1.SelectedValue);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Thêm sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để xóa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes)
                return;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    //Lấy dòng được select trong datagridview
                    int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colProductID"].Value);
                    string query = "DELETE FROM Products WHERE ProductID = @ProductID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.ExecuteNonQuery();
                        MessageBox.Show("Xóa sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sản phẩm để sửa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInputs())
                return;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    int productId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["colProductID"].Value);
                    string query = @"
                        UPDATE Products 
                        SET ProductName = @ProductName, 
                            UnitPrice = @UnitPrice, 
                            QuantityPerUnit = @QuantityPerUnit, 
                            CategoryID = @CategoryID, 
                            SupplierID = @SupplierID
                        WHERE ProductID = @ProductID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProductID", productId);
                        command.Parameters.AddWithValue("@ProductName", txtTenSP.Text.Trim());
                        command.Parameters.AddWithValue("@UnitPrice", decimal.Parse(txtDongia.Text));
                        command.Parameters.AddWithValue("@QuantityPerUnit", int.Parse(txtSoluong.Text));
                        command.Parameters.AddWithValue("@CategoryID", cbLoaiSP.SelectedValue);
                        command.Parameters.AddWithValue("@SupplierID", comboBox1.SelectedValue);

                        command.ExecuteNonQuery();
                        MessageBox.Show("Cập nhật sản phẩm thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ClearInputs();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                this.Close();
        }

        private void txtTimtenSP_TextChanged(object sender, EventArgs e)
        {
            string filter = txtTimtenSP.Text.Trim();
            if (dataTable != null)
            {
                dataTable.DefaultView.RowFilter = string.Format("ProductName LIKE '%{0}%'", filter.Replace("'", "''"));
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txtTenSP.Text = row.Cells["colProductName"].Value?.ToString() ?? "";
                txtDongia.Text = row.Cells["colUnitPrice"].Value?.ToString() ?? "";
                txtSoluong.Text = row.Cells["colQuantityPerUnit"].Value?.ToString() ?? "";
                cbLoaiSP.Text = row.Cells["colCategoryName"].Value?.ToString() ?? "";
                comboBox1.Text = row.Cells["colCompanyName"].Value?.ToString() ?? "";
            }
        }

        private void ClearInputs()
        {
            txtTenSP.Text = "";
            txtSoluong.Text = "";
            txtDongia.Text = "";
            cbLoaiSP.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;
        }
    }
}