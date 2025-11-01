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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel1 = new AntdUI.Panel();
            this.tabs1 = new AntdUI.Tabs();
            this.tabProject = new AntdUI.TabPage();
            this.tabTask = new AntdUI.TabPage();
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
            this.tabMyProfile = new AntdUI.TabPage();
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
            this.tabNhanVien = new AntdUI.TabPage();
            this.tblNhanVien = new AntdUI.Table();
            this.btnThem = new AntdUI.Button();
            this.btnXoa = new AntdUI.Button();
            this.select2 = new AntdUI.Select();
            this.select1 = new AntdUI.Select();
            this.button2 = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.input1 = new AntdUI.Input();
            this.tabPhongBan = new AntdUI.TabPage();
            this.tblPhongBan = new AntdUI.Table();
            this.button3 = new AntdUI.Button();
            this.button4 = new AntdUI.Button();
            this.select3 = new AntdUI.Select();
            this.select4 = new AntdUI.Select();
            this.button5 = new AntdUI.Button();
            this.button6 = new AntdUI.Button();
            this.input2 = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.tabs1.SuspendLayout();
            this.tabChangePassword.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabNhanVien.SuspendLayout();
            this.tabPhongBan.SuspendLayout();
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
            this.tabs1.Controls.Add(this.tabPhongBan);
            this.tabs1.Controls.Add(this.tabNhanVien);
            this.tabs1.Controls.Add(this.tabProject);
            this.tabs1.Controls.Add(this.tabTask);
            this.tabs1.Controls.Add(this.tabCompany);
            this.tabs1.Controls.Add(this.tabDatabase);
            this.tabs1.Controls.Add(this.tabChangePassword);
            this.tabs1.Controls.Add(this.tabLogout);
            this.tabs1.Controls.Add(this.tabMyProfile);
            this.tabs1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tabs1.Location = new System.Drawing.Point(3, 35);
            this.tabs1.Name = "tabs1";
            this.tabs1.Pages.Add(this.tabProject);
            this.tabs1.Pages.Add(this.tabTask);
            this.tabs1.Pages.Add(this.tabCompany);
            this.tabs1.Pages.Add(this.tabDatabase);
            this.tabs1.Pages.Add(this.tabChangePassword);
            this.tabs1.Pages.Add(this.tabLogout);
            this.tabs1.Pages.Add(this.tabMyProfile);
            this.tabs1.Pages.Add(this.tabNhanVien);
            this.tabs1.Pages.Add(this.tabPhongBan);
            this.tabs1.SelectedIndex = 8;
            this.tabs1.Size = new System.Drawing.Size(1023, 685);
            this.tabs1.Style = styleLine1;
            this.tabs1.TabIndex = 2;
            this.tabs1.Text = "tabs1";
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
            this.tabTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTask.Location = new System.Drawing.Point(0, 0);
            this.tabTask.Name = "tabTask";
            this.tabTask.Size = new System.Drawing.Size(1023, 658);
            this.tabTask.TabIndex = 1;
            this.tabTask.Text = "Tasks";
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
            menuItem3.IconSvg = "UserOutlined";
            menuItem3.Text = "Employees";
            menuItem4.Expand = false;
            menuItem4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem4.IconSvg = "SettingOutlined";
            menuItem5.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem5.Text = "    Company";
            menuItem6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem6.Text = "    Database";
            menuItem4.Sub.Add(menuItem5);
            menuItem4.Sub.Add(menuItem6);
            menuItem4.Text = "Setting";
            menuItem7.Expand = false;
            menuItem7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem7.IconSvg = "UserOutlined";
            menuItem8.Text = "My Profile";
            menuItem9.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem9.Text = "Change Password";
            menuItem10.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem10.Text = "Log out";
            menuItem7.Sub.Add(menuItem8);
            menuItem7.Sub.Add(menuItem9);
            menuItem7.Sub.Add(menuItem10);
            menuItem7.Text = "Account";
            menuItem11.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            menuItem11.IconSvg = "TeamOutlined";
            menuItem11.Text = "Phòng ban";
            this.menu1.Items.Add(menuItem1);
            this.menu1.Items.Add(menuItem2);
            this.menu1.Items.Add(menuItem3);
            this.menu1.Items.Add(menuItem4);
            this.menu1.Items.Add(menuItem7);
            this.menu1.Items.Add(menuItem11);
            this.menu1.Location = new System.Drawing.Point(44, 142);
            this.menu1.Name = "menu1";
            this.menu1.Size = new System.Drawing.Size(212, 485);
            this.menu1.TabIndex = 5;
            this.menu1.Text = "menu1";
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
            // tabNhanVien
            // 
            this.tabNhanVien.Controls.Add(this.tblNhanVien);
            this.tabNhanVien.Controls.Add(this.btnThem);
            this.tabNhanVien.Controls.Add(this.btnXoa);
            this.tabNhanVien.Controls.Add(this.select2);
            this.tabNhanVien.Controls.Add(this.select1);
            this.tabNhanVien.Controls.Add(this.button2);
            this.tabNhanVien.Controls.Add(this.button1);
            this.tabNhanVien.Controls.Add(this.input1);
            this.tabNhanVien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabNhanVien.Location = new System.Drawing.Point(0, 0);
            this.tabNhanVien.Name = "tabNhanVien";
            this.tabNhanVien.Size = new System.Drawing.Size(1023, 658);
            this.tabNhanVien.TabIndex = 7;
            this.tabNhanVien.Text = "Nhân viên";
            // 
            // tblNhanVien
            // 
            this.tblNhanVien.Gap = 12;
            this.tblNhanVien.Location = new System.Drawing.Point(7, 75);
            this.tblNhanVien.Name = "tblNhanVien";
            this.tblNhanVien.Size = new System.Drawing.Size(1008, 527);
            this.tblNhanVien.TabIndex = 15;
            this.tblNhanVien.Text = "table1";
            // 
            // btnThem
            // 
            this.btnThem.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Location = new System.Drawing.Point(904, 18);
            this.btnThem.Name = "btnThem";
            this.btnThem.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.btnThem.Radius = 0;
            this.btnThem.Size = new System.Drawing.Size(111, 26);
            this.btnThem.TabIndex = 14;
            this.btnThem.Text = "Thêm";
            // 
            // btnXoa
            // 
            this.btnXoa.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Location = new System.Drawing.Point(760, 18);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.btnXoa.Radius = 0;
            this.btnXoa.Size = new System.Drawing.Size(108, 26);
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            // 
            // select2
            // 
            this.select2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select2.BorderWidth = 0F;
            this.select2.Location = new System.Drawing.Point(468, 6);
            this.select2.Margin = new System.Windows.Forms.Padding(0);
            this.select2.Name = "select2";
            this.select2.Radius = 0;
            this.select2.Size = new System.Drawing.Size(138, 38);
            this.select2.TabIndex = 12;
            this.select2.Text = "Employees (All)";
            // 
            // select1
            // 
            this.select1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select1.BorderWidth = 0F;
            this.select1.Location = new System.Drawing.Point(323, 6);
            this.select1.Margin = new System.Windows.Forms.Padding(0);
            this.select1.Name = "select1";
            this.select1.Radius = 0;
            this.select1.Size = new System.Drawing.Size(145, 38);
            this.select1.TabIndex = 11;
            this.select1.Text = "Status (All)";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button2.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.IconSvg = "ReloadOutlined";
            this.button2.Location = new System.Drawing.Point(266, 6);
            this.button2.Name = "button2";
            this.button2.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button2.Size = new System.Drawing.Size(54, 38);
            this.button2.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button1.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.IconSvg = "SearchOutlined";
            this.button1.Location = new System.Drawing.Point(206, 6);
            this.button1.Name = "button1";
            this.button1.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button1.Size = new System.Drawing.Size(54, 38);
            this.button1.TabIndex = 9;
            // 
            // input1
            // 
            this.input1.BorderWidth = 0F;
            this.input1.Location = new System.Drawing.Point(7, 6);
            this.input1.Name = "input1";
            this.input1.Radius = 0;
            this.input1.Size = new System.Drawing.Size(193, 38);
            this.input1.TabIndex = 8;
            this.input1.Text = "Search...";
            // 
            // tabPhongBan
            // 
            this.tabPhongBan.Controls.Add(this.tblPhongBan);
            this.tabPhongBan.Controls.Add(this.button3);
            this.tabPhongBan.Controls.Add(this.button4);
            this.tabPhongBan.Controls.Add(this.select3);
            this.tabPhongBan.Controls.Add(this.select4);
            this.tabPhongBan.Controls.Add(this.button5);
            this.tabPhongBan.Controls.Add(this.button6);
            this.tabPhongBan.Controls.Add(this.input2);
            this.tabPhongBan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPhongBan.Location = new System.Drawing.Point(0, 0);
            this.tabPhongBan.Name = "tabPhongBan";
            this.tabPhongBan.Showed = true;
            this.tabPhongBan.Size = new System.Drawing.Size(1023, 658);
            this.tabPhongBan.TabIndex = 8;
            this.tabPhongBan.Text = "Phòng ban";
            // 
            // tblPhongBan
            // 
            this.tblPhongBan.Gap = 12;
            this.tblPhongBan.Location = new System.Drawing.Point(7, 75);
            this.tblPhongBan.Name = "tblPhongBan";
            this.tblPhongBan.Size = new System.Drawing.Size(1008, 527);
            this.tblPhongBan.TabIndex = 23;
            this.tblPhongBan.Text = "table1";
            // 
            // button3
            // 
            this.button3.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(904, 18);
            this.button3.Name = "button3";
            this.button3.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button3.Radius = 0;
            this.button3.Size = new System.Drawing.Size(111, 26);
            this.button3.TabIndex = 22;
            this.button3.Text = "Thêm";
            // 
            // button4
            // 
            this.button4.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.button4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(760, 18);
            this.button4.Name = "button4";
            this.button4.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.button4.Radius = 0;
            this.button4.Size = new System.Drawing.Size(108, 26);
            this.button4.TabIndex = 21;
            this.button4.Text = "Xóa";
            // 
            // select3
            // 
            this.select3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select3.BorderWidth = 0F;
            this.select3.Location = new System.Drawing.Point(468, 6);
            this.select3.Margin = new System.Windows.Forms.Padding(0);
            this.select3.Name = "select3";
            this.select3.Radius = 0;
            this.select3.Size = new System.Drawing.Size(138, 38);
            this.select3.TabIndex = 20;
            this.select3.Text = "Employees (All)";
            // 
            // select4
            // 
            this.select4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select4.BorderWidth = 0F;
            this.select4.Location = new System.Drawing.Point(323, 6);
            this.select4.Margin = new System.Windows.Forms.Padding(0);
            this.select4.Name = "select4";
            this.select4.Radius = 0;
            this.select4.Size = new System.Drawing.Size(145, 38);
            this.select4.TabIndex = 19;
            this.select4.Text = "Status (All)";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button5.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.IconSvg = "ReloadOutlined";
            this.button5.Location = new System.Drawing.Point(266, 6);
            this.button5.Name = "button5";
            this.button5.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button5.Size = new System.Drawing.Size(54, 38);
            this.button5.TabIndex = 18;
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button6.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.IconSvg = "SearchOutlined";
            this.button6.Location = new System.Drawing.Point(206, 6);
            this.button6.Name = "button6";
            this.button6.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button6.Size = new System.Drawing.Size(54, 38);
            this.button6.TabIndex = 17;
            // 
            // input2
            // 
            this.input2.BorderWidth = 0F;
            this.input2.Location = new System.Drawing.Point(7, 6);
            this.input2.Name = "input2";
            this.input2.Radius = 0;
            this.input2.Size = new System.Drawing.Size(193, 38);
            this.input2.TabIndex = 16;
            this.input2.Text = "Search...";
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
            this.tabChangePassword.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tabNhanVien.ResumeLayout(false);
            this.tabPhongBan.ResumeLayout(false);
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
        private AntdUI.TabPage tabNhanVien;
        private AntdUI.Table tblNhanVien;
        private AntdUI.Button btnThem;
        private AntdUI.Button btnXoa;
        private AntdUI.Select select2;
        private AntdUI.Select select1;
        private AntdUI.Button button2;
        private AntdUI.Button button1;
        private AntdUI.Input input1;
        private AntdUI.TabPage tabPhongBan;
        private AntdUI.Table tblPhongBan;
        private AntdUI.Button button3;
        private AntdUI.Button button4;
        private AntdUI.Select select3;
        private AntdUI.Select select4;
        private AntdUI.Button button5;
        private AntdUI.Button button6;
        private AntdUI.Input input2;
    }
}