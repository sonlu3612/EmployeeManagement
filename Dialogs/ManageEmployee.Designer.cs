namespace EmployeeManagement.Dialogs
{
    partial class ManageEmployee
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
            this.pageEmployees = new AntdUI.PageHeader();
            this.page_ManageEmployees1 = new EmployeeManagement.Pages.Page_ManageEmployees();
            this.SuspendLayout();
            // 
            // pageEmployees
            // 
            this.pageEmployees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(97)))), ((int)(((byte)(190)))));
            this.pageEmployees.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageEmployees.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageEmployees.ForeColor = System.Drawing.Color.White;
            this.pageEmployees.Icon = global::EmployeeManagement.Properties.Resources.menu_burger_white;
            this.pageEmployees.Location = new System.Drawing.Point(0, 0);
            this.pageEmployees.Name = "pageEmployees";
            this.pageEmployees.ShowButton = true;
            this.pageEmployees.ShowIcon = true;
            this.pageEmployees.Size = new System.Drawing.Size(1017, 39);
            this.pageEmployees.TabIndex = 2;
            this.pageEmployees.Text = "Quản lý nhân viên";
            this.pageEmployees.UseForeColorDrawIcons = true;
            this.pageEmployees.UseSystemStyleColor = true;
            // 
            // page_ManageEmployees1
            // 
            this.page_ManageEmployees1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.page_ManageEmployees1.Location = new System.Drawing.Point(0, 39);
            this.page_ManageEmployees1.Name = "page_ManageEmployees1";
            this.page_ManageEmployees1.Size = new System.Drawing.Size(1017, 574);
            this.page_ManageEmployees1.TabIndex = 3;
            // 
            // ManageEmployee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 613);
            this.Controls.Add(this.page_ManageEmployees1);
            this.Controls.Add(this.pageEmployees);
            this.Name = "ManageEmployee";
            this.Text = "ManageEmployee";
            this.Load += new System.EventHandler(this.ManageEmployee_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.PageHeader pageEmployees;
        private Pages.Page_ManageEmployees page_ManageEmployees1;
    }
}