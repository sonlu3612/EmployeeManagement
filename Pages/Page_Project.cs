using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = AntdUI.Message;


namespace EmployeeManagement.Pages
{
    public partial class Page_Project : UserControl
    {
        private readonly ProjectRepository _projectRepository = new ProjectRepository();

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

            LoadData();
        }

        private void LoadData()
        {
            tbProject.DataSource = _projectRepository.GetAll().ToList();
            if (cbNhanVien.Items.Count == 0)
            {
                loadEmployeesName();
            }
            if (cbTrangThai.Items.Count == 0)
            {
                cbTrangThai.Items.AddRange(_projectRepository.GetAll().Select(p => p.Status).Distinct().ToArray());
            }
        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void loadEmployeesName()
        {
            var employees = employeeRepository.GetAll();

            cbNhanVien.Items.Clear();
            cbNhanVien.Items.Insert(0, "Tất cả");

            foreach (var emp in employees)
            {
                string displayText = $"{emp.EmployeeID} - {emp.FullName}";
                cbNhanVien.Items.Add(displayText);
            }
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

        }

        private void tbProject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tbProject.SelectedIndex;
                if (sel >= 0)
                {
                    ctm1.Show(Cursor.Position);
                }
            }
        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbProject.SelectedIndex - 1;
            if (tbProject.DataSource is IList<Project> projects && selectedIndex >= 0 && selectedIndex < projects.Count)
            {
                var record = projects[selectedIndex];
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!"); return; }
                int id = record.ProjectID;
                frmManageTasks frm = new frmManageTasks();
                frm.frmManageTasks_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";

                var projects = _projectRepository.GetAll().AsEnumerable();

                if (!string.IsNullOrWhiteSpace(q))
                {
                    projects = projects.Where(p =>
                        !string.IsNullOrEmpty(p.ProjectName) &&
                        p.ProjectName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                if (!string.IsNullOrWhiteSpace(cbNhanVien.Text) && cbNhanVien.Text != "Nhân viên")
                {
                    string empFilter = cbNhanVien.Text.Trim();
                    projects = projects.Where(p => p.CreatedBy.ToString().Contains(empFilter));
                }

                if (!string.IsNullOrWhiteSpace(cbTrangThai.Text) && cbTrangThai.Text != "Trạng thái")
                {
                    string statusFilter = cbTrangThai.Text.Trim();
                    projects = projects.Where(p => !string.IsNullOrEmpty(p.Status) &&
                                                   p.Status.IndexOf(statusFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                var result = projects.ToList();

                tbProject.DataSource = result;

                if (result.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} dự án.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }


        private void cbNhanVien_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            //cbNhanVien.Text = e.Value?.ToString() ?? "";
            //var filteredProjects = _projectRepository.GetAll()
            //    .Where(p => p.CreatedBy.ToString().Contains(cbNhanVien.Text))
            //    .ToList();
            //tbProject.DataSource = filteredProjects;
            string selected = cbNhanVien.SelectedValue.ToString();

            try
            {

                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    LoadData();
                }
                else
                {
                    List<Project> projects = new List<Project>();


                    int employeeId = int.Parse(selected.Split('-')[0].Trim());
                    projects = _projectRepository.GetByEmployee(employeeId);
                    tbProject.DataSource = projects.ToList();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dự án: " + ex.Message);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            cbNhanVien.Text = "Nhân viên";
            cbTrangThai.Text = "Trạng thái";
        }

        private void cbTrangThai_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            cbTrangThai.Text = e.Value?.ToString() ?? "";
            var filteredProjects = _projectRepository.GetAll()
                .Where(p => p.Status.Contains(cbTrangThai.Text))
                .ToList();
            tbProject.DataSource = filteredProjects;
        }
    }
}
