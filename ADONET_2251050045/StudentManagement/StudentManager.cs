using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Windows.Forms;

namespace StudentManagement
{
    //class StudentManager
    //{
    //    private string connectionString;

    //    public StudentManager()
    //    {
    //        // Lấy chuỗi kết nối từ App.config
    //        connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
    //    }

    //    // 1. Hiển thị danh sách sinh viên
    //    public void DisplayStudents()
    //    {
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            string query = "SELECT * FROM Students";
    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            try
    //            {
    //                conn.Open();
    //                SqlDataReader reader = cmd.ExecuteReader();
    //                Console.WriteLine("Danh sach sinh vien:");
    //                Console.WriteLine("ID\tHo ten\tTuoi\tChuyen nganh");
    //                while (reader.Read())
    //                {
    //                    Console.WriteLine($"{reader["StudentID"]}\t{reader["FullName"]}\t{reader["Age"]}\t{reader["Major"]}");
    //                }
    //                reader.Close();
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine("Loi: " + ex.Message);
    //            }
    //        }
    //    }

    //    // 2. Thêm sinh viên mới
    //    public void AddStudent(string fullName, int age, string major)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            string query = "INSERT INTO Students (FullName, Age, Major) VALUES (@FullName, @Age, @Major)";
    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            cmd.Parameters.AddWithValue("@FullName", fullName);
    //            cmd.Parameters.AddWithValue("@Age", age);
    //            cmd.Parameters.AddWithValue("@Major", major);
    //            try
    //            {
    //                conn.Open();
    //                cmd.ExecuteNonQuery();
    //                Console.WriteLine("Them sinh vien thanh cong!");
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine("Loi: " + ex.Message);
    //            }
    //        }
    //    }

    //    // 3. Sửa thông tin sinh viên
    //    public void UpdateStudent(int studentID, string fullName, int age, string major)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            string query = "UPDATE Students SET FullName = @FullName, Age = @Age, Major = @Major WHERE StudentID = @StudentID";
    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            cmd.Parameters.AddWithValue("@StudentID", studentID);
    //            cmd.Parameters.AddWithValue("@FullName", fullName);
    //            cmd.Parameters.AddWithValue("@Age", age);
    //            cmd.Parameters.AddWithValue("@Major", major);
    //            try
    //            {
    //                conn.Open();
    //                int rowsAffected = cmd.ExecuteNonQuery();
    //                if (rowsAffected > 0)
    //                    Console.WriteLine("Cap nhat sinh vien thanh cong!");
    //                else
    //                    Console.WriteLine("Khong tim thay sinh vien voi ID: " + studentID);
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine("Loi: " + ex.Message);
    //            }
    //        }
    //    }

    //    // 4. Xóa sinh viên
    //    public void DeleteStudent(int studentID)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connectionString))
    //        {
    //            string query = "DELETE FROM Students WHERE StudentID = @StudentID";
    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            cmd.Parameters.AddWithValue("@StudentID", studentID);
    //            try
    //            {
    //                conn.Open();
    //                int rowsAffected = cmd.ExecuteNonQuery();
    //                if (rowsAffected > 0)
    //                    Console.WriteLine("Xoa sinh vien thanh cong!");
    //                else
    //                    Console.WriteLine("Khong tim thay sinh vien voi ID: " + studentID);
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine("Loi: " + ex.Message);
    //            }
    //        }
    //    }
    //}

    // StudentManager class (reused from console app)
    public class StudentManager
    {
        private string connectionString;

        public StudentManager()
        {
            connectionString = ConfigurationManager.ConnectionStrings["StudentDB"].ConnectionString;
        }

        // Get students as DataTable for DataGridView
        public DataTable GetStudents()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return dt;
            }
        }

        public void AddStudent(string fullName, int age, string major)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (FullName, Age, Major) VALUES (@FullName, @Age, @Major)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Major", major);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void UpdateStudent(int studentID, string fullName, int age, string major)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET FullName = @FullName, Age = @Age, Major = @Major WHERE StudentID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                cmd.Parameters.AddWithValue("@FullName", fullName);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Major", major);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Cập nhật sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Không tìm thấy sinh viên với ID: " + studentID, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        public void DeleteStudent(int studentID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE StudentID = @StudentID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentID", studentID);
                try
                {
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        MessageBox.Show("Xóa sinh viên thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Không tìm thấy sinh viên với ID: " + studentID, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
