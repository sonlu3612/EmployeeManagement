namespace EmployeeManagement.Dialogs
{
    partial class frmEmployee
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
            this.label1 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.label3 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this.label6 = new AntdUI.Label();
            this.label7 = new AntdUI.Label();
            this.txtTaskName = new AntdUI.Input();
            this.txtPosition = new AntdUI.Input();
            this.ddownDepartment = new AntdUI.Dropdown();
            this.ddownRole = new AntdUI.Dropdown();
            this.dateStart = new AntdUI.DatePicker();
            this.image = new AntdUI.Image3D();
            this.btnAnh = new AntdUI.Button();
            this.btnXem = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.btnHuy = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.label2 = new AntdUI.Label();
            this.txtEmail = new AntdUI.Input();
            this.label8 = new AntdUI.Label();
            this.txtDienThoai = new AntdUI.Input();
            this.label9 = new AntdUI.Label();
            this.txtGioiTinh = new AntdUI.Input();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ảnh";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 243);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 27);
            this.label5.TabIndex = 5;
            this.label5.Text = "Họ và tên";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 566);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ngày vào";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 527);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "Vai trò";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 399);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 27);
            this.label6.TabIndex = 9;
            this.label6.Text = "Phòng ban";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 27);
            this.label7.TabIndex = 10;
            this.label7.Text = "Chức vụ";
            // 
            // txtTaskName
            // 
            this.txtTaskName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderWidth = 2F;
            this.txtTaskName.Location = new System.Drawing.Point(223, 236);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(217, 34);
            this.txtTaskName.TabIndex = 12;
            // 
            // txtPosition
            // 
            this.txtPosition.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtPosition.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtPosition.BorderWidth = 2F;
            this.txtPosition.Location = new System.Drawing.Point(223, 341);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(217, 34);
            this.txtPosition.TabIndex = 14;
            // 
            // ddownDepartment
            // 
            this.ddownDepartment.BorderWidth = 2F;
            this.ddownDepartment.Location = new System.Drawing.Point(223, 394);
            this.ddownDepartment.Name = "ddownDepartment";
            this.ddownDepartment.ShowArrow = true;
            this.ddownDepartment.Size = new System.Drawing.Size(217, 32);
            this.ddownDepartment.TabIndex = 15;
            this.ddownDepartment.Text = "                                    ";
            // 
            // ddownRole
            // 
            this.ddownRole.BorderWidth = 2F;
            this.ddownRole.Location = new System.Drawing.Point(223, 522);
            this.ddownRole.Name = "ddownRole";
            this.ddownRole.ShowArrow = true;
            this.ddownRole.Size = new System.Drawing.Size(217, 32);
            this.ddownRole.TabIndex = 16;
            this.ddownRole.Text = "                                    ";
            // 
            // dateStart
            // 
            this.dateStart.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dateStart.BorderWidth = 2F;
            this.dateStart.Location = new System.Drawing.Point(223, 560);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(217, 33);
            this.dateStart.TabIndex = 17;
            // 
            // image
            // 
            this.image.ImageFit = AntdUI.TFit.Contain;
            this.image.Location = new System.Drawing.Point(223, 66);
            this.image.Name = "image";
            this.image.Radius = 2;
            this.image.Size = new System.Drawing.Size(144, 142);
            this.image.TabIndex = 18;
            this.image.Text = "image3D1";
            // 
            // btnAnh
            // 
            this.btnAnh.BackHover = System.Drawing.Color.White;
            this.btnAnh.BorderWidth = 2F;
            this.btnAnh.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnAnh.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnh.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnAnh.Location = new System.Drawing.Point(126, 118);
            this.btnAnh.Name = "btnAnh";
            this.btnAnh.Size = new System.Drawing.Size(54, 29);
            this.btnAnh.TabIndex = 19;
            this.btnAnh.Text = "Ảnh";
            this.btnAnh.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnXem
            // 
            this.btnXem.BackColor = System.Drawing.Color.White;
            this.btnXem.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnXem.BorderWidth = 2F;
            this.btnXem.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnXem.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(225)))));
            this.btnXem.ForeHover = System.Drawing.Color.White;
            this.btnXem.Location = new System.Drawing.Point(126, 153);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(54, 29);
            this.btnXem.TabIndex = 20;
            this.btnXem.Text = "Xem";
            // 
            // button1
            // 
            this.button1.BackHover = System.Drawing.Color.White;
            this.button1.BorderWidth = 2F;
            this.button1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(384, 603);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 41);
            this.button1.TabIndex = 21;
            this.button1.Text = "Lưu";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            // 
            // btnHuy
            // 
            this.btnHuy.BackColor = System.Drawing.Color.White;
            this.btnHuy.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.BorderWidth = 2F;
            this.btnHuy.DefaultBack = System.Drawing.Color.White;
            this.btnHuy.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(225)))));
            this.btnHuy.ForeHover = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(293, 603);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(76, 41);
            this.btnHuy.TabIndex = 22;
            this.btnHuy.Text = "Hủy";
            // 
            // pageHeader1
            // 
            this.pageHeader1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.pageHeader1.CancelButton = true;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageHeader1.ForeColor = System.Drawing.Color.White;
            this.pageHeader1.Icon = global::EmployeeManagement.Properties.Resources.menu_burger_white;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.ShowIcon = true;
            this.pageHeader1.Size = new System.Drawing.Size(519, 36);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Nhân viên";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 441);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 27);
            this.label2.TabIndex = 23;
            this.label2.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtEmail.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtEmail.BorderWidth = 2F;
            this.txtEmail.Location = new System.Drawing.Point(223, 432);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(217, 34);
            this.txtEmail.TabIndex = 24;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 489);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 27);
            this.label8.TabIndex = 25;
            this.label8.Text = "Điện thoại";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDienThoai.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDienThoai.BorderWidth = 2F;
            this.txtDienThoai.Location = new System.Drawing.Point(223, 482);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(217, 34);
            this.txtDienThoai.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(40, 295);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 27);
            this.label9.TabIndex = 27;
            this.label9.Text = "Giới tính";
            // 
            // txtGioiTinh
            // 
            this.txtGioiTinh.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtGioiTinh.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtGioiTinh.BorderWidth = 2F;
            this.txtGioiTinh.Location = new System.Drawing.Point(223, 288);
            this.txtGioiTinh.Name = "txtGioiTinh";
            this.txtGioiTinh.Size = new System.Drawing.Size(217, 34);
            this.txtGioiTinh.TabIndex = 28;
            // 
            // frmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(536, 486);
            this.Controls.Add(this.txtGioiTinh);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtDienThoai);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnXem);
            this.Controls.Add(this.btnAnh);
            this.Controls.Add(this.image);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.ddownRole);
            this.Controls.Add(this.ddownDepartment);
            this.Controls.Add(this.txtPosition);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEmployee";
            this.Text = "frmTasks";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Label label1;
        private AntdUI.Label label5;
        private AntdUI.Label label3;
        private AntdUI.Label label4;
        private AntdUI.Label label6;
        private AntdUI.Label label7;
        private AntdUI.Input txtTaskName;
        private AntdUI.Input txtPosition;
        private AntdUI.Dropdown ddownDepartment;
        private AntdUI.Dropdown ddownRole;
        private AntdUI.DatePicker dateStart;
        private AntdUI.Image3D image;
        private AntdUI.Button btnAnh;
        private AntdUI.Button btnXem;
        private AntdUI.Button button1;
        private AntdUI.Button btnHuy;
        private AntdUI.Label label2;
        private AntdUI.Input txtEmail;
        private AntdUI.Label label8;
        private AntdUI.Input txtDienThoai;
        private AntdUI.Label label9;
        private AntdUI.Input txtGioiTinh;
    }
}