using EmployeeManagement.Auth;
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
    
    public partial class frmInfor : Form
    {
        private Form _parentForm;
        public frmInfor(string text, Form parentForm)
        {

           
            InitializeComponent();
            labelThongBao.Text = text;
            _parentForm = parentForm;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
            _parentForm.Close();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmInfor.ActiveForm.Close();
        }
    }
}
