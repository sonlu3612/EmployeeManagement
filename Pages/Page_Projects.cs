using AntdUI;
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
    public partial class Page_Projects : AntdUI.Window
    {
        public Page_Projects()
        {
            InitializeComponent();
        }

        private void Page_Projects_Load(object sender, EventArgs e)
        {
            table1.Columns.Add(new Column("ProjectName", "Project Name"));
            table1.Columns.Add(new Column("Description", "Description"));
            table1.Columns.Add(new Column("ProjectOwnerID", "Project Owner ID"));
            table1.Columns.Add(new Column("Status", "Status"));
            table1.Columns.Add(new Column("Document", "Document"));
            table1.Columns.Add(new Column("StartDate", "Start Date"));
            table1.Columns.Add(new Column("CompletionDate", "Completion Date"));
        }

        private void table1_CellClick(object sender, AntdUI.TableClickEventArgs e)
        {

        }
    }
}
