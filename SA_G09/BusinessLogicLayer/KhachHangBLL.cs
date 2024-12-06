using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SA_G09.DataAccessLayer;

namespace SA_G09.BusinessLogicLayer
{
    internal class KhachHangBLL
    {
        private KhachHangDAL khachHangDAL = new KhachHangDAL();

        public DataTable findAll()
        {
            return khachHangDAL.findAll();
        }
        public bool insert(string maKH, string tenKH, string soDienThoai)
        {
            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(soDienThoai))
                return false;
            else if (!IsValidPhoneNumber(soDienThoai))
                return false;
            else
                return khachHangDAL.insert(maKH, tenKH, soDienThoai);
        }

        public bool update(string maKH, string tenKH, string soDienThoai)
        {
            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH) || string.IsNullOrEmpty(soDienThoai))
                return false;
            else if (!IsValidPhoneNumber(soDienThoai))
                return false;
            else
                return khachHangDAL.update(maKH, tenKH, soDienThoai);
        }

        public bool deleteById(string maKH)
        {
            if (string.IsNullOrEmpty(maKH))
                return false;
            return khachHangDAL.deleteById(maKH);
        }

        public DataTable search(string tenKH, string soDienThoai)
        {
            if (string.IsNullOrEmpty(tenKH)) tenKH = null;
            if (string.IsNullOrEmpty(soDienThoai)) soDienThoai = null;
            return khachHangDAL.search(tenKH, soDienThoai);
        }


        public bool checkExistsNameAndPhone(string tenKH, string soDienThoai)
        {
            DataTable dt = khachHangDAL.findAll();
            foreach (DataRow dr in dt.Rows)
            {
                if (String.Equals(dr["Tên KH"].ToString(), tenKH, StringComparison.InvariantCultureIgnoreCase) &&
                    String.Equals(dr["Số điện thoại"].ToString(), soDienThoai, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            return phoneNumber.All(char.IsDigit) && phoneNumber.Length >= 10 && phoneNumber.Length <= 15;
        }
    }

}
