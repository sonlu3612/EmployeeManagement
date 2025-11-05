using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
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
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Employee : UserControl
    {
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        public Page_Employee()
        {
            InitializeComponent();
        }

        private void table1_CellClick(object sender, AntdUI.TableClickEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void Page_Employee_Load(object sender, EventArgs e)
        {
            tbNV.Columns.Add(new AntdUI.Column("AvatarPath", "Ảnh đại diện")
            {
                Render = (value, rowData, rowIndex) =>
                {
                    try
                    {
                        var dict = rowData as Dictionary<string, object>;
                        string avatarPath = dict?["AvatarPath"]?.ToString();

                        Console.WriteLine($"[Row {rowIndex}] AvatarPath = {avatarPath}");

                        if (!string.IsNullOrEmpty(avatarPath))
                        {
                            if (!Path.IsPathRooted(avatarPath))
                            {
                                string baseDir = Application.StartupPath; 
                                avatarPath = Path.Combine(baseDir, avatarPath.TrimStart('/', '\\'));
                            }

                            if (File.Exists(avatarPath))
                            {
                                Console.WriteLine($"✅ Tìm thấy ảnh tại: {avatarPath}");
                                return new AntdUI.Image3D()
                                {
                                    Image = Image.FromFile(avatarPath),
                                    Width = 48,
                                    Height = 48,
                                    Radius = 24,
                                    Shadow = 0
                                };
                            }
                            else
                            {
                                Console.WriteLine($"❌ Không tìm thấy file: {avatarPath}");
                            }
                        }

                        return new AntdUI.Image3D()
                        {
                            Image = Properties.Resources.back,
                            Width = 48,
                            Height = 48,
                            Radius = 24,
                            Shadow = 0
                        };
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Lỗi render ảnh: {ex.Message}");
                        return new Label() { Text = "Lỗi ảnh", AutoSize = true };
                    }
                }

            });


            tbNV.Columns.Add(new AntdUI.Column("FullName", "Họ và tên"));
            tbNV.Columns.Add(new AntdUI.Column("Gender", "Giới tính"));
            tbNV.Columns.Add(new AntdUI.Column("Email", "Email"));
            tbNV.Columns.Add(new AntdUI.Column("Role", "Quyền"));
            tbNV.Columns.Add(new AntdUI.Column("ProjectSummary", "Dự án"));
            tbNV.Columns.Add(new AntdUI.Column("TaskSummary", "Nhiệm vụ"));

            LoadData();        
        }

        private void LoadData()
        {
            tbNV.DataSource = employeeRepository.GetForGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeManagement.Dialogs.frmEmployee frm = new EmployeeManagement.Dialogs.frmEmployee();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newEmployee = frm.Tag as Employee;
                if (newEmployee != null)
                {
                    employeeRepository.Insert(newEmployee);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm nhân viên thành công!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbNV.SelectedIndex - 1;

            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }

            if (tbNV.DataSource is IList<Employee> projects && selectedIndex < projects.Count)
            {
                var record = projects[selectedIndex];

                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                    return;
                }

                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá dự án \"{record.ProjectName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        _projectRepository.Delete(record.ProjectID);
                        LoadData();

                        Message.success(this.FindForm(), "Xóa dự án thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá dự án: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }
    }
}
