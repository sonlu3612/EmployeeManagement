namespace EmployeeManagement.Pages
{
    partial class Page_Employee
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
            this.tbNV = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.btnDelete = new AntdUI.Button();
            this.btnAdd = new AntdUI.Button();
            this.ddownGender = new AntdUI.Dropdown();
            this.ddownRole = new AntdUI.Dropdown();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.txtTim = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbNV
            // 
            this.tbNV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbNV.Gap = 12;
            this.tbNV.Location = new System.Drawing.Point(0, 41);
            this.tbNV.Name = "tbNV";
            this.tbNV.Size = new System.Drawing.Size(918, 507);
            this.tbNV.TabIndex = 3;
            this.tbNV.Text = "table1";
            this.tbNV.CellClick += new AntdUI.Table.ClickEventHandler(this.table1_CellClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.ddownGender);
            this.panel1.Controls.Add(this.ddownRole);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtTim);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(918, 41);
            this.panel1.TabIndex = 2;
            this.panel1.Text = "panel1";
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackHover = System.Drawing.Color.White;
            this.btnDelete.BorderWidth = 2F;
            this.btnDelete.DefaultBack = System.Drawing.Color.Red;
            this.btnDelete.DefaultBorderColor = System.Drawing.Color.Red;
            this.btnDelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeActive = System.Drawing.Color.Red;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(770, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 41);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackHover = System.Drawing.Color.White;
            this.btnAdd.BorderWidth = 2F;
            this.btnAdd.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(844, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 41);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ddownGender
            // 
            this.ddownGender.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownGender.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownGender.ForeColor = System.Drawing.Color.White;
            this.ddownGender.Location = new System.Drawing.Point(471, 3);
            this.ddownGender.Name = "ddownGender";
            this.ddownGender.ShowArrow = true;
            this.ddownGender.Size = new System.Drawing.Size(135, 35);
            this.ddownGender.TabIndex = 4;
            this.ddownGender.Text = "Giới tính";
            this.ddownGender.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownGender_SelectedValueChanged);
            // 
            // ddownRole
            // 
            this.ddownRole.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownRole.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownRole.ForeColor = System.Drawing.Color.White;
            this.ddownRole.Location = new System.Drawing.Point(330, 3);
            this.ddownRole.Name = "ddownRole";
            this.ddownRole.ShowArrow = true;
            this.ddownRole.Size = new System.Drawing.Size(135, 35);
            this.ddownRole.TabIndex = 3;
            this.ddownRole.Text = "Quyền";
            this.ddownRole.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownRole_SelectedValueChanged);
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
            this.txtTim.PlaceholderText = "Tìm kiếm theo tên ...";
            this.txtTim.Radius = 10;
            this.txtTim.Size = new System.Drawing.Size(216, 35);
            this.txtTim.TabIndex = 0;
            // 
            // Page_Employee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbNV);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Employee";
            this.Size = new System.Drawing.Size(918, 548);
            this.Load += new System.EventHandler(this.Page_Employee_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table tbNV;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnAdd;
        private AntdUI.Button btnDelete;
        private AntdUI.Dropdown ddownGender;
        private AntdUI.Dropdown ddownRole;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input txtTim;
    }
}
