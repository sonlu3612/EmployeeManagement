namespace EmployeeManagement.Dialogs
{
    partial class frmSubmit
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
            this.txtMaNguoiTao = new AntdUI.Input();
            this.dateEnd = new AntdUI.DatePicker();
            this.label8 = new AntdUI.Label();
            this.label2 = new AntdUI.Label();
            this.btnHuy = new AntdUI.Button();
            this.btnLuu = new AntdUI.Button();
            this.ddownStatus = new AntdUI.Dropdown();
            this.txtDescription = new AntdUI.Input();
            this.txtTaskName = new AntdUI.Input();
            this.ddownProjectID = new AntdUI.Dropdown();
            this.label7 = new AntdUI.Label();
            this.label6 = new AntdUI.Label();
            this.label4 = new AntdUI.Label();
            this.label5 = new AntdUI.Label();
            this.label1 = new AntdUI.Label();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.uploadDragger1 = new AntdUI.UploadDragger();
            this.SuspendLayout();
            // 
            // txtMaNguoiTao
            // 
            this.txtMaNguoiTao.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtMaNguoiTao.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtMaNguoiTao.BorderWidth = 2F;
            this.txtMaNguoiTao.Location = new System.Drawing.Point(223, 291);
            this.txtMaNguoiTao.Name = "txtMaNguoiTao";
            this.txtMaNguoiTao.Size = new System.Drawing.Size(217, 34);
            this.txtMaNguoiTao.TabIndex = 43;
            // 
            // dateEnd
            // 
            this.dateEnd.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dateEnd.BorderWidth = 2F;
            this.dateEnd.Location = new System.Drawing.Point(223, 434);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(217, 33);
            this.dateEnd.TabIndex = 42;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 434);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 27);
            this.label8.TabIndex = 41;
            this.label8.Text = "Ngày hết hạn*";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 385);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 27);
            this.label2.TabIndex = 39;
            this.label2.Text = "Đính kèm";
            // 
            // btnHuy
            // 
            this.btnHuy.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.BorderWidth = 2F;
            this.btnHuy.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.ForeHover = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(296, 499);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 41);
            this.btnHuy.TabIndex = 38;
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
            this.btnLuu.Location = new System.Drawing.Point(387, 499);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 41);
            this.btnLuu.TabIndex = 37;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Type = AntdUI.TTypeMini.Primary;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // ddownStatus
            // 
            this.ddownStatus.BorderWidth = 2F;
            this.ddownStatus.Items.AddRange(new object[] {
            "Cần làm",
            "Đang thực hiện",
            "Hoàn thành"});
            this.ddownStatus.Location = new System.Drawing.Point(223, 340);
            this.ddownStatus.Name = "ddownStatus";
            this.ddownStatus.ShowArrow = true;
            this.ddownStatus.Size = new System.Drawing.Size(217, 32);
            this.ddownStatus.TabIndex = 35;
            this.ddownStatus.Text = "                                    ";
            this.ddownStatus.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownStatus_SelectedValueChanged);
            // 
            // txtDescription
            // 
            this.txtDescription.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderWidth = 2F;
            this.txtDescription.Location = new System.Drawing.Point(223, 183);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(217, 102);
            this.txtDescription.TabIndex = 34;
            // 
            // txtTaskName
            // 
            this.txtTaskName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderWidth = 2F;
            this.txtTaskName.Location = new System.Drawing.Point(223, 125);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(217, 34);
            this.txtTaskName.TabIndex = 33;
            // 
            // ddownProjectID
            // 
            this.ddownProjectID.BorderWidth = 2F;
            this.ddownProjectID.Location = new System.Drawing.Point(223, 66);
            this.ddownProjectID.Name = "ddownProjectID";
            this.ddownProjectID.ShowArrow = true;
            this.ddownProjectID.Size = new System.Drawing.Size(217, 32);
            this.ddownProjectID.TabIndex = 32;
            this.ddownProjectID.Text = "                                    ";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 190);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 27);
            this.label7.TabIndex = 31;
            this.label7.Text = "Mô tả";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 293);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 27);
            this.label6.TabIndex = 30;
            this.label6.Text = "Mã người tạo*";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 340);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 27);
            this.label4.TabIndex = 29;
            this.label4.Text = "Trạng thái";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 27);
            this.label5.TabIndex = 27;
            this.label5.Text = "Tên nhiệm vụ*";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 27);
            this.label1.TabIndex = 26;
            this.label1.Text = "Mã dự án*";
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
            this.pageHeader1.Size = new System.Drawing.Size(551, 36);
            this.pageHeader1.TabIndex = 25;
            this.pageHeader1.Text = "Nhiệm vụ";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // uploadDragger1
            // 
            this.uploadDragger1.Location = new System.Drawing.Point(237, 388);
            this.uploadDragger1.Name = "uploadDragger1";
            this.uploadDragger1.Size = new System.Drawing.Size(75, 23);
            this.uploadDragger1.TabIndex = 44;
            this.uploadDragger1.Text = "uploadDragger1";
            this.uploadDragger1.Click += new System.EventHandler(this.uploadDragger1_Click);
            // 
            // frmSubmit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 582);
            this.Controls.Add(this.uploadDragger1);
            this.Controls.Add(this.txtMaNguoiTao);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.ddownStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.ddownProjectID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSubmit";
            this.Text = "frmSubmit";
            this.Load += new System.EventHandler(this.frmSubmit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input txtMaNguoiTao;
        private AntdUI.DatePicker dateEnd;
        private AntdUI.Label label8;
        private AntdUI.Label label2;
        private AntdUI.Button btnHuy;
        private AntdUI.Button btnLuu;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.Input txtDescription;
        private AntdUI.Input txtTaskName;
        private AntdUI.Dropdown ddownProjectID;
        private AntdUI.Label label7;
        private AntdUI.Label label6;
        private AntdUI.Label label4;
        private AntdUI.Label label5;
        private AntdUI.Label label1;
        private AntdUI.PageHeader pageHeader1;
        private AntdUI.UploadDragger uploadDragger1;
    }
}