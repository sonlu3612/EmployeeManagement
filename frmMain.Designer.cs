using System.Windows.Forms;

namespace EmployeeManagement
{
    partial class frmMain
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
            AntdUI.MenuItem menuItem1 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem2 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem3 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem4 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem5 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem6 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem7 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem8 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem9 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem10 = new AntdUI.MenuItem();
            AntdUI.MenuItem menuItem11 = new AntdUI.MenuItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.panel1 = new AntdUI.Panel();
            this.tabs1 = new AntdUI.Tabs();
            this.tpPhongBan = new AntdUI.TabPage();
            this.tabProject = new AntdUI.TabPage();
            this.tabMyProfile = new AntdUI.TabPage();
            this.tabTask = new AntdUI.TabPage();
            this.tabNV = new AntdUI.TabPage();
            this.tabCompany = new AntdUI.TabPage();
            this.tabDatabase = new AntdUI.TabPage();
            this.tabChangePassword = new AntdUI.TabPage();
            this.panel4 = new AntdUI.Panel();
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
            this.phTrangChu = new AntdUI.PageHeader();
            this.panel2 = new AntdUI.Panel();
            this.avatar2 = new AntdUI.Avatar();
            this.label3 = new AntdUI.Label();
            this.menu1 = new AntdUI.Menu();
            this.panel3 = new AntdUI.Panel();
            this.avatar1 = new AntdUI.Avatar();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.divider2 = new AntdUI.Divider();
            this.divider1 = new AntdUI.Divider();
            this.page_Project1 = new EmployeeManagement.Pages.Page_Project();
            this.page_Task1 = new EmployeeManagement.Pages.Page_Task();
            this.page_Employee1 = new EmployeeManagement.Pages.Page_Employee();
            this.page_Account1 = new EmployeeManagement.Pages.Page_Account();
            this.page_Department1 = new EmployeeManagement.Pages.Page_Department();
            this.page_Department2 = new EmployeeManagement.Pages.Page_Department();
            this.panel1.SuspendLayout();
            this.tabs1.SuspendLayout();
            this.tpPhongBan.SuspendLayout();
            this.tabProject.SuspendLayout();
            this.tabMyProfile.SuspendLayout();
            this.tabTask.SuspendLayout();
            this.tabNV.SuspendLayout();
            this.tabChangePassword.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabs1);
            this.panel1.Controls.Add(this.phTrangChu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(254, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1026, 720);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "panel1";
            // 
            // tabs1
            // 
            this.tabs1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabs1.Controls.Add(this.tabMyProfile);
            this.tabs1.Controls.Add(this.tabChangePassword);
            this.tabs1.Controls.Add(this.tabProject);
            this.tabs1.Controls.Add(this.tabTask);
            this.tabs1.Controls.Add(this.tabNV);
            this.tabs1.Controls.Add(this.tabCompany);
            this.tabs1.Controls.Add(this.tabDatabase);
            this.tabs1.Controls.Add(this.tabLogout);
            this.tabs1.Controls.Add(this.tpPhongBan);
            this.tabs1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabs1.Location = new System.Drawing.Point(3, 35);
            this.tabs1.Name = "tabs1";
            this.tabs1.Pages.Add(this.tabProject);
            this.tabs1.Pages.Add(this.tabTask);
            this.tabs1.Pages.Add(this.tabNV);
            this.tabs1.Pages.Add(this.tabCompany);
            this.tabs1.Pages.Add(this.tabDatabase);
            this.tabs1.Pages.Add(this.tabChangePassword);
            this.tabs1.Pages.Add(this.tabLogout);
            this.tabs1.Pages.Add(this.tabMyProfile);
            this.tabs1.Pages.Add(this.tpPhongBan);
            this.tabs1.SelectedIndex = 5;
            this.tabs1.Size = new System.Drawing.Size(1023, 685);
            this.tabs1.Style = styleLine1;
            this.tabs1.TabIndex = 2;
            this.tabs1.Text = "tabs1";
            // 
            // tpPhongBan
            // 
            this.tpPhongBan.Controls.Add(this.page_Department1);
            this.tpPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tpPhongBan.Location = new System.Drawing.Point(0, 0);
            this.tpPhongBan.Name = "tpPhongBan";
            this.tpPhongBan.Size = new System.Drawing.Size(1023, 658);
            this.tpPhongBan.TabIndex = 8;
            this.tpPhongBan.Text = "Phòng ban";
            // 
            // tabProject
            // 
            this.tabProject.Controls.Add(this.page_Project1);
            this.tabProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabProject.Location = new System.Drawing.Point(0, 0);
            this.tabProject.Name = "tabProject";
            this.tabProject.Size = new System.Drawing.Size(1023, 658);
            this.tabProject.TabIndex = 0;
            this.tabProject.Text = "Projects";
            // 
            // tabMyProfile
            // 
            this.tabMyProfile.AutoScroll = true;
            this.tabMyProfile.Controls.Add(this.page_Account1);
            this.tabMyProfile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMyProfile.Location = new System.Drawing.Point(0, 0);
            this.tabMyProfile.Name = "tabMyProfile";
            this.tabMyProfile.Showed = true;
            this.tabMyProfile.Size = new System.Drawing.Size(1023, 658);
            this.tabMyProfile.TabIndex = 6;
            this.tabMyProfile.Text = "Account.MyProfile";
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
            // tabNV
            // 
            this.tabNV.Controls.Add(this.page_Employee1);
            this.tabNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNV.Location = new System.Drawing.Point(0, 0);
            this.tabNV.Name = "tabNV";
            this.tabNV.Size = new System.Drawing.Size(1023, 658);
            this.tabNV.TabIndex = 7;
            this.tabNV.Text = "Nhân viên";
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
            this.tabChangePassword.Controls.Add(this.panel4);
            this.tabChangePassword.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChangePassword.Location = new System.Drawing.Point(0, 0);
            this.tabChangePassword.Name = "tabChangePassword";
            this.tabChangePassword.Showed = true;
            this.tabChangePassword.Size = new System.Drawing.Size(1023, 658);
            this.tabChangePassword.TabIndex = 4;
            this.tabChangePassword.Text = "Account.ChangePassword";
            this.tabChangePassword.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.labelMatKhau);
            this.panel4.Controls.Add(this.txtXNMK);
            this.panel4.Controls.Add(this.txtMKM);
            this.panel4.Controls.Add(this.txtMKC);
            this.panel4.Controls.Add(this.btnThoat);
            this.panel4.Controls.Add(this.btnChangePass);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Location = new System.Drawing.Point(289, 132);
            this.panel4.Name = "panel4";
            this.panel4.Radius = 8;
            this.panel4.Size = new System.Drawing.Size(546, 321);
            this.panel4.TabIndex = 0;
            this.panel4.Text = "panel4";
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
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click_1);
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
            // phTrangChu
            // 
            this.phTrangChu.Dock = System.Windows.Forms.DockStyle.Top;
            this.phTrangChu.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.phTrangChu.IconSvg = "DiffOutlined";
            this.phTrangChu.Location = new System.Drawing.Point(0, 0);
            this.phTrangChu.Name = "phTrangChu";
            this.phTrangChu.ShowButton = true;
            this.phTrangChu.ShowIcon = true;
            this.phTrangChu.Size = new System.Drawing.Size(1026, 38);
            this.phTrangChu.TabIndex = 1;
            this.phTrangChu.Text = "Project";
            this.phTrangChu.Click += new System.EventHandler(this.phTrangChu_Click);
            // 
            // panel2
            // 
            this.panel2.Back = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.avatar2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.menu1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.divider2);
            this.panel2.Controls.Add(this.divider1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Radius = 0;
            this.panel2.Size = new System.Drawing.Size(256, 720);
            this.panel2.TabIndex = 2;
            this.panel2.Text = "panel2";
            // 
            // avatar2
            // 
            this.avatar2.ImageSvg = "RubyOutlined";
            this.avatar2.Location = new System.Drawing.Point(62, 675);
            this.avatar2.Name = "avatar2";
            this.avatar2.Size = new System.Drawing.Size(29, 23);
            this.avatar2.TabIndex = 7;
            this.avatar2.Text = "a";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(97, 675);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "SonLu INC";
            // 
            // menu1
            // 
            this.menu1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.menu1.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(145)))), ((int)(((byte)(159)))), ((int)(((byte)(180)))));
            this.menu1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            menuItem1.BadgeAlign = AntdUI.TAlign.RT;
            menuItem1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem1.IconActiveSvg = "";
            menuItem1.IconSvg = "BarsOutlined";
            menuItem1.Text = "Projects";
            menuItem2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem2.IconSvg = "ScheduleOutlined";
            menuItem2.Text = "Tasks";
            menuItem3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem3.IconSvg = "GroupOutlined";
            menuItem3.Name = "Phòng ban";
            menuItem3.Text = "Phòng ban";
            menuItem4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem4.IconSvg = "UserOutlined";
            menuItem4.Text = "Employees";
            menuItem5.Expand = false;
            menuItem5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem5.IconSvg = "SettingOutlined";
            menuItem6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem6.Text = "    Company";
            menuItem7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem7.Text = "    Database";
            menuItem5.Sub.Add(menuItem6);
            menuItem5.Sub.Add(menuItem7);
            menuItem5.Text = "Setting";
            menuItem8.Expand = false;
            menuItem8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem8.IconSvg = "UserOutlined";
            menuItem9.Text = "My Profile";
            menuItem10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem10.Text = "Change Password";
            menuItem11.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem11.Text = "Log out";
            menuItem8.Sub.Add(menuItem9);
            menuItem8.Sub.Add(menuItem10);
            menuItem8.Sub.Add(menuItem11);
            menuItem8.Text = "Account";
            this.menu1.Items.Add(menuItem1);
            this.menu1.Items.Add(menuItem2);
            this.menu1.Items.Add(menuItem3);
            this.menu1.Items.Add(menuItem4);
            this.menu1.Items.Add(menuItem5);
            this.menu1.Items.Add(menuItem8);
            this.menu1.Location = new System.Drawing.Point(44, 142);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(212, 485);
            this.menu1.TabIndex = 5;
            this.menu1.Text = "menu1";
            this.menu1.SelectChanged += new AntdUI.SelectEventHandler(this.menu1_SelectChanged);
            // 
            // panel3
            // 
            this.panel3.Back = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.panel3.Controls.Add(this.avatar1);
            this.panel3.Location = new System.Drawing.Point(41, 23);
            this.panel3.Name = "panel3";
            this.panel3.Radius = 0;
            this.panel3.Size = new System.Drawing.Size(75, 85);
            this.panel3.TabIndex = 4;
            this.panel3.Text = "panel3";
            // 
            // avatar1
            // 
            this.avatar1.Image = ((System.Drawing.Image)(resources.GetObject("avatar1.Image")));
            this.avatar1.Location = new System.Drawing.Point(3, 6);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(60, 76);
            this.avatar1.TabIndex = 0;
            this.avatar1.Text = "a";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.label2.Location = new System.Drawing.Point(135, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Admin";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(133, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Hornet";
            // 
            // divider2
            // 
            this.divider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.divider2.ColorSplit = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(174)))), ((int)(((byte)(228)))));
            this.divider2.Location = new System.Drawing.Point(12, 633);
            this.divider2.Name = "divider2";
            this.divider2.OrientationMargin = 0F;
            this.divider2.Size = new System.Drawing.Size(236, 23);
            this.divider2.TabIndex = 1;
            this.divider2.Text = "";
            this.divider2.Thickness = 0.9F;
            // 
            // divider1
            // 
            this.divider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.divider1.ColorSplit = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(174)))), ((int)(((byte)(228)))));
            this.divider1.Location = new System.Drawing.Point(12, 129);
            this.divider1.Name = "divider1";
            this.divider1.OrientationMargin = 0F;
            this.divider1.Size = new System.Drawing.Size(236, 23);
            this.divider1.TabIndex = 0;
            this.divider1.Text = "";
            this.divider1.Thickness = 0.9F;
            // 
            // page_Project1
            // 
            this.page_Project1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Project1.Location = new System.Drawing.Point(0, 0);
            this.page_Project1.Name = "page_Project1";
            this.page_Project1.Size = new System.Drawing.Size(1023, 658);
            this.page_Project1.TabIndex = 0;
            // 
            // page_Task1
            // 
            this.page_Task1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Task1.Location = new System.Drawing.Point(0, 0);
            this.page_Task1.Name = "page_Task1";
            this.page_Task1.Size = new System.Drawing.Size(1023, 658);
            this.page_Task1.TabIndex = 0;
            // 
            // page_Employee1
            // 
            this.page_Employee1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Employee1.Location = new System.Drawing.Point(0, 0);
            this.page_Employee1.Name = "page_Employee1";
            this.page_Employee1.Size = new System.Drawing.Size(1023, 658);
            this.page_Employee1.TabIndex = 0;
            // 
            // page_Account1
            // 
            this.page_Account1.AutoScroll = true;
            this.page_Account1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Account1.Location = new System.Drawing.Point(0, 0);
            this.page_Account1.Name = "page_Account1";
            this.page_Account1.Size = new System.Drawing.Size(1023, 658);
            this.page_Account1.TabIndex = 0;
            this.page_Account1.Load += new System.EventHandler(this.page_Account1_Load);
            // 
            // page_Department1
            // 
            this.page_Department1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Department1.Location = new System.Drawing.Point(0, 0);
            this.page_Department1.Name = "page_Department1";
            this.page_Department1.Size = new System.Drawing.Size(1023, 658);
            this.page_Department1.TabIndex = 0;
            // 
            // page_Department2
            // 
            this.page_Department2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_Department2.Location = new System.Drawing.Point(0, 0);
            this.page_Department2.Name = "page_Department2";
            this.page_Department2.Size = new System.Drawing.Size(1023, 658);
            this.page_Department2.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "frmMain";
            this.Text = "frmMain";
            this.panel1.ResumeLayout(false);
            this.tabs1.ResumeLayout(false);
            this.tpPhongBan.ResumeLayout(false);
            this.tabProject.ResumeLayout(false);
            this.tabMyProfile.ResumeLayout(false);
            this.tabTask.ResumeLayout(false);
            this.tabNV.ResumeLayout(false);
            this.tabChangePassword.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Panel panel1;
        private AntdUI.PageHeader phTrangChu;
        private AntdUI.Panel panel2;
        private AntdUI.Avatar avatar1;
        private AntdUI.Divider divider1;
        private AntdUI.Divider divider2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private AntdUI.Panel panel3;
        private AntdUI.Menu menu1;
        private AntdUI.Avatar avatar2;
        private AntdUI.Label label3;
        private AntdUI.Tabs tabs1;
        private AntdUI.TabPage tabProject;
        private AntdUI.TabPage tabTask;
        private AntdUI.TabPage tabDatabase;
        private AntdUI.TabPage tabCompany;
        private AntdUI.TabPage tabLogout;
        private AntdUI.TabPage tabChangePassword;
        private AntdUI.Panel panel4;
        private AntdUI.Label label4;
        private AntdUI.Label label5;
        private AntdUI.Button btnChangePass;
        private AntdUI.Label label7;
        private AntdUI.Label label6;
        private AntdUI.Button btnThoat;
        private AntdUI.Input txtXNMK;
        private AntdUI.Input txtMKM;
        private AntdUI.Input txtMKC;
        private System.Windows.Forms.Label labelMatKhau;
        private AntdUI.TabPage tabMyProfile;
        private Pages.Page_Task page_Task1;
        private AntdUI.TabPage tabNV;
        private Pages.Page_Employee page_Employee1;
        private AntdUI.TabPage tpPhongBan;
        private Pages.Page_Department page_Department2;
        private Pages.Page_Department page_Department1;
        private Pages.Page_Project page_Project1;
        public Pages.Page_Account page_Account1;
    }
}