using EmployeeManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement.Pages
{
    public partial class Page_ManageTask : UserControl
    {
        public Page_ManageTask()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTask frmTask = new frmTask(null);
            frmTask.Show();
        }
    }
}
