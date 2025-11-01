using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement.Dialogs
{
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
            // Nút Lưu (xanh, chữ trắng)
            btnLuu.BackColor = Color.DodgerBlue;
            btnLuu.ForeColor = Color.White;
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.FlatAppearance.BorderSize = 0;
            btnLuu.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLuu.Size = new Size(90, 30); // nhỏ gọn hơn
            btnLuu.Text = "SAVE";
            btnLuu.Cursor = Cursors.Hand;

            // Nút Hủy (trắng, viền xanh)
            btnHuy.BackColor = Color.White;
            btnHuy.ForeColor = Color.DodgerBlue;
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.FlatAppearance.BorderSize = 2;
            btnHuy.FlatAppearance.BorderColor = Color.DodgerBlue;
            btnHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnHuy.Size = new Size(90, 30);
            btnHuy.Text = "CANCEL";
            btnHuy.Cursor = Cursors.Hand;

   
            
            

        }

        private void pageHeader1_Click(object sender, EventArgs e)
        {

        }
    }
}
