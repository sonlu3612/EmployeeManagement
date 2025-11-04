namespace EmployeeManagement.Dialogs
{
    partial class frmTask
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
            this.ddownProjectID = new AntdUI.Dropdown();
            this.txtTaskName = new AntdUI.Input();
            this.txtDescription = new AntdUI.Input();
            this.ddownOwnerID = new AntdUI.Dropdown();
            this.ddownStatus = new AntdUI.Dropdown();
            this.dateStart = new AntdUI.DatePicker();
            this.btnLuu = new AntdUI.Button();
            this.btnHuy = new AntdUI.Button();
            this.pageHeader1 = new AntdUI.PageHeader();
            this.ddownPriority = new AntdUI.Dropdown();
            this.label2 = new AntdUI.Label();
            this.label8 = new AntdUI.Label();
            this.dateEnd = new AntdUI.DatePicker();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 27);
            this.label1.TabIndex = 1;
            this.label1.Text = "Mã dự án*";
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 27);
            this.label5.TabIndex = 5;
            this.label5.Text = "Tên nhiệm vụ*";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(40, 389);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 27);
            this.label3.TabIndex = 7;
            this.label3.Text = "Ngày bắt đầu*";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(40, 335);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 27);
            this.label4.TabIndex = 8;
            this.label4.Text = "Trạng thái*";
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 288);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 27);
            this.label6.TabIndex = 9;
            this.label6.Text = "Mã người thực hiện*";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(40, 185);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 27);
            this.label7.TabIndex = 10;
            this.label7.Text = "Mô tả";
            // 
            // ddownProjectID
            // 
            this.ddownProjectID.BorderWidth = 2F;
            this.ddownProjectID.Location = new System.Drawing.Point(223, 61);
            this.ddownProjectID.Name = "ddownProjectID";
            this.ddownProjectID.ShowArrow = true;
            this.ddownProjectID.Size = new System.Drawing.Size(217, 32);
            this.ddownProjectID.TabIndex = 11;
            this.ddownProjectID.Text = "                                    ";
            this.ddownProjectID.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownProjectID_SelectedValueChanged);
            // 
            // txtTaskName
            // 
            this.txtTaskName.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtTaskName.BorderWidth = 2F;
            this.txtTaskName.Location = new System.Drawing.Point(223, 120);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(217, 34);
            this.txtTaskName.TabIndex = 12;
            // 
            // txtDescription
            // 
            this.txtDescription.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderHover = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.txtDescription.BorderWidth = 2F;
            this.txtDescription.Location = new System.Drawing.Point(223, 178);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(217, 102);
            this.txtDescription.TabIndex = 14;
            // 
            // ddownOwnerID
            // 
            this.ddownOwnerID.BorderWidth = 2F;
            this.ddownOwnerID.Location = new System.Drawing.Point(223, 288);
            this.ddownOwnerID.Name = "ddownOwnerID";
            this.ddownOwnerID.ShowArrow = true;
            this.ddownOwnerID.Size = new System.Drawing.Size(217, 32);
            this.ddownOwnerID.TabIndex = 15;
            this.ddownOwnerID.Text = "                                    ";
            this.ddownOwnerID.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownOwnerID_SelectedValueChanged);
            // 
            // ddownStatus
            // 
            this.ddownStatus.BorderWidth = 2F;
            this.ddownStatus.Location = new System.Drawing.Point(223, 335);
            this.ddownStatus.Name = "ddownStatus";
            this.ddownStatus.ShowArrow = true;
            this.ddownStatus.Size = new System.Drawing.Size(217, 32);
            this.ddownStatus.TabIndex = 16;
            this.ddownStatus.Text = "                                    ";
            this.ddownStatus.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownStatus_SelectedValueChanged);
            // 
            // dateStart
            // 
            this.dateStart.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dateStart.BorderWidth = 2F;
            this.dateStart.Location = new System.Drawing.Point(223, 383);
            this.dateStart.Name = "dateStart";
            this.dateStart.Size = new System.Drawing.Size(217, 33);
            this.dateStart.TabIndex = 17;
            // 
            // btnLuu
            // 
            this.btnLuu.BackHover = System.Drawing.Color.White;
            this.btnLuu.BorderWidth = 2F;
            this.btnLuu.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnLuu.Location = new System.Drawing.Point(387, 531);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(85, 41);
            this.btnLuu.TabIndex = 18;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Type = AntdUI.TTypeMini.Primary;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.BackHover = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.BorderWidth = 2F;
            this.btnHuy.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(119)))), ((int)(((byte)(255)))));
            this.btnHuy.ForeHover = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(296, 531);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(85, 41);
            this.btnHuy.TabIndex = 19;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
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
            this.pageHeader1.Size = new System.Drawing.Size(536, 36);
            this.pageHeader1.TabIndex = 0;
            this.pageHeader1.Text = "Nhiệm vụ";
            this.pageHeader1.UseForeColorDrawIcons = true;
            this.pageHeader1.UseSystemStyleColor = true;
            // 
            // ddownPriority
            // 
            this.ddownPriority.BorderWidth = 2F;
            this.ddownPriority.Items.AddRange(new object[] {
            "Medium"});
            this.ddownPriority.Location = new System.Drawing.Point(223, 436);
            this.ddownPriority.Name = "ddownPriority";
            this.ddownPriority.ShowArrow = true;
            this.ddownPriority.Size = new System.Drawing.Size(217, 32);
            this.ddownPriority.TabIndex = 21;
            this.ddownPriority.Text = "                                    ";
            this.ddownPriority.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownPriority_SelectedValueChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(40, 436);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 27);
            this.label2.TabIndex = 20;
            this.label2.Text = "Mức ưu tiên";
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(40, 485);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 27);
            this.label8.TabIndex = 22;
            this.label8.Text = "Ngày hết hạn*";
            // 
            // dateEnd
            // 
            this.dateEnd.BorderActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.dateEnd.BorderWidth = 2F;
            this.dateEnd.Location = new System.Drawing.Point(223, 485);
            this.dateEnd.Name = "dateEnd";
            this.dateEnd.Size = new System.Drawing.Size(217, 33);
            this.dateEnd.TabIndex = 23;
            // 
            // frmTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(536, 585);
            this.Controls.Add(this.dateEnd);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ddownPriority);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.dateStart);
            this.Controls.Add(this.ddownStatus);
            this.Controls.Add(this.ddownOwnerID);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtTaskName);
            this.Controls.Add(this.ddownProjectID);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pageHeader1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTask";
            this.Text = "frmTasks";
            this.Load += new System.EventHandler(this.frmTask_Load);
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
        private AntdUI.Dropdown ddownProjectID;
        private AntdUI.Input txtTaskName;
        private AntdUI.Input txtDescription;
        private AntdUI.Dropdown ddownOwnerID;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.DatePicker dateStart;
        private AntdUI.Button btnLuu;
        private AntdUI.Button btnHuy;
        private AntdUI.Dropdown ddownPriority;
        private AntdUI.Label label2;
        private AntdUI.Label label8;
        private AntdUI.DatePicker dateEnd;
    }
}