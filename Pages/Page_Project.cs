using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Project : UserControl
    {
        private readonly IRepository<Project> _projectRepository = new ProjectRepository();

        public Page_Project()
        {
            InitializeComponent();
        }

        public void Page_Project_Load()
        {
            // Cấu hình các cột trong bảng
            tbProject.Columns.Add(new Column("ProjectID", "ID"));
            tbProject.Columns.Add(new Column("ProjectName", "Project Name"));
            tbProject.Columns.Add(new Column("Description", "Description"));
            tbProject.Columns.Add(new Column("StartDate", "Start Date"));
            tbProject.Columns.Add(new Column("EndDate", "End Date"));
            tbProject.Columns.Add(new Column("Status", "Status"));
            tbProject.Columns.Add(new Column("CreatedBy", "Created By"));
            tbProject.Columns.Add(new Column("CreatedDate", "Created Date"));

            LoadData();
        }

        private void LoadData()
        {
            tbProject.DataSource = _projectRepository.GetAll().ToList();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbProject.SelectedIndex - 1;

            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }

            if (tbProject.DataSource is IList<Project> projects && selectedIndex < projects.Count)
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

        private void tbProject_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbProject.SelectedIndex;
            if (tbProject.DataSource is List<Project> projects && selectedIndex >= 0 && selectedIndex < projects.Count)
            {
                var project = projects[selectedIndex];

                frmProject frm = new frmProject(project);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var updatedProject = frm.Tag as Project;
                    if (updatedProject != null)
                    {
                        _projectRepository.Update(updatedProject);
                        LoadData();
                        Message.success(this.FindForm(), "Cập nhật dự án thành công!");
                    }
                }
            }
            else
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần sửa!");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmProject frm = new frmProject(); 
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newProject = frm.Tag as Project;
                if (newProject != null)
                {
                    _projectRepository.Insert(newProject);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm dự án thành công!");
                }
            }
        }

        private void tbProject_CellClick(object sender, TableClickEventArgs e)
        {
            frmManageTasks frm = new frmManageTasks();
            frm.frmManageTasks_Load();
            frm.ShowDialog(); 
        }
    }
}
