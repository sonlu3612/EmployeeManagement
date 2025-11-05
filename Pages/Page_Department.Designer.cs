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
            this.components = new System.ComponentModel.Container();
            this.tbPB = new AntdUI.Table();
            this.panel1 = new AntdUI.Panel();
            this.btnDelete = new AntdUI.Button();
            this.btnAdd = new AntdUI.Button();
            this.ddownTP = new AntdUI.Dropdown();
            this.txtTim = new AntdUI.Input();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.ctm1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chỉnhSửaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.ctm1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbPB
            // 
            this.tbPB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbPB.Gap = 12;
            this.tbPB.Location = new System.Drawing.Point(0, 41);
            this.tbPB.Name = "tbPB";
            this.tbPB.Size = new System.Drawing.Size(913, 501);
            this.tbPB.TabIndex = 5;
            this.tbPB.Text = "table1";
            this.tbPB.CellClick += new AntdUI.Table.ClickEventHandler(this.tbPB_CellClick);
            this.tbPB.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbPB_MouseDown);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.btnAdd);
            this.panel1.Controls.Add(this.ddownTP);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.txtTim);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(913, 41);
            this.panel1.TabIndex = 4;
            this.panel1.Text = "panel1";
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
            this.btnDelete.Location = new System.Drawing.Point(765, 0);
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
            this.btnAdd.Location = new System.Drawing.Point(839, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(74, 41);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // ddownTP
            // 
            this.ddownTP.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownTP.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownTP.ForeColor = System.Drawing.Color.White;
            this.ddownTP.Location = new System.Drawing.Point(330, 3);
            this.ddownTP.Name = "ddownTP";
            this.ddownTP.ShowArrow = true;
            this.ddownTP.Size = new System.Drawing.Size(135, 35);
            this.ddownTP.TabIndex = 4;
            this.ddownTP.Text = "Trưởng phòng";
            this.ddownTP.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownEmployee_SelectedValueChanged);
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
            // ctm1
            // 
            this.ctm1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quảnLýToolStripMenuItem,
            this.chỉnhSửaToolStripMenuItem});
            this.ctm1.Name = "ctm1";
            this.ctm1.Size = new System.Drawing.Size(181, 70);
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            this.quảnLýToolStripMenuItem.Click += new System.EventHandler(this.taskToolStripMenuItem_Click);
            // 
            // chỉnhSửaToolStripMenuItem
            // 
            this.chỉnhSửaToolStripMenuItem.Name = "chỉnhSửaToolStripMenuItem";
            this.chỉnhSửaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.chỉnhSửaToolStripMenuItem.Text = "Chỉnh sửa";
            // 
            // Page_Department
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbPB);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Department";
            this.Size = new System.Drawing.Size(913, 542);
            this.Load += new System.EventHandler(this.Page_Department_Load);
            this.panel1.ResumeLayout(false);
            this.ctm1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Table tbPB;
        private AntdUI.Panel panel1;
        private AntdUI.Button btnAdd;
        private AntdUI.Button btnDelete;
        private AntdUI.Dropdown ddownTP;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnSearch;
        private AntdUI.Input txtTim;
        private System.Windows.Forms.ContextMenuStrip ctm1;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem chỉnhSửaToolStripMenuItem;
    }
}
