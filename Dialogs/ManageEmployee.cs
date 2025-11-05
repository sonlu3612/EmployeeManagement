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
    public partial class ManageEmployee : AntdUI.Window
    {
        public ManageEmployee()
        {
            InitializeComponent();
        }

        private void ManageEmployee_Load(object sender, EventArgs e)
        {

        }

        public void ManageEmployee_Load(int id, string DepartmentName)
        {
            page_ManageEmployees1.Page_ManageEmployee_Load(id, DepartmentName);
        }
    }
}
