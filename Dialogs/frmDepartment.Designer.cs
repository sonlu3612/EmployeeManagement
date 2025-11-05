namespace EmployeeManagement.Dialogs
{
    partial class frmDepartment
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
            this.txtDescription = new AntdUI.Input();
            this.label9 = new AntdUI.Label();
            this.btnHuy = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.cbTP = new AntdUI.Dropdown();
            this.txtTenPB = new AntdUI.Input();
            this.label6 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDescription
            // 
            this.txtDescription.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderWidth = 2F;
            this.txtDescription.Location = new System.Drawing.Point(153, 109);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(321, 165);
            this.txtDescription.TabIndex = 70;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 27);
            this.label9.TabIndex = 69;
            this.label9.Text = "Mô tả:";
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
            this.btnHuy.Location = new System.Drawing.Point(258, 344);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(76, 41);
            this.btnHuy.TabIndex = 67;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // button1
            // 
            this.button1.BackHover = System.Drawing.Color.White;
            this.button1.BorderWidth = 2F;
            this.button1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(366, 344);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 41);
            this.button1.TabIndex = 66;
            this.button1.Text = "Lưu";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbTP
            // 
            this.cbTP.BorderWidth = 2F;
            this.cbTP.Location = new System.Drawing.Point(153, 306);
            this.cbTP.Name = "cbTP";
            this.cbTP.ShowArrow = true;
            this.cbTP.Size = new System.Drawing.Size(321, 32);
            this.cbTP.TabIndex = 64;
            this.cbTP.Text = "                                    ";
            this.cbTP.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.cbTP_SelectedValueChanged);
            // 
            // txtTenPB
            // 
            this.txtTenPB.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTenPB.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTenPB.BorderWidth = 2F;
            this.txtTenPB.Location = new System.Drawing.Point(153, 56);
            this.txtTenPB.Name = "txtTenPB";
            this.txtTenPB.Size = new System.Drawing.Size(321, 34);
            this.txtTenPB.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 27);
            this.label6.TabIndex = 62;
            this.label6.Text = "Trưởng phòng:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 27);
            this.label5.TabIndex = 60;
            this.label5.Text = "Tên phòng ban:";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.cbTP);
            this.panel1.Controls.Add(this.txtTenPB);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 40);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(500, 415);
            this.panel1.TabIndex = 60;
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
            this.pageHeader1.Size = new System.Drawing.Size(500, 40);
            this.pageHeader1.TabIndex = 61;
            this.pageHeader1.Text = "Phòng ban";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // frmDepartment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 455);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDepartment";
            this.Text = "frmDepartment";
            this.Load += new System.EventHandler(this.frmDepartment_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageHeader1;
        private AntdUI.Input txtDescription;
        private AntdUI.Label label9;
        private AntdUI.Button btnHuy;
        private AntdUI.Button button1;
        private AntdUI.Dropdown cbTP;
        private AntdUI.Input txtTenPB;
        private AntdUI.Label label6;
        private AntdUI.Label label5;
        private System.Windows.Forms.Panel panel1;
    }
}