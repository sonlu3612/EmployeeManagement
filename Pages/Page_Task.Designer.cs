namespace EmployeeManagement.Pages
{
    partial class Page_Task
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
            this.panel1 = new AntdUI.Panel();
            this.btnDelete = new AntdUI.Button();
            this.ddownEmployee = new AntdUI.Dropdown();
            this.ddownStatus = new AntdUI.Dropdown();
            this.btnSync = new AntdUI.Button();
            this.btnSearch = new AntdUI.Button();
            this.input1 = new AntdUI.Input();
            this.tableTask = new AntdUI.Table();
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xóaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.btnDelete);
            this.panel1.Controls.Add(this.ddownEmployee);
            this.panel1.Controls.Add(this.ddownStatus);
            this.panel1.Controls.Add(this.btnSync);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.input1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 41);
            this.panel1.TabIndex = 0;
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
            this.btnDelete.Location = new System.Drawing.Point(724, 0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(74, 41);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
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
            this.ddownEmployee.Text = "Người tạo";
            this.ddownEmployee.SelectedValueChanged += new AntdUI.ObjectNEventHandler(this.ddownEmployee_SelectedValueChanged);
            // 
            // ddownStatus
            // 
            this.ddownStatus.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(79)))), ((int)(((byte)(190)))));
            this.ddownStatus.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddownStatus.ForeColor = System.Drawing.Color.White;
            this.ddownStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Cần làm",
            "Đang làm",
            "Đang xem xét",
            "Hoàn thành",
            "Quá hạn"});
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
            this.btnSync.Location = new System.Drawing.Point(273, 4);
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
            // tableTask
            // 
            this.tableTask.AutoSizeColumnsMode = AntdUI.ColumnsMode.Fill;
            this.tableTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableTask.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tableTask.Gap = 12;
            this.tableTask.Location = new System.Drawing.Point(0, 41);
            this.tableTask.Name = "tableTask";
            this.tableTask.Size = new System.Drawing.Size(798, 416);
            this.tableTask.TabIndex = 1;
            this.tableTask.Text = "table1";
            this.tableTask.CellClick += new AntdUI.Table.ClickEventHandler(this.tableTask_CellClick);
            this.tableTask.MouseDown += new System.Windows.Forms.MouseEventHandler(this.menuStrip_MouseDown);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xóaToolStripMenuItem,
            this.quảnLýToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(185, 48);
            this.menuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip_ItemClicked);
            // 
            // xóaToolStripMenuItem
            // 
            this.xóaToolStripMenuItem.Name = "xóaToolStripMenuItem";
            this.xóaToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.xóaToolStripMenuItem.Text = "Danh sách nhân viên";
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.quảnLýToolStripMenuItem.Text = "Cập nhật";
            // 
            // Page_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableTask);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Task";
            this.Size = new System.Drawing.Size(798, 457);
            this.Load += new System.EventHandler(this.Page_Task_Load);
            this.panel1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Panel panel1;
        private AntdUI.Button btnSearch;
        private AntdUI.Input input1;
        private AntdUI.Button btnSync;
        private AntdUI.Button btnDelete;
        private AntdUI.Dropdown ddownEmployee;
        private AntdUI.Dropdown ddownStatus;
        private AntdUI.Table tableTask;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem xóaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
    }
}
