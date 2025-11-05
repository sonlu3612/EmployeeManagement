namespace EmployeeManagement.Dialogs
{
    partial class frmAssignTask
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
            this.pageTasks = new AntdUI.PageHeader();
            this.page_AssignTask1 = new EmployeeManagement.Pages.Page_AssignTask();
            this.SuspendLayout();
            // 
            // pageTasks
            // 
            this.pageTasks.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.pageTasks.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageTasks.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageTasks.ForeColor = System.Drawing.Color.White;
            this.pageTasks.Icon = global::EmployeeManagement.Properties.Resources.menu_burger_white;
            this.pageTasks.Location = new System.Drawing.Point(0, 0);
            this.pageTasks.Name = "pageTasks";
            this.pageTasks.ShowButton = true;
            this.pageTasks.ShowIcon = true;
            this.pageTasks.Size = new System.Drawing.Size(969, 39);
            this.pageTasks.TabIndex = 2;
            this.pageTasks.Text = "Thêm nhiệm vụ";
            this.pageTasks.UseForeColorDrawIcons = true;
            this.pageTasks.UseSystemStyleColor = true;
            // 
            // page_AssignTask1
            // 
            this.page_AssignTask1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_AssignTask1.Location = new System.Drawing.Point(0, 39);
            this.page_AssignTask1.Name = "page_AssignTask1";
            this.page_AssignTask1.Size = new System.Drawing.Size(969, 507);
            this.page_AssignTask1.TabIndex = 3;
            // 
            // frmAssignTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(969, 546);
            this.Controls.Add(this.page_AssignTask1);
            this.Controls.Add(this.pageTasks);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAssignTask";
            this.Text = "frmAssignTask";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageTasks;
        private Pages.Page_AssignTask page_AssignTask1;
    }
}