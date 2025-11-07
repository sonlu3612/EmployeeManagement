using AntdUI;
using EmployeeManagement.Auth;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Drawing;
using System.Windows.Forms;


namespace EmployeeManagement
{
    public partial class frmMain : AntdUI.Window
    {
        private readonly User _currentUser;
        public frmMain(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            tabs1.SelectedTab = tabProject;
            menu1.SelectIndex(0, true);
        }

        // Khi click vào dòng trong bảng
        private void TblNhanVien_Click(object sender, EventArgs e)
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

                //tabs1.SelectedTab = tabProject;
                //frmManageTasks frmManageTasks = new frmManageTasks();
                //frmManageTasks.Show();
            }
            if (select == "Company")
            {
                tabs1.SelectedTab = tabCompany;
                phTrangChu.Text = "Company";
            }
            if (select == "Tasks")
            {
                tabs1.SelectedTab = tabTask;
                phTrangChu.Text = "Tasks";
                phTrangChu.Icon = Properties.Resources.note;
            }
            if (select == "Log out")
            {
                string message = "Bạn có muốn đăng xuất?";
                frmInfor frmInfor = new frmInfor(message,this);
                frmInfor.ShowDialog();
                
            }
            if (select == "My Profile")
            {
                tabs1.SelectedTab = tabMyProfile;
                
                phTrangChu.Text = "My Profile";
            }
            if (select == "Employees")
            {
                tabs1.SelectedTab = tabNV;
                phTrangChu.Text = "Employees";
            }
           
            if (select == "Phòng ban")
            {
                tabs1.SelectedTab = tpPhongBan;
                phTrangChu.Text = "Phòng Ban";
            }


        }

        private readonly HashPassword hp = new HashPassword();
        private readonly UserRepository userRepository = new UserRepository();
        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            btnChangePass.Loading = true;
            int userID = _currentUser.UserID;
            string oldPass = hp.Hash(txtMKC.Text);
            Console.WriteLine(_currentUser.PasswordHash + "\n");
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
                    btnChangePass.Loading = false;
                    return;
                }
                if (newPass == confirmPass)
                {
                    string newPassHash = hp.Hash(newPass);
                    bool changeResult = userRepository.ChangePassword(userID, newPassHash);
                    if (changeResult)
                    {
                        labelMatKhau.ForeColor = Color.Green;
                        labelMatKhau.Text = "Đổi mật khẩu thành công!";
                        txtMKC.Text = "";
                        txtMKM.Text = "";
                        txtXNMK.Text = "";
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
                Console.WriteLine(txtMKC.Text + "\n" + oldPass);
                txtMKC.Focus();
            }
            btnChangePass.Loading = false;
        }


        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void page_Account1_Load(object sender, EventArgs e)
        {
            var employee = employeeRepository.GetFromIdUser(_currentUser.UserID);
            //if (employee == null)
            //{
            //    MessageBox.Show("Không tìm thấy nhân viên cho UserID = " + _currentUser.UserID);
            //    return;
            //}

            //page_Account1.LoadProfile(employee);
            //if (page_Account1 != null && employee != null) // Assuming page_Account1 is the instance of Page_Account in tabMyProfile
            //{
            //    page_Account1.LoadProfile(employee);
            //}

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            tabs1.SelectedTab = tabProject;
        }

       

       
    }
}
