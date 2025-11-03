namespace EmployeeManagement.Pages
{
    partial class Page_Department
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
            this.table1 = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.btnAdd = new AntdUI.Button();
            this.btnDelete = new AntdUI.Button();
            this.ddownEmployee = new AntdUI.Dropdown();
            this.ddownStatus = new AntdUI.Dropdown();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.input1 = new AntdUI.Input();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // table1
            // 
            this.table1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.table1.Gap = 12;
            this.table1.Location = new System.Drawing.Point(0, 41);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(769, 454);
            this.table1.TabIndex = 5;
            this.table1.Text = "table1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.ddownEmployee);
            this.panel1.Controls.Add(this.ddownStatus);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.input1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(769, 41);
            this.panel1.TabIndex = 4;
            this.panel1.Text = "panel1";
            // 
            // btnAdd
            // 
            this.btnAdd.BackHover = System.Drawing.Color.White;
            this.btnAdd.BorderWidth = 2F;
            this.btnAdd.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.DefaultBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeActive = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(692, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 35);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            // 
            // btnDelete
            // 
            this.btnDelete.BackHover = System.Drawing.Color.White;
            this.btnDelete.BorderWidth = 2F;
            this.btnDelete.DefaultBack = System.Drawing.Color.Red;
            this.btnDelete.DefaultBorderColor = System.Drawing.Color.Red;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeActive = System.Drawing.Color.Red;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(612, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Xóa";
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
            // Page_Department
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.table1);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Department";
            this.Size = new System.Drawing.Size(769, 495);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table table1;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnAdd;
        private AntdUI.Button btnDelete;
        private AntdUI.Dropdown ddownEmployee;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input input1;
    }
}
