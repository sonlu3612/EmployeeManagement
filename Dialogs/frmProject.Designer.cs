namespace EmployeeManagement.Dialogs
{
    partial class frmProject
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
            AntdUI.HyperlinkLabel.LinkAppearance linkAppearance3 = new AntdUI.HyperlinkLabel.LinkAppearance();
            AntdUI.HyperlinkLabel.LinkAppearance linkAppearance4 = new AntdUI.HyperlinkLabel.LinkAppearance();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dtEndDate = new AntdUI.DatePicker();
            this.label7 = new AntdUI.Label();
            this.hyperlinkLabel1 = new AntdUI.HyperlinkLabel();
            this.cboStatus = new AntdUI.Dropdown();
            this.label1 = new AntdUI.Label();
            this.txtDescription = new AntdUI.Input();
            this.label9 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.btnHuy = new AntdUI.Button();
            this.button1 = new AntdUI.Button();
            this.dtStartDate = new AntdUI.DatePicker();
            this.cbManager = new AntdUI.Dropdown();
            this.txtProjectName = new AntdUI.Input();
            this.label6 = new AntdUI.Label();
            this.label3 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dtEndDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.hyperlinkLabel1);
            this.panel1.Controls.Add(this.cboStatus);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnHuy);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.dtStartDate);
            this.panel1.Controls.Add(this.cbManager);
            this.panel1.Controls.Add(this.txtProjectName);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(591, 468);
            this.panel1.TabIndex = 0;
            // 
            // dtEndDate
            // 
            this.dtEndDate.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dtEndDate.BorderWidth = 2F;
            this.dtEndDate.Location = new System.Drawing.Point(160, 393);
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Size = new System.Drawing.Size(321, 33);
            this.dtEndDate.TabIndex = 77;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(33, 393);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(149, 27);
            this.label7.TabIndex = 76;
            this.label7.Text = "Ngày hoàn thành:";
            // 
            // hyperlinkLabel1
            // 
            this.hyperlinkLabel1.HoverStyle = linkAppearance3;
            this.hyperlinkLabel1.Location = new System.Drawing.Point(188, 321);
            this.hyperlinkLabel1.Name = "hyperlinkLabel1";
            this.hyperlinkLabel1.NormalStyle = linkAppearance4;
            this.hyperlinkLabel1.Size = new System.Drawing.Size(75, 23);
            this.hyperlinkLabel1.TabIndex = 73;
            this.hyperlinkLabel1.Text = "Đính kèm";
            // 
            // cboStatus
            // 
            this.cboStatus.BorderWidth = 2F;
            this.cboStatus.Items.AddRange(new object[] {
            "Đang chờ",
            "Đang thực hiện",
            "Hoàn thành"});
            this.cboStatus.Location = new System.Drawing.Point(160, 283);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.ShowArrow = true;
            this.cboStatus.Size = new System.Drawing.Size(321, 32);
            this.cboStatus.TabIndex = 72;
            this.cboStatus.Text = "                                    ";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(33, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 27);
            this.label1.TabIndex = 71;
            this.label1.Text = "Trạng thái:";
            // 
            // txtDescription
            // 
            this.txtDescription.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderWidth = 2F;
            this.txtDescription.Location = new System.Drawing.Point(160, 69);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(321, 165);
            this.txtDescription.TabIndex = 70;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(33, 69);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(90, 27);
            this.label9.TabIndex = 69;
            this.label9.Text = "Mô tả:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(33, 321);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 27);
            this.label2.TabIndex = 68;
            this.label2.Text = "Tài liệu:";
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
            this.btnHuy.Location = new System.Drawing.Point(299, 432);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(76, 41);
            this.btnHuy.TabIndex = 67;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // button1
            // 
            this.button1.BackHover = System.Drawing.Color.White;
            this.button1.BorderWidth = 2F;
            this.button1.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.button1.Location = new System.Drawing.Point(396, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(85, 41);
            this.button1.TabIndex = 66;
            this.button1.Text = "Lưu";
            this.button1.Type = AntdUI.TTypeMini.Primary;
            this.button1.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // dtStartDate
            // 
            this.dtStartDate.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dtStartDate.BorderWidth = 2F;
            this.dtStartDate.Location = new System.Drawing.Point(160, 354);
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Size = new System.Drawing.Size(321, 33);
            this.dtStartDate.TabIndex = 65;
            // 
            // cbManager
            // 
            this.cbManager.BorderWidth = 2F;
            this.cbManager.Location = new System.Drawing.Point(160, 245);
            this.cbManager.Name = "cbManager";
            this.cbManager.ShowArrow = true;
            this.cbManager.Size = new System.Drawing.Size(321, 32);
            this.cbManager.TabIndex = 64;
            this.cbManager.Text = "                                    ";
            this.cbManager.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.cbManager_SelectedValueChanged);
            // 
            // txtProjectName
            // 
            this.txtProjectName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtProjectName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtProjectName.BorderWidth = 2F;
            this.txtProjectName.Location = new System.Drawing.Point(160, 29);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(321, 34);
            this.txtProjectName.TabIndex = 63;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(33, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 27);
            this.label6.TabIndex = 62;
            this.label6.Text = "Người quản lý:";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(33, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 27);
            this.label3.TabIndex = 61;
            this.label3.Text = "Ngày bắt đầu:";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(33, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 27);
            this.label5.TabIndex = 60;
            this.label5.Text = "Tên dự án:";
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
            this.pageHeader1.Size = new System.Drawing.Size(591, 36);
            this.pageHeader1.TabIndex = 59;
            this.pageHeader1.Text = "Dự án";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // frmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 504);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pageHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmProject";
            this.Text = "frmProject";
            this.Load += new System.EventHandler(this.frmProject_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private AntdUI.DatePicker dtEndDate;
        private AntdUI.Label label7;
        private AntdUI.HyperlinkLabel hyperlinkLabel1;
        private AntdUI.Dropdown cboStatus;
        private AntdUI.Label label1;
        private AntdUI.Input txtDescription;
        private AntdUI.Label label9;
        private AntdUI.Label label2;
        private AntdUI.Button btnHuy;
        private AntdUI.Button button1;
        private AntdUI.DatePicker dtStartDate;
        private AntdUI.Dropdown cbManager;
        private AntdUI.Input txtProjectName;
        private AntdUI.Label label6;
        private AntdUI.Label label3;
        private AntdUI.Label label5;
        private AntdUI.PageHeader pageHeader1;
    }
}