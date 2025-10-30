using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using AntdUI;
using EmployeeManagement.Auth;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;


namespace EmployeeManagement
{
    public partial class frmManager : AntdUI.Window
    {
        private User _currentUser;
        public frmManager(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
        }

        private void frmManager_Load(object sender, EventArgs e)
        {

        }

        private void phTrangChu_Click(object sender, EventArgs e)
        {

        }

        private void menu1_SelectChanged(object sender, MenuSelectEventArgs e)
        {
            string select = menu1.GetSelectItem().ToString().Trim();

            //Console.WriteLine(select);
           if(select=="Change Password")
            {
                tabChangePassword.Visible = true;
                tabs1.SelectedTab = tabChangePassword;

            }


        }

        private HashPassword hp = new HashPassword();
        private UserRepository userRepository = new UserRepository();
        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            btnChangePass.Loading = true;
            int userID = _currentUser.UserID;
            string oldPass = hp.Hash(txtMKC.Text);

            if(oldPass == _currentUser.PasswordHash)
            {
                string newPass = txtMKM.Text;
                string confirmPass = txtXNMK.Text;
                if(newPass.Length < 8)
                {
                    labelMatKhau.Text = "Mật khẩu mới phải có ít nhất 8 ký tự!";
                    txtMKM.Text = "";
                    txtXNMK.Text = "";
                    txtMKM.Focus();
                }
                if (newPass == confirmPass)
                {
                    string newPassHash = hp.Hash(newPass);
                    bool changeResult = userRepository.ChangePassword(userID, newPassHash);
                    if (changeResult)
                    {
                        labelMatKhau.ForeColor = Color.Green;
                        labelMatKhau.Text = "Đổi mật khẩu thành công!";
                    }
                    else
                    {
                        labelMatKhau.Text = "Đổi mật khẩu thất bại. Vui lòng thử lại!";
                    }

                }
                else
                {
                    labelMatKhau.Text = "Mật khẩu mới và xác nhận mật khẩu không khớp!";
                }
            }

        }
    }
}
