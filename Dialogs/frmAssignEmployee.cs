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
        private int _taskID;
        
        public frmAssignEmployee(int taskID)
        {
            InitializeComponent();
            this._taskID = taskID;
            
        }

       
    }
}
