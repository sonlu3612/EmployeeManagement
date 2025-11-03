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
    public partial class frmManageTasks : Form
    {
        public frmManageTasks()
        {
            InitializeComponent();
        }

        public void frmManageTasks_Load(int id)
        {
            page_Task1.Page_ManageTask_Load(id);
        }

        private void page_Task1_Load(object sender, EventArgs e)
        {

        }
    }
}
