using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA_G09.DataAccessLayer
{
    internal class KhachHangDAL
    {
        private string constr = @"Data Source=LAPTOP-9BKQGS37\SQLEXPRESS;Initial Catalog=db_se05;Integrated Security=True";
        public DataTable findAll()
        {
            try
            {
                using (SqlConnection cnn = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = cnn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "sp_ShowViewKhachHang";
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            using (DataTable dt = new DataTable())
                            {
                                ad.Fill(dt);
                                return dt;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }


        public bool insert(string maKH, string tenKH, string soDienThoai)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ThemKhachHang";
                    cmd.Parameters.AddWithValue("@PK_sMaKH", maKH);
                    cmd.Parameters.AddWithValue("@sTenKH", tenKH);
                    cmd.Parameters.AddWithValue("@sSDT", soDienThoai);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();

                    return i > 0;
                }
            }
        }

        public bool update(string maKH, string tenKH, string soDienThoai)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SuaTTKhachHang";
                    cmd.Parameters.AddWithValue("@PK_sMaKH", maKH);
                    cmd.Parameters.AddWithValue("@sTenKH", tenKH);
                    cmd.Parameters.AddWithValue("@sSDT", soDienThoai);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();

                    return i > 0;
                }
            }
        }

        public bool deleteById(string maKH)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_XoaKhachHang";
                    cmd.Parameters.AddWithValue("@PK_sMaKH", maKH);
                    cnn.Open();
                    int i = cmd.ExecuteNonQuery();
                    cnn.Close();

                    return i > 0;
                }
            }
        }

        public DataTable search(string tenKH, string soDienThoai)
        {
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = cnn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "sp_TimKiemKhachHang";

                    // Truyền tham số vào stored procedure
                    cmd.Parameters.AddWithValue("@sTenKH", tenKH);
                    cmd.Parameters.AddWithValue("@sSDT", soDienThoai);

                    using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            ad.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }

    }
}
