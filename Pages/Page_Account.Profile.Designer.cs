namespace EmployeeManagement.Pages
{
    partial class Page_Account
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
            this.panel1 = new AntdUI.Panel();
            this.Profile = new AntdUI.Label();
            this.panel2 = new AntdUI.Panel();
            this.avatar1 = new AntdUI.Avatar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DarkGray;
            this.panel1.Controls.Add(this.Profile);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1085, 103);
            this.panel1.TabIndex = 1;
            this.panel1.Text = "panel1";
            // 
            // Profile
            // 
            this.Profile.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Profile.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Profile.Location = new System.Drawing.Point(36, 25);
            this.Profile.Name = "Profile";
            this.Profile.Size = new System.Drawing.Size(136, 52);
            this.Profile.TabIndex = 1;
            this.Profile.Text = "Profile";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.avatar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1085, 548);
            this.panel2.TabIndex = 2;
            this.panel2.Text = "panel2";
            // 
            // avatar1
            // 
            this.avatar1.Location = new System.Drawing.Point(870, 68);
            this.avatar1.Name = "avatar1";
            this.avatar1.Size = new System.Drawing.Size(75, 160);
            this.avatar1.TabIndex = 0;
            this.avatar1.Text = "a";
            // 
            // Page_Account
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Page_Account";
            this.Size = new System.Drawing.Size(1085, 651);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Panel panel1;
        private AntdUI.Label Profile;
        private AntdUI.Panel panel2;
        private AntdUI.Avatar avatar1;
    }
}
