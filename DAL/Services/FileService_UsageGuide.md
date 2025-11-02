# FileService Usage Guide

## Overview
FileService là layer trung gian giữa UI (WinForms) và DAL, xử lý toàn bộ business logic liên quan đến files:
- Upload files (EmployeeFiles, ProjectFiles, TaskFiles)
- Delete files (cả database và file vật lý)
- Update avatar (Employees.AvatarPath)

---

## 1. Upload Employee File (CV, Certificate)

```csharp
using EmployeeManagement.DAL.Services;
using System.Windows.Forms;

private void btnUploadEmployeeFile_Click(object sender, EventArgs e)
{
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Title = "Chọn file để upload";
        openFileDialog.Filter = "Documents|*.pdf;*.doc;*.docx;*.xls;*.xlsx|Images|*.jpg;*.jpeg;*.png|All Files|*.*";

        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                FileService fileService = new FileService();
     
                bool success = fileService.UploadEmployeeFile(
                    employeeId: currentEmployeeId,  // ID của employee
                    title: txtFileTitle.Text,             // Tiêu đề file
                    sourceFilePath: openFileDialog.FileName, // Đường dẫn file gốc
                    uploadedBy: currentUserId       // ID người upload
                );

                if (success)
                {
                    MessageBox.Show("Upload file thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadEmployeeFiles(); // Refresh danh sách
                }
                else
                {
                    MessageBox.Show("Upload file thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
```

---

## 2. Delete Employee File

```csharp
private void btnDeleteEmployeeFile_Click(object sender, EventArgs e)
{
    if (dataGridViewFiles.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng chọn file để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
    }

    DialogResult result = MessageBox.Show(
        "Bạn có chắc chắn muốn xóa file này?",
        "Xác nhận xóa",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

    if (result == DialogResult.Yes)
    {
        try
        {
            int fileId = Convert.ToInt32(dataGridViewFiles.SelectedRows[0].Cells["EmployeeFileID"].Value);
      
            FileService fileService = new FileService();
            bool success = fileService.DeleteEmployeeFile(fileId);

            if (success)
            {
                MessageBox.Show("Xóa file thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadEmployeeFiles();
            }
            else
            {
                MessageBox.Show("Xóa file thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
```

---

## 3. Upload Project File

```csharp
private void btnUploadProjectFile_Click(object sender, EventArgs e)
{
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Title = "Chọn file dự án";
        openFileDialog.Filter = "All Files|*.pdf;*.doc;*.docx;*.xls;*.xlsx;*.ppt;*.pptx;*.jpg;*.png;*.zip;*.rar";
    
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
        try
        {
            FileService fileService = new FileService();
            bool success = fileService.UploadProjectFile(
                projectId: currentProjectId,
                title: txtProjectFileTitle.Text,
                sourceFilePath: openFileDialog.FileName,
                uploadedBy: currentUserId
            );

            if (success)
            {
                MessageBox.Show("Upload file dự án thành công!");
                LoadProjectFiles();
            }
            else
            {
                MessageBox.Show("Upload file dự án thất bại!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
```

---

## 4. Upload Task File

```csharp
private void btnUploadTaskFile_Click(object sender, EventArgs e)
{
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Title = "Chọn file task";
        openFileDialog.Filter = "Documents|*.pdf;*.doc;*.docx;*.xls;*.xlsx|Images|*.jpg;*.png|Text|*.txt;*.log|All Files|*.*";
        
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                FileService fileService = new FileService();
                
                bool success = fileService.UploadTaskFile(
                    taskId: currentTaskId,
                    title: txtTaskFileTitle.Text,
                    sourceFilePath: openFileDialog.FileName,
                    uploadedBy: currentUserId
                );

                if (success)
                {
                    MessageBox.Show("Upload file task thành công!");
                    LoadTaskFiles();
                }
                else
                {
                    MessageBox.Show("Upload file task thất bại!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
```

---

## 5. Update Employee Avatar

```csharp
private void btnUpdateAvatar_Click(object sender, EventArgs e)
{
    using (OpenFileDialog openFileDialog = new OpenFileDialog())
    {
        openFileDialog.Title = "Chọn ảnh đại diện";
        openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif";
        
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
            try
            {
                FileService fileService = new FileService();
 
                bool success = fileService.UpdateEmployeeAvatar(
                    employeeId: currentEmployeeId,
                    sourceFilePath: openFileDialog.FileName,
                    uploadedBy: currentUserId
                );

                if (success)
                {
                    MessageBox.Show("Cập nhật avatar thành công!");
                    LoadEmployeeAvatar(); // Refresh avatar
                }
                else
                {
                    MessageBox.Show("Cập nhật avatar thất bại!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
```

---

## 6. Display Avatar

```csharp
private void LoadEmployeeAvatar(int employeeId)
{
    try
    {
        EmployeeRepository empRepo = new EmployeeRepository();
        Employee employee = empRepo.GetById(employeeId);
        
        if (employee != null && !string.IsNullOrEmpty(employee.AvatarPath))
        {
            FileService fileService = new FileService();
    
            // Convert "/Uploads/Avatars/file.jpg" -> "Avatars\file.jpg"
            string fileName = employee.AvatarPath.Replace("/Uploads/", "").Replace("/", "\\");
            string fullPath = fileService.GetFilePath(fileName);
      
            if (fileService.FileExists(fileName))
            {
                pictureBoxAvatar.Image = Image.FromFile(fullPath);
                pictureBoxAvatar.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                // Set default avatar
                pictureBoxAvatar.Image = Properties.Resources.DefaultAvatar;
            }
        }
        else
        {
            // No avatar
            pictureBoxAvatar.Image = Properties.Resources.DefaultAvatar;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"LoadEmployeeAvatar error: {ex.Message}");
        pictureBoxAvatar.Image = Properties.Resources.DefaultAvatar;
    }
}
```

---

## 7. Load Files List

```csharp
private void LoadEmployeeFiles()
{
    try
    {
        EmployeeFileRepository fileRepo = new EmployeeFileRepository();
        List<EmployeeFile> files = fileRepo.GetByEmployee(currentEmployeeId);
     
        dataGridViewFiles.Rows.Clear();
        
        FileService fileService = new FileService();
        
        foreach (EmployeeFile file in files)
        {
            long fileSize = fileService.GetFileSize(file.FileName);
            string fileSizeFormatted = fileService.FormatFileSize(fileSize);
       
            dataGridViewFiles.Rows.Add(
                file.EmployeeFileID,
                file.Title,
                file.FileName,
                fileSizeFormatted,
                file.CreatedByName,
                file.CreatedAt.ToString("dd/MM/yyyy HH:mm")
            );
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi load files: " + ex.Message);
    }
}
```

---

## 8. Open/Download File

```csharp
private void btnOpenFile_Click(object sender, EventArgs e)
{
    if (dataGridViewFiles.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng chọn file để mở!");
        return;
    }

    try
    {
        string fileName = dataGridViewFiles.SelectedRows[0].Cells["FileName"].Value.ToString();
        
        FileService fileService = new FileService();
        string fullPath = fileService.GetFilePath(fileName);
        
        if (fileService.FileExists(fileName))
        {
            // Mở file bằng ứng dụng mặc định
            System.Diagnostics.Process.Start(fullPath);
        }
        else
        {
            MessageBox.Show("File không tồn tại!");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show("Lỗi mở file: " + ex.Message);
    }
}
```

---

## File Type Restrictions

### EmployeeFiles
- `.pdf`, `.doc`, `.docx` - Documents
- `.xls`, `.xlsx` - Spreadsheets
- `.jpg`, `.jpeg`, `.png` - Images

### ProjectFiles
- `.pdf`, `.doc`, `.docx` - Documents
- `.xls`, `.xlsx` - Spreadsheets
- `.ppt`, `.pptx` - Presentations
- `.jpg`, `.jpeg`, `.png` - Images
- `.zip`, `.rar` - Archives

### TaskFiles
- `.pdf`, `.doc`, `.docx` - Documents
- `.xls`, `.xlsx` - Spreadsheets
- `.jpg`, `.jpeg`, `.png` - Screenshots
- `.txt`, `.log` - Text files

### Avatar
- `.jpg`, `.jpeg`, `.png`, `.gif` - Images only

---

## Storage Structure

```
C:\Users\Hung\source\EmployeeManagement\bin\Debug\Uploads\
├── Avatars\                  (Employee avatars)
│   ├── 20250601120000_avatar_nva.jpg
│   ├── 20250601120100_avatar_ttb.png
│   └── ...
│
├── 20250601130000_cv_employee.pdf             (EmployeeFiles)
├── 20250601140000_requirements.pdf     (ProjectFiles)
├── 20250601150000_test_results.xlsx     (TaskFiles)
└── ...
```

---

## Error Handling

FileService tự động xử lý:
- ✅ File validation (type, existence)
- ✅ Database transaction-like behavior
- ✅ Rollback nếu copy file thành công nhưng save DB fail
- ✅ Delete old avatar khi upload avatar mới
- ✅ Log errors ra Console

---

## Best Practices

1. **Always check return value:**
   ```csharp
   bool success = fileService.UploadEmployeeFile(...);
   if (!success) { /* Handle error */ }
   ```

2. **Show confirmation before delete:**
   ```csharp
   DialogResult result = MessageBox.Show("Xác nhận xóa?", ..., MessageBoxButtons.YesNo);
   ```

3. **Use try-catch in UI layer:**
   ```csharp
   try {
    // FileService calls
   } catch (Exception ex) {
       MessageBox.Show("Lỗi: " + ex.Message);
   }
   ```

4. **Refresh UI after operations:**
   ```csharp
   if (success) {
       LoadFiles(); // Refresh danh sách
   }
   ```
