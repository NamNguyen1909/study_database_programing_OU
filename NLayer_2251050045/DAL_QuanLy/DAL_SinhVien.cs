using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DTO_QuanLy;
using System.Windows.Forms;

namespace DAL_QuanLy
{
    public class DAL_SinhVien : DBConnect
    {
        /// <summary>
        /// Gọi toàn bộ Sinh viên
        /// </summary>
        /// <returns></returns>
        public DataTable getSinhVien()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SINHVIEN", _conn);
            DataTable dtSinhvien = new DataTable();
            da.Fill(dtSinhvien);
            return dtSinhvien;
        }

        /// <summary>
        /// Thêm Sinh viên
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        public bool themSinhVien(DTO_SinhVien sv)
        {
            try
            {
                // Ket noi
                _conn.Open();
                // Query string - vì SV_ID là identity (giữ tri tự tăng dần) nên ko cần phải insert ID
                string SQL = string.Format("INSERT INTO SINHVIEN(SV_NAME, SV_PHONE, SV_EMAIL) VALUES('{0}', '{1}', '{2}')", sv.SINHVIEN_NAME, sv.SINHVIEN_PHONE, sv.SINHVIEN_EMAIL);
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }

        /// <summary>
        /// Sửa sinh viên
        /// </summary>
        /// <param name="sv"></param>
        /// <returns></returns>
        public bool suaSinhVien(DTO_SinhVien sv)
        {
            try
            {
                // Ket noi
                _conn.Open();
                // Query string
                string SQL = string.Format("UPDATE SINHVIEN SET SV_NAME = '{0}', SV_PHONE = '{1}', SV_EMAIL = '{2}' WHERE SV_ID = {3}",
                    sv.SINHVIEN_NAME, sv.SINHVIEN_PHONE, sv.SINHVIEN_EMAIL, sv.SINHVIEN_ID);
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }

        /// <summary>
        /// Xóa Sinh viên
        /// </summary>
        /// <param name="SV_ID"></param>
        /// <returns></returns>
        public bool xoaSinhVien(int SV_ID)
        {
            try
            {
                // Ket noi
                _conn.Open();
                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format("DELETE FROM SINHVIEN WHERE SV_ID = {0}", SV_ID);
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Console.WriteLine(e.Message);
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }
    }
}