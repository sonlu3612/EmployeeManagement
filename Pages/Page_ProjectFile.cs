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
    public partial class Page_ProjectFile : UserControl
    {
        private ProjectFileRepository _projectFileRepository;
        private EmployeeRepository _employeeRepository;
        private FileService _fileService;
        private int _currentProjectId;
        private string _currentProjectName;
        public Page_ProjectFile()
        {
            InitializeComponent();
            _projectFileRepository = new ProjectFileRepository();
            _employeeRepository = new EmployeeRepository();
            _fileService = new FileService();
        }
        public void LoadProjectFiles(int projectId, string projectName)
        {
            _currentProjectId = projectId;
            _currentProjectName = projectName;
            this.Text = $"Quản lý Tập Tin Dự Án - {projectName}";
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                var files = _projectFileRepository.GetByProject(_currentProjectId);
                var filesWithType = files.Select(f => new
                {
                    f.ProjectFileID,
                    f.ProjectID,
                    f.Title,
                    f.FileName,
                    FileType = GetFileType(f.FileName),
                    File = "Mở tập tin",
                    f.CreatedBy,
                    f.CreatedAt,
                    f.ProjectName,
                    f.CreatedByName,
                    Actions = new AntdUI.CellLink[]
                 {
    // Nút SỬA - Default (xám)
    new AntdUI.CellButton("edit", "Sửa", AntdUI.TTypeMini.Default)
        .SetIcon("EditOutlined"),
    // Nút TẢI - Primary (xanh dương)
    new AntdUI.CellButton("download", "Tải", AntdUI.TTypeMini.Primary)
        .SetIcon("DownloadOutlined"),
    // Nút XÓA - Error (đỏ)
                    new AntdUI.CellButton("delete", "Xóa", AntdUI.TTypeMini.Error).SetIcon("DeleteOutlined")
                     }
                }).ToList();
                tbFiles.DataSource = filesWithType;
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        private void Page_ProjectFile_Load(object sender, EventArgs e)
        {
            tbFiles.Columns.Add(new Column("Title", "File Name") { Width = "22%" });
            tbFiles.Columns.Add(new Column("FileType", "Attachment Type") { Width = "15%" });
            var fileColumn = new Column("File", "Tập tin") { Width = "12%" };
            fileColumn.SetStyle(new AntdUI.Table.CellStyleInfo
            {
                ForeColor = System.Drawing.Color.Blue
            });
            tbFiles.Columns.Add(fileColumn);
            tbFiles.Columns.Add(new Column("CreatedAt", "Date Added") { Width = "15%" });
            tbFiles.Columns.Add(new Column("CreatedByName", "Created By") { Width = "16%" });
            tbFiles.Columns.Add(new Column("Actions", "Actions") { Width = "20%" });
            tbFiles.CellButtonClick += tbFiles_CellButtonClick;
            ddSort.Items.Add("Tên A-Z");
            ddSort.Items.Add("Tên Z-A");
            ddSort.Items.Add("Ngày thêm mới nhất");
            ddSort.Items.Add("Ngày thêm cũ nhất");
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
        private void ddSort_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

        }
        private void tbFiles_CellClick(object sender, TableClickEventArgs e)
        {

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

        }
        private void EditFile(dynamic record)
        {
            try
            {
                int fileId = record.ProjectFileID;
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
                        var projectFile = _projectFileRepository.GetById(fileId);
                        if (projectFile != null)
                        {
                            projectFile.Title = newTitle;
                            if (_projectFileRepository.Update(projectFile))
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
                int fileId = record.ProjectFileID;
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
                    if (_fileService.DeleteProjectFile(fileId))
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

        private void btnAdd_Click_1(object sender, EventArgs e)
        {

        }
    }
}