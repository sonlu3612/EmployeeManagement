namespace EmployeeManagement.Pages
{
    partial class Page_TaskFile
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
            this.btnSearch = new AntdUI.Button();
            this.panel1 = new AntdUI.Panel();
            this.btnSync = new AntdUI.Button();
            this.btnThem = new AntdUI.Button();
            this.ddSort = new AntdUI.Dropdown();
            this.txtSearch = new AntdUI.Input();
            this.tbFiles = new AntdUI.Table();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
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
            this.btnSearch.Location = new System.Drawing.Point(305, 8);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(51, 35);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnThem);
            this.panel1.Controls.Add(this.ddSort);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(963, 44);
            this.panel1.TabIndex = 2;
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
            this.btnSync.Location = new System.Drawing.Point(353, 8);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(51, 35);
            this.btnSync.TabIndex = 9;
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
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
            this.btnThem.Location = new System.Drawing.Point(889, 0);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(74, 44);
            this.btnThem.TabIndex = 8;
            this.btnThem.Text = "Thêm";
            this.btnThem.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ddSort
            // 
            this.ddSort.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddSort.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.ddSort.ForeColor = System.Drawing.Color.White;
            this.ddSort.Location = new System.Drawing.Point(410, 9);
            this.ddSort.Name = "ddSort";
            this.ddSort.ShowArrow = true;
            this.ddSort.Size = new System.Drawing.Size(159, 35);
            this.ddSort.TabIndex = 2;
            this.ddSort.Text = "Sắp xếp";
            this.ddSort.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddSort_SelectedValueChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(12, 8);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Search...";
            this.txtSearch.Radius = 10;
            this.txtSearch.Size = new System.Drawing.Size(287, 35);
            this.txtSearch.TabIndex = 0;
            // 
            // tbFiles
            // 
            this.tbFiles.ColumnBack = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(138)))), ((int)(((byte)(243)))));
            this.tbFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbFiles.EmptyText = "Không có tài liệu";
            this.tbFiles.Gap = 12;
            this.tbFiles.Location = new System.Drawing.Point(0, 44);
            this.tbFiles.Name = "tbFiles";
            this.tbFiles.Size = new System.Drawing.Size(963, 544);
            this.tbFiles.TabIndex = 3;
            this.tbFiles.CellClick += new AntdUI.Table.ClickEventHandler(this.tbFiles_CellClick);
            this.tbFiles.CellButtonClick += new AntdUI.Table.ClickButtonEventHandler(this.tbFiles_CellButtonClick);
            this.tbFiles.CellDoubleClick += new AntdUI.Table.ClickEventHandler(this.tbFiles_CellDoubleClick);
            this.tbFiles.SetRowStyle += new AntdUI.Table.SetRowStyleEventHandler(this.tbFiles_SetRowStyle);
            // 
            // Page_TaskFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbFiles);
            this.Controls.Add(this.panel1);
            this.Name = "Page_TaskFile";
            this.Size = new System.Drawing.Size(963, 588);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Button btnSearch;
        private AntdUI.Panel panel1;
        private AntdUI.Dropdown ddSort;
        private AntdUI.Input txtSearch;
        private AntdUI.Table tbFiles;
        private AntdUI.Button btnThem;
        private AntdUI.Button btnSync;
    }
}
