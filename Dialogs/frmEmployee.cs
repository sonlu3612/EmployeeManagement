using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.DAL.Services;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EmployeeManagement.Dialogs
{
    public partial class frmEmployee : Form
    {
        private readonly IRepository<Employee> _employeeRepository = new EmployeeRepository();
        private readonly IRepository<Department> _departmentRepository = new DepartmentRepository();
        private readonly UserRepository _userRepository = new UserRepository();
        private readonly AvatarService _avatarService = new AvatarService();
        private Employee _employee;
        private bool _canEdit;
        private string _fixedDepartment;

        public frmEmployee(Employee employee = null, bool canEdit = true, string fixedDepartment = null)
        {
            InitializeComponent();
            _employee = employee;
            _canEdit = canEdit;
            _fixedDepartment = fixedDepartment;
        }

        public void frmEmployee_Load()
        {
            ddownGT.Items.Add("Nam");
            ddownGT.Items.Add("Nữ");

            btnXem.Click += btnXem_Click;

            var departments = _departmentRepository.GetAll().ToList();
            ddownDepartment.Items.Clear();
            ddownDepartment.Items.AddRange(departments.Select(d => d.DepartmentName).ToArray());

            if (_employee != null)
            {
                txtName.Text = _employee.FullName;
                txtEmail.Text = _employee.Email;
                txtDienThoai.Text = _employee.Phone;
                txtPosition.Text = _employee.Position;
                txtDiaChi.Text = _employee.Address;
                ddownGT.Text = _employee.Gender;
                dateStart.Value = _employee.HireDate;
                var currentDep = departments.FirstOrDefault(d => d.DepartmentID == _employee.DepartmentID);
                if (currentDep != null)
                {
                    ddownDepartment.Text = currentDep.DepartmentName;
                }
                if (!string.IsNullOrEmpty(_employee.AvatarPath))
                {
                    string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
                    string fullPath = Path.Combine(projectRoot, _employee.AvatarPath.Replace("/", "\\"));
                    if (File.Exists(fullPath))
                    {
                        img.Image = Image.FromFile(fullPath);
                        img.Tag = _employee.AvatarPath;
                    }
                    else
                    {
                        Console.WriteLine($"[frmEmployee] Avatar không tìm thấy: {fullPath}");
                        img.Image = _avatarService.CreateDefaultAvatar(_employee.FullName);
                        img.Tag = null;
                    }
                }
                else
                {
                    img.Image = _avatarService.CreateDefaultAvatar(_employee.FullName);
                    img.Tag = null;
                }
            }
            else
            {
                dateStart.Value = DateTime.Today;
                if (!string.IsNullOrEmpty(_fixedDepartment))
                {
                    ddownDepartment.Text = _fixedDepartment;
                    ddownDepartment.Enabled = false;
                }
            }

            if (_canEdit && _employee != null)
            {
                bool isManager = departments.Any(d => d.ManagerID == _employee.EmployeeID);
                if (isManager)
                {
                    ddownDepartment.Enabled = false;
                }
            }
        }

        public void frmEmployee_Load(string DepartmentName)
        {
            ddownGT.Items.Add("Nam");
            ddownGT.Items.Add("Nữ");
            ddownDepartment.Text = DepartmentName;
            ddownDepartment.Enabled = false;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!_canEdit)
            {
                this.Close();
                return;
            }

            string name = txtName?.Text?.Trim() ?? string.Empty;
            string email = txtEmail?.Text?.Trim() ?? string.Empty;
            string phone = txtDienThoai?.Text?.Trim() ?? string.Empty;
            string position = txtPosition?.Text?.Trim() ?? string.Empty;
            string departmentText = ddownDepartment?.Text?.Trim() ?? string.Empty;
            DateTime hireDate = dateStart?.Value ?? DateTime.Today;

            if (string.IsNullOrWhiteSpace(name))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập họ và tên!");
                return;
            }
            if (name.Length > 200)
            {
                AntdUI.Message.warn(this.FindForm(), "Họ và tên quá dài (tối đa 200 ký tự).");
                return;
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập email!");
                return;
            }
            if (!IsValidEmail(email))
            {
                AntdUI.Message.warn(this.FindForm(), "Email không hợp lệ!");
                return;
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập điện thoại!");
                return;
            }
            if (!IsValidPhone(phone))
            {
                AntdUI.Message.warn(this.FindForm(), "Số điện thoại không hợp lệ (chỉ gồm chữ số, 7-15 ký tự).");
                return;
            }

            if (string.IsNullOrWhiteSpace(position))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập chức vụ!");
                return;
            }
            if (position.Length > 100)
            {
                AntdUI.Message.warn(this.FindForm(), "Chức vụ quá dài (tối đa 100 ký tự).");
                return;
            }

            if (string.IsNullOrWhiteSpace(departmentText))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng chọn phòng ban!");
                return;
            }

            if (hireDate > DateTime.Today)
            {
                AntdUI.Message.warn(this.FindForm(), "Ngày bắt đầu không thể lớn hơn ngày hôm nay!");
                return;
            }

            var department = _department_repository_fallback();

            if (department == null)
            {
                AntdUI.Message.error(this.FindForm(), "Phòng ban không tồn tại!");
                return;
            }

            var allUsers = _userRepository.GetAll().ToList();
            string normalizedPhone = NormalizePhone(phone);

            if (_employee == null)
            {
                var emailExists = allUsers.Any(u => !string.IsNullOrWhiteSpace(u.Email) &&
                    string.Equals(u.Email.Trim(), email, StringComparison.OrdinalIgnoreCase));
                if (emailExists)
                {
                    AntdUI.Message.warn(this.FindForm(), "Email đã được sử dụng bởi tài khoản khác!");
                    return;
                }

                var phoneExists = allUsers.Any(u => !string.IsNullOrWhiteSpace(u.Phone) &&
                    NormalizePhone(u.Phone) == normalizedPhone);
                if (phoneExists)
                {
                    AntdUI.Message.warn(this.FindForm(), "Số điện thoại đã được sử dụng bởi tài khoản khác!");
                    return;
                }

                var newUser = new User
                {
                    Email = email,
                    Phone = phone,
                    PasswordHash = "123456",
                    CreatedDate = DateTime.Now,
                    IsActive = true,
                    Roles = new List<string>()
                };
                if (!newUser.Roles.Contains("Nhân viên"))
                {
                    newUser.Roles.Add("Nhân viên");
                }
                var userID = _userRepository.InsertAndReturnId(newUser);
                var newEmployee = new Employee
                {
                    AvatarPath = img.Tag?.ToString() ?? string.Empty,
                    EmployeeID = userID,
                    FullName = name,
                    Email = email,
                    Phone = phone,
                    Address = txtDiaChi?.Text?.Trim() ?? string.Empty,
                    Position = position,
                    Gender = string.IsNullOrWhiteSpace(ddownGT.Text) ? "NotSpecified" : ddownGT.Text.Trim(),
                    DepartmentID = department.DepartmentID,
                    HireDate = hireDate,
                    IsActive = true
                };
                this.Tag = newEmployee;
            }
            else
            {
                var emailExistsForOther = allUsers.Any(u =>
                    !string.IsNullOrWhiteSpace(u.Email) &&
                    string.Equals(u.Email.Trim(), email, StringComparison.OrdinalIgnoreCase) &&
                    u.UserID != _employee.EmployeeID);

                if (emailExistsForOther)
                {
                    AntdUI.Message.warn(this.FindForm(), "Email đã được sử dụng bởi tài khoản khác!");
                    return;
                }

                var phoneExistsForOther = allUsers.Any(u =>
                    !string.IsNullOrWhiteSpace(u.Phone) &&
                    NormalizePhone(u.Phone) == normalizedPhone &&
                    u.UserID != _employee.EmployeeID);

                if (phoneExistsForOther)
                {
                    AntdUI.Message.warn(this.FindForm(), "Số điện thoại đã được sử dụng bởi tài khoản khác!");
                    return;
                }

                var user = _userRepository.GetById(_employee.EmployeeID);
                if (user != null)
                {
                    user.Email = email;
                    user.Phone = phone;
                    _userRepository.Update(user);
                }
                _employee.AvatarPath = img.Tag?.ToString() ?? _employee.AvatarPath;
                _employee.FullName = name;
                _employee.Email = email;
                _employee.Phone = phone;
                _employee.Address = txtDiaChi?.Text?.Trim() ?? string.Empty;
                _employee.Position = position;
                _employee.Gender = string.IsNullOrWhiteSpace(ddownGT.Text) ? "NotSpecified" : ddownGT.Text.Trim();
                _employee.DepartmentID = department.DepartmentID;
                _employee.HireDate = hireDate;
                _employeeRepository.Update(_employee);
                this.Tag = _employee;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private Department _department_repository_fallback()
        {
            var department = _departmentRepository.GetAll()
                .FirstOrDefault(d => d.DepartmentName == ddownDepartment?.Text?.Trim());
            return department;
        }

        private void ddownGT_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            ddownGT.Text = e.Value?.ToString();
        }

        private void btnAnh_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn ảnh đại diện";
                    openFileDialog.Filter = "Ảnh (.jpg;.jpeg;.png)|*.jpg;*.jpeg;*.png";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog.FileName;
                        string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
                        string avatarFolder = Path.Combine(projectRoot, "Assets", "Avatars");

                        if (!Directory.Exists(avatarFolder))
                            Directory.CreateDirectory(avatarFolder);

                        string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileNameWithoutExtension(selectedFile)}{Path.GetExtension(selectedFile)}";
                        string destPath = Path.Combine(avatarFolder, newFileName);

                        File.Copy(selectedFile, destPath, true);
                        img.Image = Image.FromFile(destPath);
                        img.Tag = $"Assets/Avatars/{newFileName}";

                        AntdUI.Message.success(this.FindForm(), "Ảnh đã được chọn!");
                    }
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this.FindForm(), "Lỗi khi chọn ảnh: " + ex.Message);
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            try
            {
                if (img.Image == null)
                {
                    AntdUI.Message.warn(this.FindForm(), "Không có ảnh để xem!");
                    return;
                }

                using (Form imageForm = new Form())
                {
                    imageForm.Text = _employee?.FullName ?? "Ảnh đại diện";
                    imageForm.Width = 600;
                    imageForm.Height = 600;
                    imageForm.StartPosition = FormStartPosition.CenterParent;
                    imageForm.BackColor = System.Drawing.Color.Black;

                    PictureBox pictureBox = new PictureBox
                    {
                        Image = img.Image,
                        Dock = DockStyle.Fill,
                        SizeMode = PictureBoxSizeMode.Zoom,
                        BackColor = System.Drawing.Color.Black
                    };

                    imageForm.Controls.Add(pictureBox);
                    imageForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this.FindForm(), "Lỗi khi mở ảnh: " + ex.Message);
            }
        }

        private void btnXoaAnh_Click(object sender, EventArgs e)
        {
            try
            {
                if (img.Tag == null)
                {
                    AntdUI.Message.warn(this.FindForm(), "Không có ảnh tùy chọn để xóa!");
                    return;
                }

                string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\.."));
                string avatarFolder = Path.Combine(projectRoot, "Assets", "Avatars");

                if (!Directory.Exists(avatarFolder))
                    Directory.CreateDirectory(avatarFolder);

                string fullName = _employee?.FullName ?? "?";
                Image defaultAvatar = _avatarService.CreateDefaultAvatar(fullName);

                string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_default_{Path.GetRandomFileName().Replace(".", "")}.png";
                string destPath = Path.Combine(avatarFolder, newFileName);

                defaultAvatar.Save(destPath);
                img.Image = defaultAvatar;
                img.Tag = $"Assets/Avatars/{newFileName}";

                AntdUI.Message.success(this.FindForm(), "Đã sử dụng ảnh mặc định!");
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this.FindForm(), "Lỗi khi xóa ảnh: " + ex.Message);
            }
        }

        private void ddownDepartment_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            if (ddownDepartment.SelectedValue != null)
                ddownDepartment.Text = ddownDepartment.SelectedValue.ToString();
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false;
            string digitsOnly = Regex.Replace(phone, @"\D", "");
            if (!Regex.IsMatch(digitsOnly, @"^\d{7,15}$")) return false;
            return true;
        }

        private string NormalizePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return string.Empty;
            return Regex.Replace(phone, @"\D", "");
        }
    }
}
