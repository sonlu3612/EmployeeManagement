namespace EmployeeManagement.Pages
{
    partial class Page_AssignEmployee
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
            this.tbEmployee = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.ddownSelect = new AntdUI.Dropdown();
            this.ddownPB = new AntdUI.Dropdown();
            this.btnLuu = new AntdUI.Button();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.txtTim = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbEmployee
            // 
            this.tbEmployee.ColumnBack = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(138)))), ((int)(((byte)(243)))));
            this.tbEmployee.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbEmployee.EmptyText = "Không có nhân viên nào";
            this.tbEmployee.Gap = 12;
            this.tbEmployee.Location = new System.Drawing.Point(0, 41);
            this.tbEmployee.Name = "tbEmployee";
            this.tbEmployee.Size = new System.Drawing.Size(841, 515);
            this.tbEmployee.TabIndex = 3;
            this.tbEmployee.Text = "table1";
            this.tbEmployee.SetRowStyle += new AntdUI.Table.SetRowStyleEventHandler(this.tbEmployee_SetRowStyle);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.ddownSelect);
            this.panel1.Controls.Add(this.ddownPB);
            this.panel1.Controls.Add(this.btnLuu);
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
            // ddownSelect
            // 
            this.ddownSelect.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownSelect.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownSelect.ForeColor = System.Drawing.Color.White;
            this.ddownSelect.Location = new System.Drawing.Point(462, 3);
            this.ddownSelect.Name = "ddownSelect";
            this.ddownSelect.ShowArrow = true;
            this.ddownSelect.Size = new System.Drawing.Size(135, 35);
            this.ddownSelect.TabIndex = 8;
            this.ddownSelect.Text = "Trạng thái";
            this.ddownSelect.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownSelect_SelectedValueChanged);
            // 
            // ddownPB
            // 
            this.ddownPB.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownPB.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownPB.ForeColor = System.Drawing.Color.White;
            this.ddownPB.Location = new System.Drawing.Point(330, 3);
            this.ddownPB.Name = "ddownPB";
            this.ddownPB.ShowArrow = true;
            this.ddownPB.Size = new System.Drawing.Size(135, 35);
            this.ddownPB.TabIndex = 7;
            this.ddownPB.Text = "Phòng ban";
            this.ddownPB.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownPB_SelectedValueChanged);
            // 
            // btnLuu
            // 
            this.btnLuu.BackHover = System.Drawing.Color.White;
            this.btnLuu.BorderWidth = 2F;
            this.btnLuu.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnLuu.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnLuu.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuu.ForeActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(767, 0);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(74, 41);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnAdd_Click);
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
            // Page_AssignEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbEmployee);
            this.Controls.Add(this.panel1);
            this.Name = "Page_AssignEmployee";
            this.Size = new System.Drawing.Size(841, 556);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table tbEmployee;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnLuu;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input txtTim;
        private AntdUI.Dropdown ddownPB;
        private AntdUI.Dropdown ddownSelect;
    }
}
