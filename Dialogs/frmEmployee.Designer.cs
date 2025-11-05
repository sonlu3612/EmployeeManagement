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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDiaChi = new AntdUI.Input();
            this.label10 = new AntdUI.Label();
            this.ddownGT = new AntdUI.Dropdown();
            this.label9 = new AntdUI.Label();
            this.txtDienThoai = new AntdUI.Input();
            this.label8 = new AntdUI.Label();
            this.txtEmail = new AntdUI.Input();
            this.label2 = new AntdUI.Label();
            this.btnHuy = new AntdUI.Button();
            this.btnLuu = new AntdUI.Button();
            this.btnXem = new AntdUI.Button();
            this.btnAnh = new AntdUI.Button();
            this.img = new AntdUI.Image3D();
            this.dateStart = new AntdUI.DatePicker();
            this.ddownRole = new AntdUI.Dropdown();
            this.ddownDepartment = new AntdUI.Dropdown();
            this.txtPosition = new AntdUI.Input();
            this.txtName = new AntdUI.Input();
            this.label7 = new AntdUI.Label();
            this.label6 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this.label3 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.pageHeader1.Size = new System.Drawing.Size(587, 36);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Nhân viên";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.txtDiaChi);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.ddownGT);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtDienThoai);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.txtEmail);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.btnLuu);
            this.panel1.Controls.Add(this.btnXem);
            this.panel1.Controls.Add(this.btnAnh);
            this.panel1.Controls.Add(this.img);
            this.panel1.Controls.Add(this.dateStart);
            this.panel1.Controls.Add(this.ddownRole);
            this.panel1.Controls.Add(this.ddownDepartment);
            this.panel1.Controls.Add(this.txtPosition);
            this.panel1.Controls.Add(this.txtName);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(587, 450);
            this.panel1.TabIndex = 1;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDiaChi.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDiaChi.BorderWidth = 2F;
            this.txtDiaChi.Location = new System.Drawing.Point(253, 378);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(217, 34);
            this.txtDiaChi.TabIndex = 52;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(70, 385);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(114, 27);
            this.label10.TabIndex = 51;
            this.label10.Text = "Địa chỉ";
            // 
            // ddownGT
            // 
            this.ddownGT.BorderWidth = 2F;
            this.ddownGT.Location = new System.Drawing.Point(253, 232);
            this.ddownGT.Name = "ddownGT";
            this.ddownGT.ShowArrow = true;
            this.ddownGT.Size = new System.Drawing.Size(217, 32);
            this.ddownGT.TabIndex = 50;
            this.ddownGT.Text = "                                    ";
            this.ddownGT.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownGT_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(70, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 27);
            this.label9.TabIndex = 49;
            this.label9.Text = "Giới tính";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDienThoai.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDienThoai.BorderWidth = 2F;
            this.txtDienThoai.Location = new System.Drawing.Point(253, 478);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Size = new System.Drawing.Size(217, 34);
            this.txtDienThoai.TabIndex = 48;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(70, 485);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 27);
            this.label8.TabIndex = 47;
            this.label8.Text = "Điện thoại";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtEmail.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtEmail.BorderWidth = 2F;
            this.txtEmail.Location = new System.Drawing.Point(253, 428);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(217, 34);
            this.txtEmail.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(70, 435);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 27);
            this.label2.TabIndex = 45;
            this.label2.Text = "Email";
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
            this.btnHuy.Location = new System.Drawing.Point(301, 633);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(76, 41);
            this.btnHuy.TabIndex = 44;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BackHover = System.Drawing.Color.White;
            this.btnLuu.BorderWidth = 2F;
            this.btnLuu.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnLuu.Location = new System.Drawing.Point(396, 633);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 41);
            this.btnLuu.TabIndex = 43;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Type = AntdUI.TTypeMini.Primary;
            this.btnLuu.Click += new System.EventHandler(this.button1_Click);
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
            this.btnXem.Location = new System.Drawing.Point(156, 100);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(54, 29);
            this.btnXem.TabIndex = 42;
            this.btnXem.Text = "Xem";
            // 
            // btnAnh
            // 
            this.btnAnh.BackHover = System.Drawing.Color.White;
            this.btnAnh.BorderWidth = 2F;
            this.btnAnh.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnAnh.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnh.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnAnh.Location = new System.Drawing.Point(156, 65);
            this.btnAnh.Name = "btnAnh";
            this.btnAnh.Size = new System.Drawing.Size(54, 29);
            this.btnAnh.TabIndex = 41;
            this.btnAnh.Text = "Ảnh";
            this.btnAnh.Type = AntdUI.TTypeMini.Primary;
            this.btnAnh.Click += new System.EventHandler(this.btnAnh_Click);
            // 
            // img
            // 
            this.img.ImageFit = AntdUI.TFit.Contain;
            this.img.Location = new System.Drawing.Point(253, 13);
            this.img.Name = "img";
            this.img.Radius = 2;
            this.img.Size = new System.Drawing.Size(144, 142);
            this.img.TabIndex = 40;
            this.img.Text = "image3D1";
            // 
            // dateStart
            // 
            this.dateStart.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dateStart.BorderWidth = 2F;
            this.dateStart.Location = new System.Drawing.Point(253, 578);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(217, 33);
            this.dateStart.TabIndex = 39;
            // 
            // ddownRole
            // 
            this.ddownRole.BorderWidth = 2F;
            this.ddownRole.Location = new System.Drawing.Point(253, 533);
            this.ddownRole.Name = "ddownRole";
            this.ddownRole.ShowArrow = true;
            this.ddownRole.Size = new System.Drawing.Size(217, 32);
            this.ddownRole.TabIndex = 38;
            this.ddownRole.Text = "                                    ";
            this.ddownRole.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownRole_SelectedValueChanged);
            // 
            // ddownDepartment
            // 
            this.ddownDepartment.BorderWidth = 2F;
            this.ddownDepartment.Location = new System.Drawing.Point(253, 332);
            this.ddownDepartment.Name = "ddownDepartment";
            this.ddownDepartment.ShowArrow = true;
            this.ddownDepartment.Size = new System.Drawing.Size(217, 32);
            this.ddownDepartment.TabIndex = 37;
            this.ddownDepartment.Text = "                                    ";
            // 
            // txtPosition
            // 
            this.txtPosition.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtPosition.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtPosition.BorderWidth = 2F;
            this.txtPosition.Location = new System.Drawing.Point(253, 279);
            this.txtPosition.Name = "txtPosition";
            this.txtPosition.Size = new System.Drawing.Size(217, 34);
            this.txtPosition.TabIndex = 36;
            // 
            // txtName
            // 
            this.txtName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtName.BorderWidth = 2F;
            this.txtName.Location = new System.Drawing.Point(253, 183);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(217, 34);
            this.txtName.TabIndex = 35;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(70, 279);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 27);
            this.label7.TabIndex = 34;
            this.label7.Text = "Chức vụ";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(70, 332);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 27);
            this.label6.TabIndex = 33;
            this.label6.Text = "Phòng ban";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(70, 538);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 27);
            this.label4.TabIndex = 32;
            this.label4.Text = "Vai trò";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 584);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 27);
            this.label3.TabIndex = 31;
            this.label3.Text = "Ngày tham gia";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(70, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 27);
            this.label5.TabIndex = 30;
            this.label5.Text = "Họ và tên";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(70, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 27);
            this.label1.TabIndex = 29;
            this.label1.Text = "Ảnh";
            // 
            // frmEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(587, 486);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageHeader1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmEmployee";
            this.Text = "frmTasks";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private System.Windows.Forms.Panel panel1;
        private AntdUI.Dropdown ddownGT;
        private AntdUI.Label label9;
        private AntdUI.Input txtDienThoai;
        private AntdUI.Label label8;
        private AntdUI.Input txtEmail;
        private AntdUI.Label label2;
        private AntdUI.Button btnHuy;
        private AntdUI.Button btnLuu;
        private AntdUI.Button btnXem;
        private AntdUI.Button btnAnh;
        private AntdUI.Image3D img;
        private AntdUI.DatePicker dateStart;
        private AntdUI.Dropdown ddownRole;
        private AntdUI.Dropdown ddownDepartment;
        private AntdUI.Input txtPosition;
        private AntdUI.Input txtName;
        private AntdUI.Label label7;
        private AntdUI.Label label6;
        private AntdUI.Label label4;
        private AntdUI.Label label3;
        private AntdUI.Label label5;
        private AntdUI.Label label1;
        private AntdUI.Input txtDiaChi;
        private AntdUI.Label label10;
    }
}