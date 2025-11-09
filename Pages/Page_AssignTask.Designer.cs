namespace EmployeeManagement.Pages
{
    partial class Page_AssignTask
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbTask = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.ddownStatus = new AntdUI.Dropdown();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.txtTim = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbTask
            // 
            this.tbTask.ColumnBack = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(138)))), ((int)(((byte)(243)))));
            this.tbTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbTask.EmptyText = "Không có nhiệm vụ nào";
            this.tbTask.Gap = 12;
            this.tbTask.Location = new System.Drawing.Point(0, 41);
            this.tbTask.Name = "tbTask";
            this.tbTask.Size = new System.Drawing.Size(841, 515);
            this.tbTask.TabIndex = 3;
            this.tbTask.Text = "table1";
            this.tbTask.SetRowStyle += new AntdUI.Table.SetRowStyleEventHandler(this.tbTask_SetRowStyle);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.ddownStatus);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtTim);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(841, 41);
            this.panel1.TabIndex = 2;
            this.panel1.Text = "panel1";
            // 
            // ddownStatus
            // 
            this.ddownStatus.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownStatus.ForeColor = System.Drawing.Color.White;
            this.ddownStatus.Location = new System.Drawing.Point(330, 3);
            this.ddownStatus.Name = "ddownStatus";
            this.ddownStatus.ShowArrow = true;
            this.ddownStatus.Size = new System.Drawing.Size(135, 35);
            this.ddownStatus.TabIndex = 3;
            this.ddownStatus.Text = "Trạng thái";
            this.ddownStatus.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownStatus_SelectedValueChanged);
            // 
            // btnSync
            // 
            this.btnSync.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSync.BackHover = System.Drawing.Color.White;
            this.btnSync.BorderWidth = 2F;
            this.btnSync.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSync.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSync.Icon = global::EmployeeManagement.Properties.Resources.rotate_right_white;
            this.btnSync.IconHover = global::EmployeeManagement.Properties.Resources.rotate_right_blue;
            this.btnSync.Location = new System.Drawing.Point(273, 3);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(51, 35);
            this.btnSync.TabIndex = 2;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSearch.BackHover = System.Drawing.Color.White;
            this.btnSearch.BorderWidth = 2F;
            this.btnSearch.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSearch.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnSearch.Icon = global::EmployeeManagement.Properties.Resources.search_white;
            this.btnSearch.IconHover = global::EmployeeManagement.Properties.Resources.search_blue;
            this.btnSearch.Location = new System.Drawing.Point(225, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(51, 35);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtTim
            // 
            this.txtTim.Location = new System.Drawing.Point(3, 3);
            this.txtTim.Name = "txtTim";
            this.txtTim.PlaceholderText = "Tìm kiếm ...";
            this.txtTim.Radius = 10;
            this.txtTim.Size = new System.Drawing.Size(216, 35);
            this.txtTim.TabIndex = 0;
            // 
            // Page_AssignTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbTask);
            this.Controls.Add(this.panel1);
            this.Name = "Page_AssignTask";
            this.Size = new System.Drawing.Size(841, 556);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table tbTask;
        private AntdUI.Panel panel1;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input txtTim;
    }
}
