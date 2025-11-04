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
    
    public partial class frmInforXoa : Form
    {
        //private Form _parentForm;
        public frmInforXoa(string text)
        {

           
            InitializeComponent();
            labelThongBao.Text = text;
            //_parentForm = parentForm;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmInfor.ActiveForm.Close();
        }

      
        private void btnXoa_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }
    }
}
