using System;
using System.IO;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL.Services
{
    public class FileService
    {
        private readonly EmployeeFileRepository _employeeFileRepo;
        private readonly ProjectFileRepository _projectFileRepo;
        private readonly TaskFileRepository _taskFileRepo;
        private readonly EmployeeRepository _employeeRepo;

        public FileService()
        {
            _employeeFileRepo = new EmployeeFileRepository();
            _projectFileRepo = new ProjectFileRepository();
            _taskFileRepo = new TaskFileRepository();
            _employeeRepo = new EmployeeRepository();
        }

        public bool UploadEmployeeFile(int employeeId, string title, string sourceFilePath, int uploadedBy)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine("[FileService.UploadEmployeeFile] File không tồn tại");
                    return false;
                }

                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png" };
                string extension = Path.GetExtension(sourceFilePath).ToLower();
                if (Array.IndexOf(allowedExtensions, extension) < 0)
                {
                    Console.WriteLine("[FileService.UploadEmployeeFile] File type không hợp lệ");
                    return false;
                }

                string originalFileName = Path.GetFileName(sourceFilePath);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string timestampFileName = string.Format("{0}_{1}{2}", timestamp, fileNameWithoutExt, extension);

                string uploadsPath = FileHelper.GetUploadsPath();
                FileHelper.EnsureDirectoryExists(uploadsPath);

                string destinationPath = Path.Combine(uploadsPath, timestampFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                EmployeeFile fileEntity = new EmployeeFile
                {
                    EmployeeID = employeeId,
                    Title = title,
                    FileName = timestampFileName,
                    CreatedBy = uploadedBy
                };

                bool dbSuccess = _employeeFileRepo.Insert(fileEntity);

                if (!dbSuccess)
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    Console.WriteLine("[FileService.UploadEmployeeFile] Lưu database thất bại");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.UploadEmployeeFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool DeleteEmployeeFile(int fileId)
        {
            try
            {
                EmployeeFile file = _employeeFileRepo.GetById(fileId);
                if (file == null)
                {
                    Console.WriteLine("[FileService.DeleteEmployeeFile] File không tồn tại trong database");
                    return false;
                }

                bool dbSuccess = _employeeFileRepo.Delete(fileId);
                if (!dbSuccess)
                {
                    Console.WriteLine("[FileService.DeleteEmployeeFile] Xóa database thất bại");
                    return false;
                }

                string filePath = FileHelper.GetFullPath(file.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.DeleteEmployeeFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool UploadProjectFile(int projectId, string title, string sourceFilePath, int uploadedBy)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine("[FileService.UploadProjectFile] File không tồn tại");
                    return false;
                }

                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".jpg", ".jpeg", ".png", ".zip", ".rar" };
                string extension = Path.GetExtension(sourceFilePath).ToLower();
                if (Array.IndexOf(allowedExtensions, extension) < 0)
                {
                    Console.WriteLine("[FileService.UploadProjectFile] File type không hợp lệ");
                    return false;
                }

                string originalFileName = Path.GetFileName(sourceFilePath);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string timestampFileName = string.Format("{0}_{1}{2}", timestamp, fileNameWithoutExt, extension);

                string uploadsPath = FileHelper.GetUploadsPath();
                FileHelper.EnsureDirectoryExists(uploadsPath);

                string destinationPath = Path.Combine(uploadsPath, timestampFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                ProjectFile fileEntity = new ProjectFile
                {
                    ProjectID = projectId,
                    Title = title,
                    FileName = timestampFileName,
                    CreatedBy = uploadedBy
                };

                bool dbSuccess = _projectFileRepo.Insert(fileEntity);

                if (!dbSuccess)
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    Console.WriteLine("[FileService.UploadProjectFile] Lưu database thất bại");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.UploadProjectFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool DeleteProjectFile(int fileId)
        {
            try
            {
                ProjectFile file = _projectFileRepo.GetById(fileId);
                if (file == null)
                {
                    Console.WriteLine("[FileService.DeleteProjectFile] File không tồn tại trong database");
                    return false;
                }

                bool dbSuccess = _projectFileRepo.Delete(fileId);
                if (!dbSuccess)
                {
                    Console.WriteLine("[FileService.DeleteProjectFile] Xóa database thất bại");
                    return false;
                }

                string filePath = FileHelper.GetFullPath(file.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.DeleteProjectFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool UploadTaskFile(int taskId, string title, string sourceFilePath, int uploadedBy)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine("[FileService.UploadTaskFile] File không tồn tại");
                    return false;
                }

                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".jpg", ".jpeg", ".png", ".txt", ".log" };
                string extension = Path.GetExtension(sourceFilePath).ToLower();
                if (Array.IndexOf(allowedExtensions, extension) < 0)
                {
                    Console.WriteLine("[FileService.UploadTaskFile] File type không hợp lệ");
                    return false;
                }

                string originalFileName = Path.GetFileName(sourceFilePath);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string timestampFileName = string.Format("{0}_{1}{2}", timestamp, fileNameWithoutExt, extension);

                string uploadsPath = FileHelper.GetUploadsPath();
                FileHelper.EnsureDirectoryExists(uploadsPath);

                string destinationPath = Path.Combine(uploadsPath, timestampFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                TaskFile fileEntity = new TaskFile
                {
                    TaskID = taskId,
                    Title = title,
                    FileName = timestampFileName,
                    CreatedBy = uploadedBy
                };

                bool dbSuccess = _taskFileRepo.Insert(fileEntity);

                if (!dbSuccess)
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    Console.WriteLine("[FileService.UploadTaskFile] Lưu database thất bại");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.UploadTaskFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool DeleteTaskFile(int fileId)
        {
            try
            {
                TaskFile file = _taskFileRepo.GetById(fileId);
                if (file == null)
                {
                    Console.WriteLine("[FileService.DeleteTaskFile] File không tồn tại trong database");
                    return false;
                }

                bool dbSuccess = _taskFileRepo.Delete(fileId);
                if (!dbSuccess)
                {
                    Console.WriteLine("[FileService.DeleteTaskFile] Xóa database thất bại");
                    return false;
                }

                string filePath = FileHelper.GetFullPath(file.FileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.DeleteTaskFile] Lỗi: {ex.Message}");
                return false;
            }
        }

        public bool UpdateEmployeeAvatar(int employeeId, string sourceFilePath, int uploadedBy)
        {
            try
            {
                if (!File.Exists(sourceFilePath))
                {
                    Console.WriteLine("[FileService.UpdateEmployeeAvatar] File không tồn tại");
                    return false;
                }

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = Path.GetExtension(sourceFilePath).ToLower();
                if (Array.IndexOf(allowedExtensions, extension) < 0)
                {
                    Console.WriteLine("[FileService.UpdateEmployeeAvatar] Avatar phải là ảnh (.jpg, .png, .gif)");
                    return false;
                }

                string originalFileName = Path.GetFileName(sourceFilePath);
                string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
                string timestampFileName = string.Format("{0}_{1}{2}", timestamp, fileNameWithoutExt, extension);

                string uploadsPath = FileHelper.GetUploadsPath();
                string avatarFolder = Path.Combine(uploadsPath, "Avatars");
                FileHelper.EnsureDirectoryExists(avatarFolder);

                string destinationPath = Path.Combine(avatarFolder, timestampFileName);

                File.Copy(sourceFilePath, destinationPath, true);

                Employee employee = _employeeRepo.GetById(employeeId);
                if (employee == null)
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    Console.WriteLine("[FileService.UpdateEmployeeAvatar] Employee không tồn tại");
                    return false;
                }

                if (!string.IsNullOrEmpty(employee.AvatarPath))
                {
                    string oldAvatarFileName = employee.AvatarPath.Replace("/Uploads/", "").Replace("/", "\\");
                    string oldAvatarPath = Path.Combine(uploadsPath, oldAvatarFileName);
                    if (File.Exists(oldAvatarPath))
                    {
                        File.Delete(oldAvatarPath);
                    }
                }

                employee.AvatarPath = string.Format("/Uploads/Avatars/{0}", timestampFileName);
                bool updateSuccess = _employeeRepo.Update(employee);

                if (!updateSuccess)
                {
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    Console.WriteLine("[FileService.UpdateEmployeeAvatar] Cập nhật database thất bại");
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.UpdateEmployeeAvatar] Lỗi: {ex.Message}");
                return false;
            }
        }

        public string GetFilePath(string fileName)
        {
            try
            {
                return FileHelper.GetFullPath(fileName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.GetFilePath] Lỗi: {ex.Message}");
                return null;
            }
        }

        public bool FileExists(string fileName)
        {
            try
            {
                string fullPath = FileHelper.GetFullPath(fileName);
                return File.Exists(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.FileExists] Lỗi: {ex.Message}");
                return false;
            }
        }

        public long GetFileSize(string fileName)
        {
            try
            {
                string fullPath = FileHelper.GetFullPath(fileName);
                if (File.Exists(fullPath))
                {
                    FileInfo fileInfo = new FileInfo(fullPath);
                    return fileInfo.Length;
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[FileService.GetFileSize] Lỗi: {ex.Message}");
                return 0;
            }
        }

        public string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return string.Format("{0:0.##} {1}", len, sizes[order]);
        }
    }
}