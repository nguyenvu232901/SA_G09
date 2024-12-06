using SA_G09.BusinessLogicLayer;
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
    public partial class DangNhapGUI : Form
    {
        private const string MS_001 = "Vui lòng nhập đủ thông tin";
        private const string MS_002 = "Sai tên tài khoản";
        private const string MS_003 = "Sai mật khẩu";

        TaiKhoanBLL taiKhoanBLL = new TaiKhoanBLL();
        public DangNhapGUI()
        {
            InitializeComponent();
        }
        public bool checkValidDangNhap(string tenTK, string matKhau)
        {
            if (tenTK.Equals("") || matKhau.Equals(""))
                return false;

            return true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string tenTK = txtTenTaiKhoan.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (!checkValidDangNhap(tenTK, matKhau))
            {
                errorProviderLogin.SetError(txtTenTaiKhoan, MS_001);
                errorProviderLogin.SetError(txtMatKhau, MS_001);
                return;
            }

            errorProviderLogin.Clear();

            // Thay đổi login thành Login
            int loginResult = taiKhoanBLL.Login(tenTK, matKhau);

            switch (loginResult)
            {
                case 0: // Sai tên tài khoản
                    errorProviderLogin.SetError(txtTenTaiKhoan, MS_002);
                    break;

                case 2: // Sai mật khẩu
                    errorProviderLogin.SetError(txtMatKhau, MS_003);
                    txtMatKhau.Text = "";
                    break;

                case 1: // Đăng nhập thành công
                    MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    new TrangChuGUI().Show();
                    this.Hide();
                    break;

                default: // Lỗi không xác định
                    MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void DangNhapGUI_Load(object sender, EventArgs e)
        {

        }
    }
}
