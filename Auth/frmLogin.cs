using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement.Auth
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_MouseHover(object sender, EventArgs e)
        {
            btnDangNhap.Type = AntdUI.TTypeMini.Success;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            btnDangNhap.Loading = true;
        }
    }
}
