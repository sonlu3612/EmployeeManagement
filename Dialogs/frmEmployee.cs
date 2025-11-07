using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement.Dialogs
{
    public partial class frmEmployee : Form
    {
        private readonly IRepository<Employee> _employeeRepository = new EmployeeRepository();
        private readonly IRepository<Department> _departmentRepository = new DepartmentRepository();
        private readonly UserRepository _userRepository = new UserRepository();

        //private Employee _employee;
        //private bool IsEdited => _employee != null;
        public frmEmployee()
        {
            InitializeComponent();
            //_employee = employee;
        }

        public void frmEmployee_Load(string DepartmentName)
        {
            ddownGT.Items.Add("Nam");
            ddownGT.Items.Add("Nữ");
            ddownDepartment.Text = DepartmentName;
            ddownDepartment.Enabled = false;

            var departments = _departmentRepository.GetAll().ToList();
            ddownDepartment.Items.Clear();
            ddownDepartment.Items.AddRange(departments.Select(d => d.DepartmentName).ToArray());

            //if (IsEdited)
            //{
            //    txtName.Text = _employee.FullName;
            //    txtEmail.Text = _employee.Email;
            //    txtDienThoai.Text = _employee.Phone;
            //    txtPosition.Text = _employee.Position;
            //    txtDiaChi.Text = _employee.Address;
            //    ddownGT.Text =  _employee.Gender;
            //    dateStart.Value = _employee.HireDate;

            //}
            //else
            //{
               
            //    dateStart.Value = DateTime.Today;
            //}
            //ddownDepartment.Enabled = false;

            //if (!string.IsNullOrEmpty(_employee.AvatarPath))
            //{
            //    string fullPath = Path.Combine(Application.StartupPath, "..", "..", _employee.AvatarPath.Replace("/", "\\"));
            //    if (File.Exists(fullPath))
            //    {
            //        img.Image = Image.FromFile(fullPath);
            //        img.Tag = _employee.AvatarPath;
            //    }
            //}
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtName == null || string.IsNullOrWhiteSpace(txtName.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập họ và tên!");
                return;
            }

            if (txtEmail == null || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập email!");
                return;
            }

            if (ddownGT == null || string.IsNullOrWhiteSpace(ddownGT.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng chọn giới tính!");
                return;
            }

            if (txtPosition == null || string.IsNullOrWhiteSpace(txtPosition.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập chức vụ!");
                return;
            }

            if (ddownDepartment == null || string.IsNullOrWhiteSpace(ddownDepartment.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng chọn phòng ban!");
                return;
            }

            if (txtDienThoai == null || string.IsNullOrWhiteSpace(txtDienThoai.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập điện thoại!");
                return;
            }

            if (dateStart == null)
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng chọn ngày bắt đầu!");
                return;
            }

            var department = _departmentRepository.GetAll()
                .FirstOrDefault(d => d.DepartmentName == ddownDepartment.Text);

            var newUser = new User
            {
                Email = txtEmail.Text.Trim(),
                Phone = txtDienThoai?.Text.Trim(),
                PasswordHash = "123456",
                CreatedDate = DateTime.Now,
                IsActive = true
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
                FullName = txtName.Text.Trim(),
                Address = txtDiaChi.Text.Trim(),
                Position = txtPosition.Text.Trim(),
                Gender = string.IsNullOrWhiteSpace(ddownGT.Text) ? "NotSpecified" : ddownGT.Text.Trim(),
                DepartmentID = department.DepartmentID,
                HireDate = (DateTime)dateStart.Value,
                IsActive = true
            };

            this.Tag = newEmployee;
            this.DialogResult = DialogResult.OK;
            this.Close();
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
                    openFileDialog.Filter = "Ảnh (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string selectedFile = openFileDialog.FileName;

                        string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
                        string avatarFolder = Path.Combine(projectRoot, "Assets", "Avatars");
                        Console.WriteLine(projectRoot);
                        if (!Directory.Exists(avatarFolder))
                            Directory.CreateDirectory(avatarFolder);

                        string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(selectedFile)}";
                        string destPath = Path.Combine(avatarFolder, newFileName);

                        File.Copy(selectedFile, destPath, true);

                        img.Image = Image.FromFile(destPath);

                        img.Tag = $"Assets/Avatars/{newFileName}";
                    }
                }
            }
            catch (Exception ex)
            {
                AntdUI.Message.error(this.FindForm(), "Lỗi khi chọn ảnh: " + ex.Message);
            }
        }
    }
}
