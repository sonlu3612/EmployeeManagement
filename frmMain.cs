using AntdUI;
using EmployeeManagement.Auth;
using EmployeeManagement.DAL;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace EmployeeManagement
{
    public partial class frmMain : AntdUI.Window
    {
        private readonly User _currentUser;
        private Employee _employee;
        public frmMain(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            tabs1.SelectedTab = tabProject;
            menu1.SelectIndex(0, true);

            if (page_Account1 != null)
            {
                page_Account1.ProfileUpdated += Page_Account1_ProfileUpdated;
            }
            label.GotFocus += (s, e) => label.Parent.Focus();

        }

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
            if (select == "Đổi mật khẩu")
            {
                tabChangePassword.Visible = true;
                tabs1.SelectedTab = tabChangePassword;
            }
            if (select == "Dự án")
            {
                tabs1.SelectedTab = tabProject;
                phTrangChu.Text = "Dự án";
                phTrangChu.IconSvg = "DiffOutlined";
                page_Project1.LoadData();
                //tabs1.SelectedTab = tabProject;
                //frmManageTasks frmManageTasks = new frmManageTasks();
                //frmManageTasks.Show();
            }
            if (select == "Company")
            {
                tabs1.SelectedTab = tabCompany;
                phTrangChu.Text = "Company";
            }
            if (select == "Nhiệm vụ")
            {
                tabs1.SelectedTab = tabTask;
                phTrangChu.Text = "Nhiệm vụ";
                phTrangChu.IconSvg = "ScheduleOutlined";
                page_Task1.loadData();
            }
            if (select == "Đăng xuất")
            {
                string message = "Bạn có muốn đăng xuất?";
                frmInfor frmInfor = new frmInfor(message,this);
                frmInfor.ShowDialog();
                
            }
            if (select == "Cá nhân")
            {
                tabs1.SelectedTab = tabMyProfile;
                phTrangChu.Text = "Thông tin cá nhân";
                phTrangChu.IconSvg = "UserOutlined";
                page_Project1.LoadData();
            }
            if (select == "Nhân viên")
            {
                tabs1.SelectedTab = tabNV;
                phTrangChu.Text = "Nhân viên";
                phTrangChu.IconSvg = "TeamOutlined";
                page_Employee1.LoadData();
            }
           
            if (select == "Phòng ban")
            {
                tabs1.SelectedTab = tpPhongBan;
                phTrangChu.Text = "Phòng Ban";
                phTrangChu.IconSvg = "GroupOutlined";
                page_Department1.LoadData();
            }


        }

        private readonly HashPassword hp = new HashPassword();
        private readonly UserRepository userRepository = new UserRepository();

        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            btnChangePass.Loading = true;
            int userID = _currentUser.UserID;
            string oldPassHash = hp.Hash(txtMKC.Text);

            // Xác thực trực tiếp với DB
            if (!userRepository.VerifyPassword(userID, oldPassHash))
            {
                labelMatKhau.Text = "Mật khẩu cũ không đúng!";
                Console.WriteLine(txtMKC.Text + "\n" + oldPassHash);
                txtMKC.Focus();
                btnChangePass.Loading = false;
                return;
            }

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

            if (newPass != confirmPass)
            {
                labelMatKhau.Text = "Mật khẩu mới và xác nhận mật khẩu không khớp!";
                btnChangePass.Loading = false;
                return;
            }

            string newPassHash = hp.Hash(newPass);
            bool changeResult = userRepository.ChangePassword(userID, newPassHash);
            if (changeResult)
            {
                _currentUser.PasswordHash = newPassHash;

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

            btnChangePass.Loading = false;
        }


        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void page_Account1_Load(object sender, EventArgs e)
        {
            var employee = employeeRepository.GetById(_currentUser.UserID);
            _employee = employee;
            var user = userRepository.GetById(_currentUser.UserID);
            
            employee.Email = user.Email;
            employee.Phone = user.Phone;

            page_Account1.LoadProfile(employee);
            if (page_Account1 != null && employee != null) 
            {
                page_Account1.LoadProfile(employee);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            tabs1.SelectedTab = tabProject;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
            string normalizedPath = _employee.AvatarPath?.TrimStart('/', '\\') ?? "";

            if (string.IsNullOrEmpty(normalizedPath))
            {
                return;
            }

            string absolutePath = Path.Combine(projectRoot, normalizedPath);

            try
            {
                if (File.Exists(absolutePath))
                {
                    avatar4.Image = Image.FromFile(absolutePath);
                }
                else
                {
                    //avatar1.Image = Properties.Resources.default_avatar;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load avatar cho EmployeeID {_employee.EmployeeID}: {ex.Message}");
                // avatar1.Image = Properties.Resources.default_avatar;
            }
            label.Text = _employee.FullName;
            label5.Text = _currentUser.Roles[0];
        }

        private void panel4_Click(object sender, EventArgs e)
        {

        }

        private void Page_Account1_ProfileUpdated(object sender, EventArgs e)
        {
            var employee = employeeRepository.GetById(_currentUser.UserID);
            _employee = employee;
            var user = userRepository.GetById(_currentUser.UserID);
            employee.Email = user.Email;
            employee.Phone = user.Phone;
            page_Account1.LoadProfile(employee);
            page_Employee1.LoadData();
            label.Text = _employee.FullName;
            string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
            string normalizedPath = _employee.AvatarPath?.TrimStart('/', '\\') ?? "";
            if (!string.IsNullOrEmpty(normalizedPath))
            {
                string absolutePath = Path.Combine(projectRoot, normalizedPath);
                try
                {
                    if (File.Exists(absolutePath))
                    {
                        avatar4.Image = Image.FromFile(absolutePath);
                    }
                }
                catch { }
            }
        }
    }
}
