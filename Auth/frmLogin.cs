using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace EmployeeManagement.Auth
{
   
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_MouseHover(object sender, EventArgs e)
        {
            btnDangNhap.Type = AntdUI.TTypeMini.Success;
        }

        private UserRepository userRepository = new UserRepository();

        private HashPassword hp = new HashPassword();
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            btnDangNhap.Loading = true;

            
            txtTaiKhoan.Text = "admin@company.com";
            txtMatKhau.Text = "Admin@123!";

            string name = txtTaiKhoan.Text;
            string pass = hp.Hash(txtMatKhau.Text);

            //Console.WriteLine($"Attempting login with Username: {name}, Password: {pass}");


            User user = userRepository.ValidateLogin(name,pass);
            if (user == null)
            {
                
                //labelThongBao.Text = "Tên đăng nhập hoặc mật khẩu không đúng.";
                btnDangNhap.Loading = false;
                txtTaiKhoan.Focus();
                Task.Delay(2000).ContinueWith(t =>
                {
                    this.Invoke((Action)(() =>
                    {
                        //labelThongBao.Text = "";
                        //txtTaiKhoan.Text = "";
                        //txtMatKhau.Text = "";
                    }));
                });
            }
            else
            {
                Task.Delay(2000).ContinueWith(t =>
                {
                    this.Invoke((Action)(() =>
                    {
                        btnDangNhap.Loading = false;
                        //MessageBox.Show("Login successful!");


                    }));
                });

                if(user.Role == "Admin")
                {
                    frmMain mainForm = new frmMain(user);
                    mainForm.Show();
                    //MessageBox.Show("Đăng nhập thành công với quyền Quản trị viên!");
                }
                else if(user.Role == "Manager")
                {
                    frmManager managerForm = new frmManager(user);
                    managerForm.Show();
                    //MessageBox.Show("Đăng nhập thành công với quyền Quản lý!");
                }
                else
                {
                    frmEmployee employeeForm = new frmEmployee(user);
                    //MessageBox.Show("Đăng nhập thành công với quyền Nhân viên!");
                }

                
                this.Hide();

            }    
        }

        private void cboxMatKhau_CheckedChanged(object sender, AntdUI.BoolEventArgs e)
        {
            if(cboxMatKhau.Checked == false)
            {
                txtMatKhau.UseSystemPasswordChar = true;
            }
            else
            {
                txtMatKhau.UseSystemPasswordChar = false;
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
