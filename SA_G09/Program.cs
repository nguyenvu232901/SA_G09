using SA_G09.PresentationLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SA_G09
{
    internal static class Program
    {
        public static string constr = "Data Source=LAPTOP-9BKQGS37\\SQLEXPRESS;Initial Catalog=db_se05;Integrated Security=True";

        public static string maQuyenQuanLy = "Q01";
        public static string maQuyenBanHang = "Q02";
        public static string maQuyenKho = "Q03";
        public static string maQuyenKeToan = "Q04";

        public static string tenNV, maTK, maQuyen, tenTK;

        public static void setSession(string maTK, string maQuyen, string tenTK)
        {
            Program.maTK = maTK;
            Program.maQuyen = maQuyen;
            Program.tenTK = tenTK;
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new QuanLyBanHangGUI());
        }
    }
}
