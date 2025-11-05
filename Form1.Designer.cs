namespace EmployeeManagement
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            AntdUI.Tabs.StyleLine styleLine1 = new AntdUI.Tabs.StyleLine();
            this.tabs1 = new AntdUI.Tabs();
            this.tabNhanVien = new AntdUI.TabPage();
            this.page_Employee = new EmployeeManagement.Pages.Page_Employee();
            this.tabProject = new AntdUI.TabPage();
            this.tabTask = new AntdUI.TabPage();
            this.page_Task1 = new EmployeeManagement.Pages.Page_Task();
            this.tabCompany = new AntdUI.TabPage();
            this.tabDatabase = new AntdUI.TabPage();
            this.tabChangePassword = new AntdUI.TabPage();
            this.panel5 = new AntdUI.Panel();
            this.labelMatKhau = new System.Windows.Forms.Label();
            this.txtXNMK = new AntdUI.Input();
            this.txtMKM = new AntdUI.Input();
            this.txtMKC = new AntdUI.Input();
            this.btnThoat = new AntdUI.Button();
            this.btnChangePass = new AntdUI.Button();
            this.label7 = new AntdUI.Label();
            this.label6 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this.tabLogout = new AntdUI.TabPage();
            this.tabMyProfile = new AntdUI.TabPage();
            this.tabPhongBan = new AntdUI.TabPage();
            this.page_Department1 = new EmployeeManagement.Pages.Page_Department();
            this.tabs1.SuspendLayout();
            this.tabNhanVien.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabChangePassword.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPhongBan.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs1
            // 
            this.tabs1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabs1.Controls.Add(this.tabProject);
            this.tabs1.Controls.Add(this.tabTask);
            this.tabs1.Controls.Add(this.tabCompany);
            this.tabs1.Controls.Add(this.tabDatabase);
            this.tabs1.Controls.Add(this.tabChangePassword);
            this.tabs1.Controls.Add(this.tabLogout);
            this.tabs1.Controls.Add(this.tabMyProfile);
            this.tabs1.Controls.Add(this.tabPhongBan);
            this.tabs1.Controls.Add(this.tabNhanVien);
            this.tabs1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabs1.Location = new System.Drawing.Point(-111, -117);
            this.tabs1.Name = "tabs1";
            this.tabs1.Pages.Add(this.tabProject);
            this.tabs1.Pages.Add(this.tabTask);
            this.tabs1.Pages.Add(this.tabCompany);
            this.tabs1.Pages.Add(this.tabDatabase);
            this.tabs1.Pages.Add(this.tabChangePassword);
            this.tabs1.Pages.Add(this.tabLogout);
            this.tabs1.Pages.Add(this.tabMyProfile);
            this.tabs1.Pages.Add(this.tabPhongBan);
            this.tabs1.Pages.Add(this.tabNhanVien);
            this.tabs1.SelectedIndex = 8;
            this.tabs1.Size = new System.Drawing.Size(1023, 685);
            this.tabs1.Style = styleLine1;
            this.tabs1.TabIndex = 4;
            this.tabs1.Text = "tabs1";
            // 
            // tabNhanVien
            // 
            this.tabNhanVien.Controls.Add(this.page_Employee);
            this.tabNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNhanVien.Location = new System.Drawing.Point(0, 0);
            this.tabNhanVien.Name = "tabNhanVien";
            this.tabNhanVien.Showed = true;
            this.tabNhanVien.Size = new System.Drawing.Size(1023, 658);
            this.tabNhanVien.TabIndex = 8;
            this.tabNhanVien.Text = "Nhân Viên";
            // 
            // page_Employee
            // 
            this.page_Employee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Employee.Location = new System.Drawing.Point(0, 0);
            this.page_Employee.Name = "page_Employee";
            this.page_Employee.Size = new System.Drawing.Size(1023, 658);
            this.page_Employee.TabIndex = 0;
            // 
            // tabProject
            // 
            this.tabProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProject.Location = new System.Drawing.Point(0, 0);
            this.tabProject.Name = "tabProject";
            this.tabProject.Size = new System.Drawing.Size(1023, 658);
            this.tabProject.TabIndex = 0;
            this.tabProject.Text = "Projects";
            // 
            // tabTask
            // 
            this.tabTask.Controls.Add(this.page_Task1);
            this.tabTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTask.Location = new System.Drawing.Point(0, 0);
            this.tabTask.Name = "tabTask";
            this.tabTask.Size = new System.Drawing.Size(1023, 658);
            this.tabTask.TabIndex = 1;
            this.tabTask.Text = "Tasks";
            // 
            // page_Task1
            // 
            this.page_Task1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Task1.Location = new System.Drawing.Point(0, 0);
            this.page_Task1.Name = "page_Task1";
            this.page_Task1.Size = new System.Drawing.Size(1023, 658);
            this.page_Task1.TabIndex = 0;
            // 
            // tabCompany
            // 
            this.tabCompany.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCompany.Location = new System.Drawing.Point(0, 0);
            this.tabCompany.Name = "tabCompany";
            this.tabCompany.Size = new System.Drawing.Size(1023, 658);
            this.tabCompany.TabIndex = 2;
            this.tabCompany.Text = "Setting.Company";
            // 
            // tabDatabase
            // 
            this.tabDatabase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDatabase.Location = new System.Drawing.Point(0, 0);
            this.tabDatabase.Name = "tabDatabase";
            this.tabDatabase.Size = new System.Drawing.Size(1023, 658);
            this.tabDatabase.TabIndex = 3;
            this.tabDatabase.Text = "Setting.Database";
            // 
            // tabChangePassword
            // 
            this.tabChangePassword.Controls.Add(this.panel5);
            this.tabChangePassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChangePassword.Location = new System.Drawing.Point(0, 0);
            this.tabChangePassword.Name = "tabChangePassword";
            this.tabChangePassword.Size = new System.Drawing.Size(1023, 658);
            this.tabChangePassword.TabIndex = 4;
            this.tabChangePassword.Text = "Account.ChangePassword";
            this.tabChangePassword.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.labelMatKhau);
            this.panel5.Controls.Add(this.txtXNMK);
            this.panel5.Controls.Add(this.txtMKM);
            this.panel5.Controls.Add(this.txtMKC);
            this.panel5.Controls.Add(this.btnThoat);
            this.panel5.Controls.Add(this.btnChangePass);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(289, 132);
            this.panel5.Name = "panel5";
            this.panel5.Radius = 8;
            this.panel5.Size = new System.Drawing.Size(546, 321);
            this.panel5.TabIndex = 0;
            this.panel5.Text = "panel5";
            // 
            // labelMatKhau
            // 
            this.labelMatKhau.AutoSize = true;
            this.labelMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.labelMatKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMatKhau.ForeColor = System.Drawing.Color.Red;
            this.labelMatKhau.Location = new System.Drawing.Point(38, 248);
            this.labelMatKhau.Name = "labelMatKhau";
            this.labelMatKhau.Size = new System.Drawing.Size(0, 15);
            this.labelMatKhau.TabIndex = 12;
            // 
            // txtXNMK
            // 
            this.txtXNMK.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.txtXNMK.Location = new System.Drawing.Point(227, 206);
            this.txtXNMK.Name = "txtXNMK";
            this.txtXNMK.PlaceholderText = "Xác nhận mật khẩu mới";
            this.txtXNMK.Size = new System.Drawing.Size(179, 34);
            this.txtXNMK.TabIndex = 11;
            // 
            // txtMKM
            // 
            this.txtMKM.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.txtMKM.Location = new System.Drawing.Point(227, 146);
            this.txtMKM.Name = "txtMKM";
            this.txtMKM.PlaceholderText = "Mật khẩu mới";
            this.txtMKM.Size = new System.Drawing.Size(179, 34);
            this.txtMKM.TabIndex = 10;
            // 
            // txtMKC
            // 
            this.txtMKC.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.txtMKC.Location = new System.Drawing.Point(227, 91);
            this.txtMKC.Name = "txtMKC";
            this.txtMKC.PlaceholderText = "Mật khẩu cũ";
            this.txtMKC.Size = new System.Drawing.Size(179, 34);
            this.txtMKC.TabIndex = 9;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThoat.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.BorderWidth = 1F;
            this.btnThoat.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThoat.Location = new System.Drawing.Point(267, 265);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Radius = 8;
            this.btnThoat.Size = new System.Drawing.Size(100, 37);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnChangePass
            // 
            this.btnChangePass.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnChangePass.BorderWidth = 5F;
            this.btnChangePass.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnChangePass.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangePass.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnChangePass.Location = new System.Drawing.Point(373, 265);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Radius = 8;
            this.btnChangePass.Size = new System.Drawing.Size(100, 37);
            this.btnChangePass.TabIndex = 7;
            this.btnChangePass.Text = "Đổi mật khẩu";
            this.btnChangePass.Type = AntdUI.TTypeMini.Primary;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(38, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(169, 35);
            this.label7.TabIndex = 6;
            this.label7.Text = "Xác nhận mật khẩu mới:";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(38, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 35);
            this.label6.TabIndex = 5;
            this.label6.Text = "Mật khẩu mới:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(38, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 35);
            this.label5.TabIndex = 4;
            this.label5.Text = "Mật khẩu cũ:";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(115, 34);
            this.label4.TabIndex = 0;
            this.label4.Text = "Đổi mật khẩu";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabLogout
            // 
            this.tabLogout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabLogout.Location = new System.Drawing.Point(0, 0);
            this.tabLogout.Name = "tabLogout";
            this.tabLogout.Size = new System.Drawing.Size(1023, 658);
            this.tabLogout.TabIndex = 5;
            this.tabLogout.Text = "Account.LogOut";
            // 
            // tabMyProfile
            // 
            this.tabMyProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMyProfile.Location = new System.Drawing.Point(0, 0);
            this.tabMyProfile.Name = "tabMyProfile";
            this.tabMyProfile.Size = new System.Drawing.Size(1023, 658);
            this.tabMyProfile.TabIndex = 6;
            this.tabMyProfile.Text = "Account.MyProfile";
            // 
            // tabPhongBan
            // 
            this.tabPhongBan.Controls.Add(this.page_Department1);
            this.tabPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPhongBan.Location = new System.Drawing.Point(0, 0);
            this.tabPhongBan.Name = "tabPhongBan";
            this.tabPhongBan.Size = new System.Drawing.Size(1023, 658);
            this.tabPhongBan.TabIndex = 7;
            this.tabPhongBan.Text = "Phòng ban";
            // 
            // page_Department1
            // 
            this.page_Department1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Department1.Location = new System.Drawing.Point(0, 0);
            this.page_Department1.Name = "page_Department1";
            this.page_Department1.Size = new System.Drawing.Size(1023, 658);
            this.page_Department1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabs1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.tabs1.ResumeLayout(false);
            this.tabNhanVien.ResumeLayout(false);
            this.tabTask.ResumeLayout(false);
            this.tabChangePassword.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPhongBan.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabProject;
        private AntdUI.TabPage tabTask;
        private Pages.Page_Task page_Task1;
        private AntdUI.TabPage tabCompany;
        private AntdUI.TabPage tabDatabase;
        private AntdUI.TabPage tabChangePassword;
        private AntdUI.Panel panel5;
        private System.Windows.Forms.Label labelMatKhau;
        private AntdUI.Input txtXNMK;
        private AntdUI.Input txtMKM;
        private AntdUI.Input txtMKC;
        private AntdUI.Button btnThoat;
        private AntdUI.Button btnChangePass;
        private AntdUI.Label label7;
        private AntdUI.Label label6;
        private AntdUI.Label label5;
        private AntdUI.Label label4;
        private AntdUI.TabPage tabLogout;
        private AntdUI.TabPage tabMyProfile;
        private AntdUI.TabPage tabPhongBan;
        private Pages.Page_Department page_Department1;
        private AntdUI.TabPage tabNhanVien;
        private Pages.Page_Employee page_Employee;
    }
}