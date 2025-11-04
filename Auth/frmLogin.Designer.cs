namespace EmployeeManagement.Auth
{
    partial class frmLogin
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
            this.panel1 = new AntdUI.Panel();
            this.btnThoat = new AntdUI.Button();
            this.cboxMatKhau = new AntdUI.Checkbox();
            this.labelThongBao = new System.Windows.Forms.Label();
            this.image3D3 = new AntdUI.Image3D();
            this.btnDangNhap = new AntdUI.Button();
            this.label1 = new AntdUI.Label();
            this.txtMatKhau = new AntdUI.Input();
            this.txtTaiKhoan = new AntdUI.Input();
            this.image3D2 = new AntdUI.Image3D();
            this.image3D1 = new AntdUI.Image3D();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnThoat);
            this.panel1.Controls.Add(this.cboxMatKhau);
            this.panel1.Controls.Add(this.labelThongBao);
            this.panel1.Controls.Add(this.image3D3);
            this.panel1.Controls.Add(this.btnDangNhap);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtMatKhau);
            this.panel1.Controls.Add(this.txtTaiKhoan);
            this.panel1.Controls.Add(this.image3D2);
            this.panel1.Controls.Add(this.image3D1);
            this.panel1.Controls.Add(this.pageHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 333);
            this.panel1.TabIndex = 0;
            this.panel1.Text = "panel1";
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this.btnThoat.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.BorderWidth = 2F;
            this.btnThoat.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.btnThoat.ForeHover = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(272, 251);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(104, 35);
            this.btnThoat.TabIndex = 11;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // cboxMatKhau
            // 
            this.cboxMatKhau.BackColor = System.Drawing.Color.Transparent;
            this.cboxMatKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboxMatKhau.Location = new System.Drawing.Point(330, 211);
            this.cboxMatKhau.Name = "cboxMatKhau";
            this.cboxMatKhau.Size = new System.Drawing.Size(103, 23);
            this.cboxMatKhau.TabIndex = 10;
            this.cboxMatKhau.Text = "Hiện mật khẩu";
            this.cboxMatKhau.CheckedChanged += new AntdUI.BoolEventHandler(this.cboxMatKhau_CheckedChanged);
            // 
            // labelThongBao
            // 
            this.labelThongBao.AutoSize = true;
            this.labelThongBao.BackColor = System.Drawing.Color.Transparent;
            this.labelThongBao.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThongBao.ForeColor = System.Drawing.Color.Red;
            this.labelThongBao.Location = new System.Drawing.Point(278, 231);
            this.labelThongBao.Name = "labelThongBao";
            this.labelThongBao.Size = new System.Drawing.Size(0, 17);
            this.labelThongBao.TabIndex = 9;
            // 
            // image3D3
            // 
            this.image3D3.BackColor = System.Drawing.Color.Transparent;
            this.image3D3.Dock = System.Windows.Forms.DockStyle.Left;
            this.image3D3.Image = global::EmployeeManagement.Properties.Resources.back;
            this.image3D3.ImageFit = AntdUI.TFit.Contain;
            this.image3D3.Location = new System.Drawing.Point(0, 34);
            this.image3D3.Name = "image3D3";
            this.image3D3.Size = new System.Drawing.Size(272, 299);
            this.image3D3.TabIndex = 8;
            this.image3D3.Text = "image3D3";
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this.btnDangNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDangNhap.Location = new System.Drawing.Point(382, 251);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(104, 35);
            this.btnDangNhap.TabIndex = 7;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Type = AntdUI.TTypeMini.Primary;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            this.btnDangNhap.MouseHover += new System.EventHandler(this.btnDangNhap_MouseHover);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(298, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(179, 36);
            this.label1.TabIndex = 6;
            this.label1.Text = "ĐĂNG NHẬP";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.txtMatKhau.Location = new System.Drawing.Point(330, 166);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(156, 34);
            this.txtMatKhau.TabIndex = 5;
            this.txtMatKhau.UseSystemPasswordChar = true;
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.Location = new System.Drawing.Point(330, 115);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(156, 34);
            this.txtTaiKhoan.TabIndex = 4;
            // 
            // image3D2
            // 
            this.image3D2.BackColor = System.Drawing.Color.Transparent;
            this.image3D2.Image = global::EmployeeManagement.Properties.Resources.key_border_black;
            this.image3D2.ImageFit = AntdUI.TFit.Contain;
            this.image3D2.Location = new System.Drawing.Point(249, 166);
            this.image3D2.Name = "image3D2";
            this.image3D2.Size = new System.Drawing.Size(75, 23);
            this.image3D2.TabIndex = 3;
            this.image3D2.Text = "image3D2";
            // 
            // image3D1
            // 
            this.image3D1.BackColor = System.Drawing.Color.Transparent;
            this.image3D1.Image = global::EmployeeManagement.Properties.Resources.user_boder_black;
            this.image3D1.ImageFit = AntdUI.TFit.Contain;
            this.image3D1.Location = new System.Drawing.Point(249, 115);
            this.image3D1.Name = "image3D1";
            this.image3D1.Size = new System.Drawing.Size(75, 23);
            this.image3D1.TabIndex = 2;
            this.image3D1.Text = "image3D1";
            // 
            // pageHeader1
            // 
            this.pageHeader1.CancelButton = true;
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Icon = global::EmployeeManagement.Properties.Resources.sign_in_alt;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.MaximizeBox = false;
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.ShowIcon = true;
            this.pageHeader1.Size = new System.Drawing.Size(566, 34);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Đăng nhập";
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 333);
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLogin";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Panel panel1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Button btnDangNhap;
        private AntdUI.Label label1;
        private AntdUI.Input txtMatKhau;
        private AntdUI.Input txtTaiKhoan;
        private AntdUI.Image3D image3D2;
        private AntdUI.Image3D image3D1;
        private AntdUI.Image3D image3D3;
        private System.Windows.Forms.Label labelThongBao;
        private AntdUI.Checkbox cboxMatKhau;
        private AntdUI.Button btnThoat;
    }
}