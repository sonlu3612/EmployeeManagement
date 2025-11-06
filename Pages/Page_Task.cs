using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Message = AntdUI.Message;
using MyTask = EmployeeManagement.Models.Task;
using Task = EmployeeManagement.Models.Task;
namespace EmployeeManagement.Pages
{
    public partial class Page_Task : UserControl
    {
        public Page_Task()
        {
            InitializeComponent();
        }
        private TaskRepository taskRepository = new TaskRepository();
        
        private void Page_Task_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            tableTask.Columns.Add(new Column("ProjectName", "Dự án"));
            tableTask.Columns.Add(new Column("TaskName", "Nhiệm vụ"));
            tableTask.Columns.Add(new Column("EmployeeName", "Tạo bởi"));
            tableTask.Columns.Add(new Column("CreatedDate", "Ngày tạo"));
            tableTask.Columns.Add(new Column("Deadline", "Hạn hoàn thành"));
            tableTask.Columns.Add(new Column("Status", "Trạng thái"));
            tableTask.Columns.Add(new Column("Progress", "Tiến triển"));
            tableTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            loadData();
            loadEmployeesName();
        }
        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }
        private void loadData()
        {
            tableTask.DataSource = null;
            try
            {
                var tasks = taskRepository.GetAll();
                tableTask.DataSource = tasks.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }
        private void tableTask_CellClick(object sender, TableClickEventArgs e)
        {
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            loadData();
        }
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void loadEmployeesName()
        {
            var employees = employeeRepository.GetAll();
            ddownEmployee.Items.Clear();
            ddownEmployee.Items.Insert(0, "Tất cả");
            foreach (var emp in employees)
            {
                string displayText = $"{emp.EmployeeID} - {emp.FullName}";
                ddownEmployee.Items.Add(displayText);
            }
        }
        private void ddownEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
            tableTask.DataSource = null;
            string selected = ddownEmployee.SelectedValue.ToString();
            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    loadData();
                }
                else
                {
                    int employeeId = int.Parse(selected.Split('-')[0].Trim());
                    Console.WriteLine(employeeId);
                    List<Task> tasks = taskRepository.GetAll().Where(t => t.CreatedBy == employeeId).ToList();
                    var all = taskRepository.GetAll();
                    Console.WriteLine("tasks count: " + all.Count);
                    foreach (var t in all) Console.WriteLine($"TaskID={t.TaskID}, CreatedBy={t.CreatedBy}");

                    tableTask.DataSource = tasks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }
        private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            tableTask.DataSource = null;
            string selected = ddownStatus.SelectedValue.ToString();
            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    loadData();
                }
                else if (selected == "Quá hạn")
                {
                    List<MyTask> tasks = new List<MyTask>();
                    tasks = taskRepository.GetOverdueTasks();
                    //foreach(var task in tasks)
                    //{
                    // task.Status = "Quá hạn";
                    // taskRepository.Update(task);
                    //}
                    tableTask.DataSource = tasks.ToList();
                }
                else
                {
                    List<MyTask> tasks = new List<MyTask>();
                    List<MyTask> tasksByStatus = new List<MyTask>();
                    tasks = taskRepository.GetAll();
                    foreach (var task in tasks)
                    {
                        if (task.Status == selected)
                        {
                            tasksByStatus.Add(task);
                        }
                    }
                    tableTask.DataSource = tasksByStatus.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Cập nhật")
            {
                var selectedIndex = tableTask.SelectedIndex - 1;
                if (selectedIndex >= 0 && tableTask.DataSource is List<MyTask> datalist)
                {
                    var task = datalist[selectedIndex];
                    frmTask frmTask = new frmTask(task);
                    frmTask.ShowDialog();
                    //Console.WriteLine($"Clicked on Task ID: {task.TaskID} - {task.TaskName}");
                }
            }
            else if (e.ClickedItem.Text == "Danh sách nhân viên")
            {
                var selectedIndex = tableTask.SelectedIndex - 1;
                if (selectedIndex >= 0 && tableTask.DataSource is List<MyTask> datalist)
                {
                    var task = datalist[selectedIndex];
                    frmAssignEmployee frmAssignEmployee = new frmAssignEmployee(task.TaskID);
                    frmAssignEmployee.ShowDialog();
                    //Console.WriteLine($"Clicked on Task ID: {task.TaskID} - {task.TaskName}");
                }
            }
            else if (e.ClickedItem.Text == "Cập nhật tiến độ")
            {
                var selectedIndex = tableTask.SelectedIndex - 1;
                if (selectedIndex >= 0 && tableTask.DataSource is List<MyTask> datalist)
                {
                    var task = datalist[selectedIndex];
                    frmSubmit frm = new frmSubmit(task, SessionManager.CurrentUser.UserID);
                    frm.ShowDialog();
                }
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tableTask.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }
            if (selectedIndex >= 0 && tableTask.DataSource is List<MyTask> datalist)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn công việc cần xóa!");
                return;
            }
            if (tableTask.DataSource is IList<MyTask> list && selectedIndex < list.Count)
            {
                var task = list[selectedIndex];
                //string massage = "Bạn có muốn xóa?";
                //frmInforXoa frmInforXoa = new frmInforXoa(massage);
                //if(frmInforXoa.ShowDialog() == DialogResult.OK)
                //{
                // TaskRepository taskRepository = new TaskRepository();
                // taskRepository.Delete(task.TaskID);
                // loadData();
                //}
                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá nhiệm vụ \"{task.TaskName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Primary;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        taskRepository.Delete(task.TaskID);
                        loadData();
                        Message.success(this.FindForm(), "Xóa nhiệm vụ thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá nhiệm vụ: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
                loadData();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu nhiệm vụ được chọn!");
            }
        }
        private void menuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tableTask.SelectedIndex;
                if (sel >= 0)
                {
                    menuStrip.Show(Cursor.Position);
                }
            }
        }
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tableTask.SelectedIndex - 1;
            if (tableTask.DataSource is IList<EmployeeManagement.Models.Task> tasks && selectedIndex >= 0 && selectedIndex < tasks.Count)
            {
                var record = tasks[selectedIndex];
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!"); return; }
                int id = record.ProjectID;
                ManageEmployee frm = new ManageEmployee();
                //frm.frmManageTasks_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }

        private void cậpNhậtTiếnĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}