using System.Data.SqlClient;

namespace DAL_QuanLy
{
    public class DBConnect
    {
        protected SqlConnection _conn = new SqlConnection("Data Source=LAPTOP-0AU57SPC\\SQLEXPRESS;Initial Catalog=QLSV;Integrated Security=True");
    }
}