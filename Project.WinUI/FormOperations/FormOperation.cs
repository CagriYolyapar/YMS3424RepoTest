using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WinUI.FormOperations
{
    public class FormOperation
    {
        public static void CreateForm(Form parentForm, Form childForm)
        {
            parentForm.Hide();
            childForm.Show();
        }
    }
}
