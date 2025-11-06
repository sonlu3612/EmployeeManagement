using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = AntdUI.Message;
namespace EmployeeManagement.Pages
{
    public partial class Page_Account : UserControl
    {
        //private int userID;
        public Page_Account()
        {
            InitializeComponent();
        }

        //public Page_Account(User user)
        //{
        //    InitializeComponent();
        //}

        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var employee = new Employee()
            {
                EmployeeID = int.Parse(txtMaNhanVien.Text),
                FullName = txtHoTen.Text,
                DepartmentName = txtPhongBan.Text,
                Position = txtChucVu.Text,
                Address = txtDiaChi.Text,
                Phone = txtSoDienThoai.Text,
                Email = txtEmail.Text,
                Gender = txtGioiTinh.Text
            };

            if (employeeRepository.Update(employee))
            {
                Message.success(this.FindForm(), "Cập nhật thông tin nhân viên thành công!");
            }
            else
            {
                Message.error(this.FindForm(),"Lỗi khi cập nhật thông tin!");
            }



        }

        private void Page_Account_Load(object sender, EventArgs e)
        {
            //var employee = employeeRepository.GetFromIdUser(_currentUser.UserID);
            //if (employee == null)
            //{
            //    MessageBox.Show("Không tìm thấy nhân viên cho UserID = " + _currentUser.UserID);
            //    return;
            //}

            //page_Account1.LoadProfile(employee);

        }

        public void LoadProfile(Employee employee )
        {
            // Hiển thị summary
            lblTen.Text = employee.FullName;
            lblEmail.Text = employee.Email;

            // Avatar
            if (employee.AvatarData != null)
            {
                using (var ms = new MemoryStream(employee.AvatarData))
                {
                    avatar1.Image = Image.FromStream(ms);
                }
            }
            else if (!string.IsNullOrEmpty(employee.AvatarPath) && File.Exists(employee.AvatarPath))
            {
                avatar1.Image = Image.FromFile(employee.AvatarPath);
            }
            //else
            //{
            //    avatar1.Image = Properties.Resources.default_avatar; // Ảnh mặc định
            //    avatar1.Text = employee.FullName?.Substring(0, 1).ToUpper() ?? "U";
            //}

            // Set giá trị cho các input edit (nếu là form edit profile)
            txtMaNhanVien.Text = employee.EmployeeID.ToString();
            txtHoTen.Text = employee.FullName;
            txtPhongBan.Text = employee.DepartmentName;
            txtChucVu.Text = employee.Position;
            txtDiaChi.Text = employee.Address;
            txtSoDienThoai.Text = employee.Phone;
            txtEmail.Text = employee.Email;
            txtGioiTinh.Text = employee.Gender;

            
        }

        private void avatar1_Click(object sender, EventArgs e)
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
                        //MessageBox.Show($"Ảnh được lưu tại:\n{destPath}");

                        avatar1.Image = Image.FromFile(destPath);

                        avatar1.Tag = $"Assets/Avatars/{newFileName}";
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
