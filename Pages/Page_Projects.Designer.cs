namespace EmployeeManagement.Pages
{
    partial class Page_Projects
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
            this.input1 = new AntdUI.Input();
            this.button1 = new AntdUI.Button();
            this.button2 = new AntdUI.Button();
            this.select1 = new AntdUI.Select();
            this.select2 = new AntdUI.Select();
            this.button3 = new AntdUI.Button();
            this.button4 = new AntdUI.Button();
            this.table1 = new AntdUI.Table();
            this.SuspendLayout();
            // 
            // input1
            // 
            this.input1.BorderWidth = 0F;
            this.input1.Location = new System.Drawing.Point(12, 12);
            this.input1.Name = "input1";
            this.input1.Radius = 0;
            this.input1.Size = new System.Drawing.Size(193, 38);
            this.input1.TabIndex = 0;
            this.input1.Text = "Search...";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button1.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.IconSvg = "SearchOutlined";
            this.button1.Location = new System.Drawing.Point(211, 12);
            this.button1.Name = "button1";
            this.button1.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button1.Size = new System.Drawing.Size(54, 38);
            this.button1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(113)))), ((int)(((byte)(218)))));
            this.button2.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.IconSvg = "ReloadOutlined";
            this.button2.Location = new System.Drawing.Point(271, 12);
            this.button2.Name = "button2";
            this.button2.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button2.Size = new System.Drawing.Size(54, 38);
            this.button2.TabIndex = 2;
            // 
            // select1
            // 
            this.select1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select1.BorderWidth = 0F;
            this.select1.Location = new System.Drawing.Point(328, 12);
            this.select1.Margin = new System.Windows.Forms.Padding(0);
            this.select1.Name = "select1";
            this.select1.Radius = 0;
            this.select1.Size = new System.Drawing.Size(145, 38);
            this.select1.TabIndex = 3;
            this.select1.Text = "Status (All)";
            // 
            // select2
            // 
            this.select2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.select2.BorderWidth = 0F;
            this.select2.Location = new System.Drawing.Point(473, 12);
            this.select2.Margin = new System.Windows.Forms.Padding(0);
            this.select2.Name = "select2";
            this.select2.Radius = 0;
            this.select2.Size = new System.Drawing.Size(138, 38);
            this.select2.TabIndex = 4;
            this.select2.Text = "Employees (All)";
            // 
            // button3
            // 
            this.button3.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.button3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(765, 24);
            this.button3.Name = "button3";
            this.button3.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(209)))), ((int)(((byte)(31)))), ((int)(((byte)(31)))));
            this.button3.Radius = 0;
            this.button3.Size = new System.Drawing.Size(108, 26);
            this.button3.TabIndex = 5;
            this.button3.Text = "DELETE";
            // 
            // button4
            // 
            this.button4.DefaultBack = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(909, 24);
            this.button4.Name = "button4";
            this.button4.OriginalBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(137)))), ((int)(((byte)(228)))));
            this.button4.Radius = 0;
            this.button4.Size = new System.Drawing.Size(111, 26);
            this.button4.TabIndex = 6;
            this.button4.Text = "ADD";
            // 
            // table1
            // 
            this.table1.Gap = 12;
            this.table1.Location = new System.Drawing.Point(12, 108);
            this.table1.Name = "table1";
            this.table1.Size = new System.Drawing.Size(1008, 135);
            this.table1.TabIndex = 7;
            this.table1.Text = "table1";
            this.table1.CellClick += new AntdUI.Table.ClickEventHandler(this.table1_CellClick);
            // 
            // Page_Projects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 658);
            this.Controls.Add(this.table1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.select2);
            this.Controls.Add(this.select1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.input1);
            this.Name = "Page_Projects";
            this.Text = "Page_Projects";
            this.Load += new System.EventHandler(this.Page_Projects_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Input input1;
        private AntdUI.Button button1;
        private AntdUI.Button button2;
        private AntdUI.Select select1;
        private AntdUI.Select select2;
        private AntdUI.Button button3;
        private AntdUI.Button button4;
        private AntdUI.Table table1;
    }
}