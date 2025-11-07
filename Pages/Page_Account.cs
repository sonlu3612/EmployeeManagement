using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Account : UserControl
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();

        public Page_Account()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
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
                    Gender = txtGioiTinh.Text,
                };

                // Nếu avatar được chọn mới, chỉ lưu đường dẫn
                if (avatar1.Tag != null)
                {
                    employee.AvatarPath = avatar1.Tag.ToString();
                }

                if (employeeRepository.Update(employee))
                {
                    Message.success(this.FindForm(), "Cập nhật thông tin nhân viên thành công!");
                }
                else
                {
                    Message.error(this.FindForm(), "Lỗi khi cập nhật thông tin!");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lưu nhân viên: " + ex.Message);
            }
        }

        private void Page_Account_Load(object sender, EventArgs e)
        {
            // TODO: Load employee theo UserID nếu cần
        }

        public void LoadProfile(Employee employee)
        {
            lblTen.Text = employee.FullName ?? "Chưa cập nhật";
            lblEmail.Text = employee.Email ?? "Chưa cập nhật";

            if (!string.IsNullOrEmpty(employee.AvatarPath))
            {
                string absolutePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, employee.AvatarPath);
                if (File.Exists(absolutePath))
                {
                    avatar1.Image = Image.FromFile(absolutePath);
                }
                //else
                //{
                //    avatar1.Image = Properties.Resources.default_avatar;
                //}
            }
            //else
            //{
            //    avatar1.Image = Properties.Resources.default_avatar;
            //}


            // Avatar: chỉ đọc từ AvatarPath
            //if (!string.IsNullOrEmpty(employee.AvatarPath) && File.Exists(employee.AvatarPath))
            //{
            //    avatar1.Image = Image.FromFile(employee.AvatarPath);
            //}
            //else
            //{
            //    avatar1.Image = Properties.Resources.default_avatar; // ảnh mặc định
            //}

            // Set giá trị cho các input edit
            txtMaNhanVien.Text = employee.EmployeeID.ToString();
            txtHoTen.Text = employee.FullName ?? "";
            txtPhongBan.Text = employee.DepartmentName ?? "";
            txtChucVu.Text = employee.Position ?? "";
            txtDiaChi.Text = employee.Address ?? "";
            txtSoDienThoai.Text = employee.Phone ?? "";
            txtEmail.Text = employee.Email ?? "";
            txtGioiTinh.Text = employee.Gender ?? "";
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
                        if (!Directory.Exists(avatarFolder))
                            Directory.CreateDirectory(avatarFolder);

                        string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(selectedFile)}";
                        string destPath = Path.Combine(avatarFolder, newFileName);

                        File.Copy(selectedFile, destPath, true);

                        avatar1.Image = Image.FromFile(destPath);

                        // chỉ lưu đường dẫn tương đối
                        avatar1.Tag = $"Assets/Avatars/{newFileName}";
                    }
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi chọn ảnh: " + ex.Message);
            }
        }
    }
}
