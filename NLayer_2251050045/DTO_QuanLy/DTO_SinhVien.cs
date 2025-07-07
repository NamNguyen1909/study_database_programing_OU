using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO_QuanLy
{
    public class DTO_SinhVien
    {
        private int _SINHVIEN_ID;
        private string _SINHVIEN_NAME;
        private string _SINHVIEN_PHONE;
        private string _SINHVIEN_EMAIL;

        public int SINHVIEN_ID
        {
            get
            {
                return _SINHVIEN_ID;
            }
            set
            {
                _SINHVIEN_ID = value;
            }
        }

        public string SINHVIEN_NAME
        {
            get
            {
                return _SINHVIEN_NAME;
            }
            set
            {
                _SINHVIEN_NAME = value;
            }
        }

        public string SINHVIEN_PHONE
        {
            get
            {
                return _SINHVIEN_PHONE;
            }
            set
            {
                _SINHVIEN_PHONE = value;
            }
        }

        public string SINHVIEN_EMAIL
        {
            get
            {
                return _SINHVIEN_EMAIL;
            }
            set
            {
                _SINHVIEN_EMAIL = value;
            }
        }

        public DTO_SinhVien(int id, string name, string phone, string email)
        {
            _SINHVIEN_ID = id;
            _SINHVIEN_NAME = name;
            _SINHVIEN_PHONE = phone;
            _SINHVIEN_EMAIL = email;
        }
    }
}