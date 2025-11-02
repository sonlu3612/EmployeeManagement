using System;
using System.IO;

namespace EmployeeManagement.DAL.Helpers
{
    /// <summary>
    /// Lớp trợ giúp cho việc xử lý tệp tin
    /// </summary>
    public static class FileHelper
    {
        private static readonly string BasePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Uploads"
        );

        public static string GetUploadsPath()
            => BasePath;

        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GenerateTimestampFileName(string originalFileName)
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string extension = Path.GetExtension(originalFileName);
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(originalFileName);
            return string.Format("{0}_{1}{2}", timestamp, fileNameWithoutExt, extension);
        }

        public static string GetFullPath(string fileName)
            => Path.Combine(BasePath, fileName);

        public static long GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }

        //public static string GetMimeType(string extension)
        //{
        //    switch (extension.ToLower())
        //    {
        //        case ".jpg":
        //        case ".jpeg":
        //            return "image/jpeg";

        //        case ".png":
        //            return "image/png";

        //        case ".gif":
        //            return "image/gif";

        //        case ".bmp":
        //            return "image/bmp";

        //        case ".pdf":
        //            return "application/pdf";

        //        case ".doc":
        //            return "application/msword";

        //        case ".docx":
        //            return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        //        case ".xls":
        //            return "application/vnd.ms-excel";

        //        case ".xlsx":
        //            return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //        case ".ppt":
        //            return "application/vnd.ms-powerpoint";

        //        case ".pptx":
        //            return "application/vnd.openxmlformats-officedocument.presentationml.presentation";

        //        case ".txt":
        //            return "text/plain";

        //        case ".zip":
        //            return "application/zip";

        //        case ".rar":
        //            return "application/x-rar-compressed";

        //        default:
        //            return "application/octet-stream";
        //    }
        //}

        public static bool IsImageFile(string extension)
        {
            string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            return Array.IndexOf(imageExtensions, extension.ToLower()) >= 0;
        }

        public static bool IsDocumentFile(string extension)
        {
            string[] docExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".txt" };
            return Array.IndexOf(docExtensions, extension.ToLower()) >= 0;
        }

        //public static string FormatFileSize(long bytes)
        //{
        //    string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        //    double len = bytes;
        //    int order = 0;
        //    while (len >= 1024 && order < sizes.Length - 1)
        //    {
        //        order++;
        //        len = len / 1024;
        //    }
        //    return string.Format("{0:0.##} {1}", len, sizes[order]);
        //}

        //public static void InitializeUploadFolders()
        //{
        //    EnsureDirectoryExists(BasePath);
        //}

        public static bool DeleteFile(string fileName)
        {
            try
            {
                string fullPath = GetFullPath(fileName);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[FileHelper.DeleteFile] Lỗi: {0}", ex.Message));
                return false;
            }
        }

        public static bool CopyFile(string sourcePath, string destinationFileName)
        {
            try
            {
                EnsureDirectoryExists(BasePath);
                string destinationPath = GetFullPath(destinationFileName);

                File.Copy(sourcePath, destinationPath, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("[FileHelper.CopyFile] Lỗi: {0}", ex.Message));
                return false;
            }
        }

        public static bool FileExists(string fileName)
        {
            string fullPath = GetFullPath(fileName);
            return File.Exists(fullPath);
        }

        public static string GetRelativePath(string fileName)
            => string.Format("/Uploads/{0}", fileName);
    }
}