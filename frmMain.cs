using AntdUI;
using EmployeeManagement.Auth;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Drawing;


namespace EmployeeManagement
{
    public partial class frmMain : AntdUI.Window
    {
        private User _currentUser;
        public frmMain(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;


        }

        private void frmMain_Load(object sender, EventArgs e)
        {

            tabChangePassword.Visible = false;
        }

        private void phTrangChu_Click(object sender, EventArgs e)
        {

        }

        private void menu1_SelectChanged(object sender, MenuSelectEventArgs e)
        {
            string select = menu1.GetSelectItem().ToString().Trim();

            //Console.WriteLine(select);
            if (select == "Change Password")
            {
                tabChangePassword.Visible = true;
                tabs1.SelectedTab = tabChangePassword;

            }
            if (select == "Projects")
            {

                tabs1.SelectedTab = tabProject;
            }
            // if (select == "Employee")
            // {
            //     tabs1.SelectedTab = tabEmployees;
            //}
            if (select == "Company")
            {
                tabs1.SelectedTab = tabCompany;
            }
            if (select == "Tasks")
            {
                tabs1.SelectedTab = tabTask;
                phTrangChu.Text = "Tasks";
                phTrangChu.Icon = Properties.Resources.note;
            }
            if (select == "Database")
            {
                tabs1.SelectedTab = tabDatabase;
            }
            if (select == "Log out")
            {
                string message = "Bạn có muốn đăng xuất?";
                frmInfor frmInfor = new frmInfor(message,this);
                frmInfor.ShowDialog();
                
            }
            if(select == "My Profile")
            {
                tabs1.SelectedTab = tabMyProfile;
            }



        }

        private HashPassword hp = new HashPassword();
        private UserRepository userRepository = new UserRepository();
        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            btnChangePass.Loading = true;
            int userID = _currentUser.UserID;
            string oldPass = hp.Hash(txtMKC.Text);

            if (oldPass == _currentUser.PasswordHash)
            {
                string newPass = txtMKM.Text;
                string confirmPass = txtXNMK.Text;
                if (newPass.Length < 8)
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
            else
            {
                labelMatKhau.Text = "Mật khẩu cũ không đúng!";
                txtMKC.Focus();
            }

        }
    }
}
