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
        public event EventHandler ProfileUpdated;
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        public event EventHandler ProfileUpdated;

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
                    IsActive = true
                };
                // Nếu avatar được chọn mới, chỉ lưu đường dẫn
                if (avatar1.Tag != null)
                {
                    employee.AvatarPath = avatar1.Tag.ToString();
                }
                if (employeeRepository.UpdateWithContact(employee))
                {
                    Message.success(this.FindForm(), "Cập nhật thông tin nhân viên thành công!");
                    ProfileUpdated?.Invoke(this, EventArgs.Empty);
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
            label1.Text = employee.FullName ?? "Chưa cập nhật";
            lbl2.Text = employee.Email ?? "Chưa cập nhật";

            string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
            string normalizedPath = employee.AvatarPath?.TrimStart('/', '\\') ?? "";

            if (string.IsNullOrEmpty(normalizedPath))
            {
                return;
            }

            string absolutePath = Path.Combine(projectRoot, normalizedPath);

            try
            {
                if (File.Exists(absolutePath))
                {
                    avatar1.Image = Image.FromFile(absolutePath);
                }
                else
                {
                    // avatar1.Image = Properties.Resources.default_avatar;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi load avatar cho EmployeeID {employee.EmployeeID}: {ex.Message}");
                // avatar1.Image = Properties.Resources.default_avatar;
            }

            // Set giá trị cho các input edit
            txtMaNhanVien.Text = employee.EmployeeID.ToString();
            txtMaNhanVien.Enabled = false;
            txtHoTen.Text = employee.FullName ?? "";
            txtHoTen.Enabled = false;
            txtPhongBan.Text = employee.DepartmentName ?? "";
            txtPhongBan.Enabled = false;
            txtChucVu.Text = employee.Position ?? "";
            txtChucVu.Enabled = false;
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
        private void lblEmail_Click(object sender, EventArgs e)
        {
        }
    }
}