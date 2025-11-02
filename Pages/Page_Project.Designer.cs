namespace EmployeeManagement.Pages
{
    partial class Page_Project
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
            this.tbProject = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.btnXoa = new AntdUI.Button();
            this.btnThem = new AntdUI.Button();
            this.ddownEmployee = new AntdUI.Dropdown();
            this.ddownStatus = new AntdUI.Dropdown();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.input1 = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbProject
            // 
            this.tbProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbProject.Gap = 12;
            this.tbProject.Location = new System.Drawing.Point(0, 41);
            this.tbProject.Name = "tbProject";
            this.tbProject.Size = new System.Drawing.Size(1023, 617);
            this.tbProject.TabIndex = 3;
            this.tbProject.Text = "table1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnXoa);
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.ddownEmployee);
            this.panel1.Controls.Add(this.ddownStatus);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.input1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1023, 41);
            this.panel1.TabIndex = 2;
            this.panel1.Text = "panel1";
            // 
            // btnXoa
            // 
            this.btnXoa.BackHover = System.Drawing.Color.White;
            this.btnXoa.BorderWidth = 2F;
            this.btnXoa.DefaultBack = System.Drawing.Color.Red;
            this.btnXoa.DefaultBorderColor = System.Drawing.Color.Red;
            this.btnXoa.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeActive = System.Drawing.Color.Red;
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(875, 0);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(74, 41);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackHover = System.Drawing.Color.White;
            this.btnThem.BorderWidth = 2F;
            this.btnThem.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnThem.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnThem.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(949, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(74, 41);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // ddownEmployee
            // 
            this.ddownEmployee.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownEmployee.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownEmployee.ForeColor = System.Drawing.Color.White;
            this.ddownEmployee.Location = new System.Drawing.Point(471, 3);
            this.ddownEmployee.Name = "ddownEmployee";
            this.ddownEmployee.ShowArrow = true;
            this.ddownEmployee.Size = new System.Drawing.Size(135, 35);
            this.ddownEmployee.TabIndex = 4;
            this.ddownEmployee.Text = "Nhân viên";
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
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(3, 3);
            this.input1.Name = "input1";
            this.input1.PlaceholderText = "Tìm kiếm ...";
            this.input1.Radius = 10;
            this.input1.Size = new System.Drawing.Size(216, 35);
            this.input1.TabIndex = 0;
            // 
            // Page_Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbProject);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Project";
            this.Size = new System.Drawing.Size(1023, 658);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table tbProject;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnXoa;
        private AntdUI.Button btnThem;
        private AntdUI.Dropdown ddownEmployee;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input input1;
    }
}
