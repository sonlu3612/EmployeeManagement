using EmployeeManagement.Models;
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
    public partial class frmAssignEmployee : Form
    {
        
        public frmAssignEmployee(int taskID)
        {
            InitializeComponent();
            page_AssignEmployee1.Page_AssignEmployee_Load(taskID);
        }

        private void page_AssignEmployee1_Load(object sender, EventArgs e)
        {

        }
    }
}
