using AntdUI;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.DAL.Services;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_TaskFile : UserControl
    {
        private TaskFileRepository _taskFileRepository;
        private EmployeeRepository _employeeRepository;
        private FileService _fileService;
        private int _currentTaskId;
        private string _currentTaskName;
        private List<dynamic> originalFiles; // Lưu danh sách gốc để filter/sort

        public Page_TaskFile()
        {
            InitializeComponent();
            _taskFileRepository = new TaskFileRepository();
            _employeeRepository = new EmployeeRepository();
            _fileService = new FileService();
        }

        public void LoadTaskFiles(int taskId, string taskName)
        {
            _currentTaskId = taskId;
            Console.WriteLine(_currentTaskId);
            _currentTaskName = taskName;
            this.Text = $"Quản lý Tập Tin Nhiệm Vụ - {taskName}";
            tbFiles.Columns.Add(new Column("Title", "Tên file") { Width = "22%" });
            tbFiles.Columns.Add(new Column("FileType", "Loại file") { Width = "15%" });
            var fileColumn = new Column("File", "Tập tin") { Width = "12%" };
            fileColumn.SetStyle(new AntdUI.Table.CellStyleInfo
            {
                ForeColor = System.Drawing.Color.Blue
            });
            tbFiles.Columns.Add(fileColumn);
            tbFiles.Columns.Add(new Column("CreatedAt", "Ngày thêm") { Width = "15%" });
            tbFiles.Columns.Add(new Column("CreatedByName", "Thêm bởi") { Width = "16%" });
            tbFiles.Columns.Add(new Column("Actions", "Hành động") { Width = "20%" });
            // tbFiles.CellButtonClick += tbFiles_CellButtonClick;
            ddSort.Items.Add("Tên A-Z");
            ddSort.Items.Add("Tên Z-A");
            ddSort.Items.Add("Ngày thêm mới nhất");
            ddSort.Items.Add("Ngày thêm cũ nhất");
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var files = _taskFileRepository.GetByTask(_currentTaskId);
                originalFiles = files.Select(f => new
                {
                    f.TaskFileID,
                    f.TaskID,
                    f.Title,
                    f.FileName,
                    FileType = GetFileType(f.FileName),
                    File = "Mở tập tin",
                    f.CreatedBy,
                    f.CreatedAt,
                    f.TaskTitle,
                    f.CreatedByName,
                    Actions = new AntdUI.CellLink[]
                    {
                        new AntdUI.CellButton("edit", "Sửa", AntdUI.TTypeMini.Default).SetIcon("EditOutlined"),
                        new AntdUI.CellButton("download", "Tải", AntdUI.TTypeMini.Primary).SetIcon("DownloadOutlined"),
                        new AntdUI.CellButton("delete", "Xóa", AntdUI.TTypeMini.Error).SetIcon("DeleteOutlined")
                    }
                }).ToList<dynamic>();
                tbFiles.DataSource = originalFiles;
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void Page_TaskFile_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText = txtSearch.Text?.Trim().ToLower() ?? "";
                if (string.IsNullOrEmpty(searchText))
                {
                    tbFiles.DataSource = originalFiles;
                    return;
                }

                var filtered = originalFiles.Where(f =>
                    (f.Title?.ToString().ToLower().Contains(searchText) ?? false) ||
                    (f.FileName?.ToString().ToLower().Contains(searchText) ?? false)
                ).ToList();

                tbFiles.DataSource = filtered;
                if (filtered.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void ddSort_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            if (originalFiles == null || originalFiles.Count == 0) return;

            string sortOption = e.Value?.ToString() ?? "";
            List<dynamic> sorted = new List<dynamic>(originalFiles);

            switch (sortOption)
            {
                case "Tên A-Z":
                    sorted = sorted.OrderBy(f => f.Title?.ToString()).ToList();
                    break;
                case "Tên Z-A":
                    sorted = sorted.OrderByDescending(f => f.Title?.ToString()).ToList();
                    break;
                case "Ngày thêm mới nhất":
                    sorted = sorted.OrderByDescending(f => f.CreatedAt).ToList();
                    break;
                case "Ngày thêm cũ nhất":
                    sorted = sorted.OrderBy(f => f.CreatedAt).ToList();
                    break;
                default:
                    return;
            }

            tbFiles.DataSource = sorted;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.CurrentUser == null)
                {
                    Message.error(this.FindForm(), "Vui lòng đăng nhập lại!");
                    return;
                }
                int currentUserId = SessionManager.CurrentUser.UserID;
                var currentEmployee = _employeeRepository.GetById(currentUserId);
                if (currentEmployee == null)
                {
                    Message.error(this.FindForm(), $"Không tìm thấy thông tin nhân viên cho User ID: {currentUserId}. Vui lòng liên hệ Admin!");
                    return;
                }
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Title = "Chọn file để tải lên";
                    openFileDialog.Filter = "All Files|*.*|Documents|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx|Images|*.jpg;*.jpeg;*.png|Archives|*.zip;*.rar";
                    openFileDialog.Multiselect = true;
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        int successCount = 0;
                        int failCount = 0;
                        foreach (string filePath in openFileDialog.FileNames)
                        {
                            string title = Path.GetFileNameWithoutExtension(filePath);
                            if (_fileService.UploadTaskFile(_currentTaskId, title, filePath, currentUserId))
                            {
                                successCount++;
                            }
                            else
                            {
                                failCount++;
                            }
                        }
                        LoadData();
                        if (failCount == 0)
                        {
                            Message.success(this.FindForm(), $"Đã tải lên thành công {successCount} file!");
                        }
                        else
                        {
                            Message.warn(this.FindForm(), $"Tải lên thành công {successCount} file, thất bại {failCount} file!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải lên file: " + ex.Message);
            }
        }

        private void tbFiles_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Record == null) return;
            dynamic record = e.Record;
            if (e.Column.Key == "File")
            {
                OpenFile(record.FileName);
            }
            else if (e.Column.Key == "CreatedByName")
            {
                // TODO: May be in the future we can show employee details when clicking on the creator's name
                //frmEmployee frm = new frmEmployee();
                //frm.ShowDialog();
            }
        }

        private void tbFiles_CellButtonClick(object sender, AntdUI.TableButtonEventArgs e)
        {
            if (e.Record == null) return;
            dynamic record = e.Record;
            if (e.Btn.Id == "edit")
            {
                EditFile(record);
            }
            else if (e.Btn.Id == "download")
            {
                DownloadFile(record);
            }
            else if (e.Btn.Id == "delete")
            {
                DeleteFile(record);
            }
        }

        private void tbFiles_CellDoubleClick(object sender, TableClickEventArgs e)
        {
            if (e.Record != null)
            {
                dynamic record = e.Record;
                OpenFile(record.FileName);
            }
        }

        private void EditFile(dynamic record)
        {
            try
            {
                int fileId = record.TaskFileID;
                string currentTitle = record.Title;
                var panel = new System.Windows.Forms.Panel
                {
                    Height = 80,
                    Padding = new System.Windows.Forms.Padding(15, 10, 15, 10)
                };
                var label = new AntdUI.Label
                {
                    Text = "Tên tập tin:",
                    Dock = DockStyle.Top,
                    Height = 25,
                    Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold)
                };
                var input = new AntdUI.Input
                {
                    Text = currentTitle,
                    PlaceholderText = "Nhập tên tập tin",
                    Dock = DockStyle.Bottom,
                    Height = 35
                };
                panel.Controls.Add(input);
                panel.Controls.Add(label);
                var modal = new AntdUI.Modal.Config(this.FindForm(), "Sửa tên tập tin", panel, TType.Info)
                {
                    OkText = "Xác nhận",
                    CancelText = "Hủy",
                    OnOk = (cfg) =>
                    {
                        string newTitle = input.Text?.Trim();
                        if (string.IsNullOrWhiteSpace(newTitle))
                        {
                            Message.warn(this.FindForm(), "Tên tập tin không được để trống!");
                            return false;
                        }
                        var taskFile = _taskFileRepository.GetById(fileId);
                        if (taskFile != null)
                        {
                            taskFile.Title = newTitle;
                            if (_taskFileRepository.Update(taskFile))
                            {
                                LoadData();
                                Message.success(this.FindForm(), "Cập nhật tên tập tin thành công!");
                                return true;
                            }
                            else
                            {
                                Message.error(this.FindForm(), "Cập nhật tên tập tin thất bại!");
                                return false;
                            }
                        }
                        return false;
                    }
                };
                AntdUI.Modal.open(modal);
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi sửa tên file: " + ex.Message);
            }
        }

        private void DownloadFile(dynamic record)
        {
            try
            {
                string fileName = record.FileName;
                string sourceFilePath = _fileService.GetFilePath(fileName);
                if (!File.Exists(sourceFilePath))
                {
                    Message.error(this.FindForm(), "File không tồn tại!");
                    return;
                }
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = record.Title + Path.GetExtension(fileName);
                    saveFileDialog.Filter = "All Files|*.*";
                    saveFileDialog.Title = "Lưu tập tin";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.Copy(sourceFilePath, saveFileDialog.FileName, true);
                        Message.success(this.FindForm(), "Tải xuống thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải xuống file: " + ex.Message);
            }
        }

        private void DeleteFile(dynamic record)
        {
            try
            {
                int fileId = record.TaskFileID;
                string fileName = record.Title;
                string fileType = record.FileType;
                string dateAdded = Convert.ToDateTime(record.CreatedAt).ToString("dd/MM/yyyy HH:mm");
                string createdBy = record.CreatedByName;
                string confirmMessage = $"Bạn có chắc muốn xóa file này không?\n\n" +
                                        $"Tên file: {fileName}\n" +
                                        $"Loại file: {fileType}\n" +
                                        $"Ngày thêm: {dateAdded}\n" +
                                        $"Người tạo: {createdBy}";
                var modalConfig = AntdUI.Modal.config(
                    this.FindForm(),
                    "Xác nhận xóa",
                    confirmMessage,
                    TType.Warn
                );
                modalConfig.OkText = "Xóa";
                modalConfig.CancelText = "Hủy";
                modalConfig.OkType = TTypeMini.Error;
                modalConfig.OnOk = (cfg) =>
                {
                    if (_fileService.DeleteTaskFile(fileId))
                    {
                        LoadData();
                        Message.success(this.FindForm(), "Đã xóa file thành công!");
                    }
                    else
                    {
                        Message.error(this.FindForm(), "Xóa file thất bại!");
                    }
                    return true;
                };
                AntdUI.Modal.open(modalConfig);
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi xóa file: " + ex.Message);
            }
        }

        private void OpenFile(string fileName)
        {
            try
            {
                string filePath = _fileService.GetFilePath(fileName);
                if (File.Exists(filePath))
                {
                    Process.Start(filePath);
                }
                else
                {
                    Message.error(this.FindForm(), "File không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi mở file: " + ex.Message);
            }
        }

        private string GetFileType(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return "Unknown";
            string extension = Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".pdf":
                    return "PDF Document";
                case ".doc":
                case ".docx":
                    return "Word Document";
                case ".xls":
                case ".xlsx":
                    return "Excel Spreadsheet";
                case ".ppt":
                case ".pptx":
                    return "PowerPoint Presentation";
                case ".jpg":
                case ".jpeg":
                case ".png":
                case ".gif":
                    return "Image";
                case ".zip":
                case ".rar":
                case ".7z":
                    return "Archive";
                case ".txt":
                    return "Text File";
                default:
                    return extension.TrimStart('.');
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            txtSearch.Text = string.Empty;
            LoadData();
        }
    }
}