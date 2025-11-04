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
    
    public partial class frmInforHuy : Form
    {
        private Form _parentForm;
        public frmInforHuy(string text, Form parentForm)
        {

           
            InitializeComponent();
            labelThongBao.Text = text;
            _parentForm = parentForm;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
            _parentForm.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmInforHuy.ActiveForm.Close();
        }
    }
}
