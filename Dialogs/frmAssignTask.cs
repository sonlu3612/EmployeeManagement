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
    public partial class frmAssignTask : Form
    {
        public frmAssignTask()
        {
            InitializeComponent();
        }

        public void frmAssignTask_Load(int id)
        {
            page_AssignTask1.Page_AssignTask_Load(id);
        }
    }
}
