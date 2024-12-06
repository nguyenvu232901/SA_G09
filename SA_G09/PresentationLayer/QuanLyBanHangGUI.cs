using SA_G09.BusinessLogicLayer;
using SA_G09.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA_G09.PresentationLayer
{
    public partial class QuanLyBanHangGUI : Form
    {
        private KhachHangBLL khachHangBLL;
        public QuanLyBanHangGUI()
        {
            InitializeComponent();
            khachHangBLL = new KhachHangBLL();
        }

        private void QuanLyBanHangGUI_Load(object sender, EventArgs e)
        {
            LoadKhachHangData();
        }

        private void LoadKhachHangData()
        {
            // Lấy dữ liệu từ KhachHangBLL
            DataTable dtKhachHang = khachHangBLL.findAll();

            if (dtKhachHang != null)
            {
                // Thêm cột số thứ tự vào DataTable
                dtKhachHang.Columns.Add("STT", typeof(int));

                // Điền số thứ tự vào cột "STT"
                int stt = 1;
                foreach (DataRow row in dtKhachHang.Rows)
                {
                    row["STT"] = stt++;
                }
                dtKhachHang.Columns["STT"].SetOrdinal(0);

                // Gán DataTable vào DataGridView
                dgvKH.DataSource = dtKhachHang;

                // Tùy chỉnh DataGridView
                dgvKH.AutoResizeColumns(); // Để cột tự động điều chỉnh độ rộng vừa với dữ liệu
                dgvKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Đảm bảo cột không bị cắt
            }
            else
            {
                MessageBox.Show("Không có dữ liệu khách hàng.");
            }
        }


        private void dgvKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // Kiểm tra nếu có hàng được chọn
            if (e.RowIndex >= 0)
            {
                // Lấy dữ liệu của hàng được chọn
                DataGridViewRow row = dgvKH.Rows[e.RowIndex];

                // Hiển thị dữ liệu vào các TextBox
                txtMaKH.Text = row.Cells["Mã KH"].Value.ToString();
                txtHoTenKH.Text = row.Cells["Tên KH"].Value.ToString();
                txtSDTKH.Text = row.Cells["SĐT"].Value.ToString();

                // Không cho phép nhập vào txtMaKH
                txtMaKH.Enabled = false; // Đặt lại thuộc tính Enabled của txtMaKH là false để không cho phép chỉnh sửa

                // Nếu cần, bạn có thể thêm logic để cập nhật thêm các trường khác
            }

        }

        private void btnSuaKH_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text;
            string tenKH = txtHoTenKH.Text;
            string soDienThoai = txtSDTKH.Text;

            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(soDienThoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Gọi hàm cập nhật
            bool isUpdated = khachHangBLL.update(maKH, tenKH, soDienThoai);
            if (isUpdated)
            {
                MessageBox.Show("Cập nhật thành công.");
                LoadKhachHangData(); // Tải lại dữ liệu lên DataGridView
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void btnXoaKH_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKH.Text;

            // Kiểm tra mã khách hàng hợp lệ
            if (string.IsNullOrEmpty(maKH))
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.");
                return;
            }

            // Gọi hàm xóa
            bool isDeleted = khachHangBLL.deleteById(maKH);
            if (isDeleted)
            {
                MessageBox.Show("Xóa thành công.");
                LoadKhachHangData(); // Tải lại dữ liệu lên DataGridView
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
        }

        private void btnLamMoiKH_Click(object sender, EventArgs e)
        {
            // Làm mới các TextBox
            txtMaKH.Clear();
            txtHoTenKH.Clear();
            txtSDTKH.Clear();
            txtMaKH.Enabled = true; // Cho phép nhập lại mã khách hàng

            // Tải lại dữ liệu lên DataGridView
            LoadKhachHangData();
        }


        private void tb_timkiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = tb_timkiem.Text;

            // Kiểm tra nếu từ khóa tìm kiếm không rỗng
            if (!string.IsNullOrEmpty(keyword))
            {
                // Tạo một BindingSource để kết nối với DataGridView
                BindingSource bindingSource = new BindingSource();

                // Gán DataTable vào BindingSource
                bindingSource.DataSource = dgvKH.DataSource;

                // Áp dụng bộ lọc cho BindingSource
                bindingSource.Filter = $"[Tên KH] LIKE '%{keyword}%' OR [SĐT] LIKE '%{keyword}%'";

                // Gán lại BindingSource vào DataGridView
                dgvKH.DataSource = bindingSource;
            }
            else
            {
                // Nếu không có từ khóa, hiển thị tất cả dữ liệu
                LoadKhachHangData(); // Gọi lại hàm để tải lại toàn bộ dữ liệu
            }

        }
    }
}
