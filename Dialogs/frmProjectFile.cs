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

namespace EmployeeManagement.Dialogs
{
    public partial class frmProjectFile : Form
    {
        private ProjectFileRepository _projectFileRepository;
        private EmployeeRepository _employeeRepository;
        private FileService _fileService;
        private int _currentProjectId;
        private string _currentProjectName;

        public frmProjectFile()
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
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M257.7 752c2 0 4-.2 6-.5L431.9 722c2-.4 3.9-1.3 5.3-2.8l423.9-423.9a9.96 9.96 0 0 0 0-14.1L694.9 114.9c-1.9-1.9-4.4-2.9-7.1-2.9s-5.2 1-7.1 2.9L256.8 538.8c-1.5 1.5-2.4 3.3-2.8 5.3l-29.5 168.2a33.5 33.5 0 0 0 9.4 29.8c6.6 6.4 14.9 9.9 23.8 9.9zm67.4-174.4L687.8 215l73.3 73.3-362.7 362.6-88.9 15.7 15.6-89zM880 836H144c-17.7 0-32 14.3-32 32v36c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-36c0-17.7-14.3-32-32-32z\"/></svg>"),

    // Nút TẢI - Primary (xanh dương)
    new AntdUI.CellButton("download", "Tải", AntdUI.TTypeMini.Primary)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M505.7 661a8 8 0 0 0 12.6 0l112-141.7c4.1-5.2.4-12.9-6.3-12.9h-74.1V168c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v338.3H400c-6.7 0-10.4 7.7-6.3 12.9l112 141.8zM878 626h-60c-4.4 0-8 3.6-8 8v154H214V634c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v198c0 17.7 14.3 32 32 32h684c17.7 0 32-14.3 32-32V634c0-4.4-3.6-8-8-8z\"/></svg>"),

    // Nút XÓA - Error (đỏ)
    new AntdUI.CellButton("delete", "Xóa", AntdUI.TTypeMini.Error)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M360 184h-8c4.4 0 8-3.6 8-8v8h304v-8c0 4.4 3.6 8 8 8h-8v72h72v-80c0-35.3-28.7-64-64-64H352c-35.3 0-64 28.7-64 64v80h72v-72zm504 72H160c-17.7 0-32 14.3-32 32v32c0 4.4 3.6 8 8 8h60.4l24.7 523c1.6 34.1 29.8 61 63.9 61h454c34.2 0 62.3-26.8 63.9-61l24.7-523H888c4.4 0 8-3.6 8-8v-32c0-17.7-14.3-32-32-32zM731.3 840H292.7l-24.2-512h487l-24.2 512z\"/></svg>")
                     }
                }).ToList();

                tbFiles.DataSource = filesWithType;
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void frmProjectFile_Load(object sender, EventArgs e)
        {
            tbFiles.Columns.Add(new Column("ProjectFileID", "ID") { Width = "5%" });
            tbFiles.Columns.Add(new Column("Title", "File Name") { Width = "22%" });
            tbFiles.Columns.Add(new Column("FileType", "Attachment Type") { Width = "15%" });

            var fileColumn = new Column("File", "Tập tin") { Width = "12%" };
            fileColumn.SetStyle(new AntdUI.Table.CellStyleInfo
            {
                ForeColor = System.Drawing.Color.Blue
            });
            tbFiles.Columns.Add(fileColumn);

            tbFiles.Columns.Add(new Column("CreatedAt", "Date Added") { Width = "15%" });
            tbFiles.Columns.Add(new Column("CreatedByName", "Created By") { Width = "13%" });
            tbFiles.Columns.Add(new Column("Actions", "Actions") { Width = "18%" });

            tbFiles.CellButtonClick += tbFiles_CellButtonClick;

            ddSort.Items.Add("Tên A-Z");
            ddSort.Items.Add("Tên Z-A");
            ddSort.Items.Add("Ngày thêm mới nhất");
            ddSort.Items.Add("Ngày thêm cũ nhất");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var files = _projectFileRepository.GetByProject(_currentProjectId);
                string searchText = txtSearch.Text?.Trim() ?? "";

                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    files = files.Where(f =>
                  (!string.IsNullOrEmpty(f.Title) && f.Title.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0) ||
                     (!string.IsNullOrEmpty(f.FileName) && f.FileName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                     ).ToList();
                }

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
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M257.7 752c2 0 4-.2 6-.5L431.9 722c2-.4 3.9-1.3 5.3-2.8l423.9-423.9a9.96 9.96 0 0 0 0-14.1L694.9 114.9c-1.9-1.9-4.4-2.9-7.1-2.9s-5.2 1-7.1 2.9L256.8 538.8c-1.5 1.5-2.4 3.3-2.8 5.3l-29.5 168.2a33.5 33.5 0 0 0 9.4 29.8c6.6 6.4 14.9 9.9 23.8 9.9zm67.4-174.4L687.8 215l73.3 73.3-362.7 362.6-88.9 15.7 15.6-89zM880 836H144c-17.7 0-32 14.3-32 32v36c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-36c0-17.7-14.3-32-32-32z\"/></svg>"),

    // Nút TẢI - Primary (xanh dương)
    new AntdUI.CellButton("download", "Tải", AntdUI.TTypeMini.Primary)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M505.7 661a8 8 0 0 0 12.6 0l112-141.7c4.1-5.2.4-12.9-6.3-12.9h-74.1V168c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v338.3H400c-6.7 0-10.4 7.7-6.3 12.9l112 141.8zM878 626h-60c-4.4 0-8 3.6-8 8v154H214V634c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v198c0 17.7 14.3 32 32 32h684c17.7 0 32-14.3 32-32V634c0-4.4-3.6-8-8-8z\"/></svg>"),

    // Nút XÓA - Error (đỏ)
    new AntdUI.CellButton("delete", "Xóa", AntdUI.TTypeMini.Error)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M360 184h-8c4.4 0 8-3.6 8-8v8h304v-8c0 4.4 3.6 8 8 8h-8v72h72v-80c0-35.3-28.7-64-64-64H352c-35.3 0-64 28.7-64 64v80h72v-72zm504 72H160c-17.7 0-32 14.3-32 32v32c0 4.4 3.6 8 8 8h60.4l24.7 523c1.6 34.1 29.8 61 63.9 61h454c34.2 0 62.3-26.8 63.9-61l24.7-523H888c4.4 0 8-3.6 8-8v-32c0-17.7-14.3-32-32-32zM731.3 840H292.7l-24.2-512h487l-24.2 512z\"/></svg>")
                  }
                }).ToList();

                tbFiles.DataSource = filesWithType;

                if (filesWithType.Count == 0)
                {
                    Message.warn(this, "Không tìm thấy kết quả phù hợp.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void ddSort_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            try
            {
                var files = _projectFileRepository.GetByProject(_currentProjectId);
                string sortOption = ddSort.SelectedValue.ToString();

                switch (sortOption)
                {
                    case "Tên A-Z":
                        files = files.OrderBy(f => f.Title).ToList();
                        break;

                    case "Tên Z-A":
                        files = files.OrderByDescending(f => f.Title).ToList();
                        break;

                    case "Ngày thêm mới nhất":
                        files = files.OrderByDescending(f => f.CreatedAt).ToList();
                        break;

                    case "Ngày thêm cũ nhất":
                        files = files.OrderBy(f => f.CreatedAt).ToList();
                        break;
                }

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
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M257.7 752c2 0 4-.2 6-.5L431.9 722c2-.4 3.9-1.3 5.3-2.8l423.9-423.9a9.96 9.96 0 0 0 0-14.1L694.9 114.9c-1.9-1.9-4.4-2.9-7.1-2.9s-5.2 1-7.1 2.9L256.8 538.8c-1.5 1.5-2.4 3.3-2.8 5.3l-29.5 168.2a33.5 33.5 0 0 0 9.4 29.8c6.6 6.4 14.9 9.9 23.8 9.9zm67.4-174.4L687.8 215l73.3 73.3-362.7 362.6-88.9 15.7 15.6-89zM880 836H144c-17.7 0-32 14.3-32 32v36c0 4.4 3.6 8 8 8h784c4.4 0 8-3.6 8-8v-36c0-17.7-14.3-32-32-32z\"/></svg>"),

    // Nút TẢI - Primary (xanh dương)
    new AntdUI.CellButton("download", "Tải", AntdUI.TTypeMini.Primary)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M505.7 661a8 8 0 0 0 12.6 0l112-141.7c4.1-5.2.4-12.9-6.3-12.9h-74.1V168c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v338.3H400c-6.7 0-10.4 7.7-6.3 12.9l112 141.8zM878 626h-60c-4.4 0-8 3.6-8 8v154H214V634c0-4.4-3.6-8-8-8h-60c-4.4 0-8 3.6-8 8v198c0 17.7 14.3 32 32 32h684c17.7 0 32-14.3 32-32V634c0-4.4-3.6-8-8-8z\"/></svg>"),

    // Nút XÓA - Error (đỏ)
    new AntdUI.CellButton("delete", "Xóa", AntdUI.TTypeMini.Error)
        .SetIcon("<svg viewBox=\"0 0 1024 1024\"><path d=\"M360 184h-8c4.4 0 8-3.6 8-8v8h304v-8c0 4.4 3.6 8 8 8h-8v72h72v-80c0-35.3-28.7-64-64-64H352c-35.3 0-64 28.7-64 64v80h72v-72zm504 72H160c-17.7 0-32 14.3-32 32v32c0 4.4 3.6 8 8 8h60.4l24.7 523c1.6 34.1 29.8 61 63.9 61h454c34.2 0 62.3-26.8 63.9-61l24.7-523H888c4.4 0 8-3.6 8-8v-32c0-17.7-14.3-32-32-32zM731.3 840H292.7l-24.2-512h487l-24.2 512z\"/></svg>")
                        }
                }).ToList();

                tbFiles.DataSource = filesWithType;
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi sắp xếp: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (SessionManager.CurrentUser == null)
                {
                    Message.error(this, "Vui lòng đăng nhập lại!");
                    return;
                }

                int currentUserId = SessionManager.CurrentUser.UserID;

                var currentEmployee = _employeeRepository.GetById(currentUserId);
                if (currentEmployee == null)
                {
                    Message.error(this, $"Không tìm thấy thông tin nhân viên cho User ID: {currentUserId}. Vui lòng liên hệ Admin!");
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
                            if (_fileService.UploadProjectFile(_currentProjectId, title, filePath, currentUserId))
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
                            Message.success(this, $"Đã tải lên thành công {successCount} file!");
                        }
                        else
                        {
                            Message.warn(this, $"Tải lên thành công {successCount} file, thất bại {failCount} file!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi tải lên file: " + ex.Message);
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

                var modal = new AntdUI.Modal.Config(this, "Sửa tên tập tin", panel, TType.Info)
                {
                    OkText = "Xác nhận",
                    CancelText = "Hủy",
                    OnOk = (cfg) =>
                     {
                         string newTitle = input.Text?.Trim();

                         if (string.IsNullOrWhiteSpace(newTitle))
                         {
                             Message.warn(this, "Tên tập tin không được để trống!");
                             return false;
                         }

                         var projectFile = _projectFileRepository.GetById(fileId);
                         if (projectFile != null)
                         {
                             projectFile.Title = newTitle;
                             if (_projectFileRepository.Update(projectFile))
                             {
                                 LoadData();
                                 Message.success(this, "Cập nhật tên tập tin thành công!");
                                 return true;
                             }
                             else
                             {
                                 Message.error(this, "Cập nhật tên tập tin thất bại!");
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
                Message.error(this, "Lỗi khi sửa tên file: " + ex.Message);
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
                    Message.error(this, "File không tồn tại!");
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
                        Message.success(this, "Tải xuống thành công!");
                    }
                }
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi tải xuống file: " + ex.Message);
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
                          this,
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
                      Message.success(this, "Đã xóa file thành công!");
                  }
                  else
                  {
                      Message.error(this, "Xóa file thất bại!");
                  }
                  return true;
              };
                AntdUI.Modal.open(modalConfig);
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi xóa file: " + ex.Message);
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
                    Message.error(this, "File không tồn tại!");
                }
            }
            catch (Exception ex)
            {
                Message.error(this, "Lỗi khi mở file: " + ex.Message);
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
    }
}