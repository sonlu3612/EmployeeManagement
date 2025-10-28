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
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel2 = new AntdUI.Panel();
            this.image3D1 = new AntdUI.Image3D();
            this.image3D2 = new AntdUI.Image3D();
            this.txtTaiKhoan = new AntdUI.Input();
            this.input1 = new AntdUI.Input();
            this.label1 = new AntdUI.Label();
            this.btnDangNhap = new AntdUI.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDangNhap);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.input1);
            this.panel1.Controls.Add(this.txtTaiKhoan);
            this.panel1.Controls.Add(this.image3D2);
            this.panel1.Controls.Add(this.image3D1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pageHeader1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 333);
            this.panel1.TabIndex = 0;
            this.panel1.Text = "panel1";
            // 
            // pageHeader1
            // 
            this.pageHeader1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader1.Icon = global::EmployeeManagement.Properties.Resources.sign_in_alt;
            this.pageHeader1.Location = new System.Drawing.Point(0, 0);
            this.pageHeader1.Name = "pageHeader1";
            this.pageHeader1.ShowButton = true;
            this.pageHeader1.ShowIcon = true;
            this.pageHeader1.Size = new System.Drawing.Size(566, 34);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Đăng nhập";
            // 
            // panel2
            // 
            this.panel2.Back = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(124)))), ((int)(((byte)(186)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(205, 299);
            this.panel2.TabIndex = 1;
            this.panel2.Text = "panel2";
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
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.txtTaiKhoan.Location = new System.Drawing.Point(330, 115);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(156, 34);
            this.txtTaiKhoan.TabIndex = 4;
            // 
            // input1
            // 
            this.input1.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.input1.Location = new System.Drawing.Point(330, 166);
            this.input1.Name = "input1";
            this.input1.Size = new System.Drawing.Size(156, 34);
            this.input1.TabIndex = 5;
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
            this.btnDangNhap.MouseHover += new System.EventHandler(this.btnDangNhap_MouseHover);
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
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Panel panel1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Panel panel2;
        private AntdUI.Button btnDangNhap;
        private AntdUI.Label label1;
        private AntdUI.Input input1;
        private AntdUI.Input txtTaiKhoan;
        private AntdUI.Image3D image3D2;
        private AntdUI.Image3D image3D1;
    }
}