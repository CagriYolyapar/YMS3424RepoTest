using Project.MODEL.Entities;
using Project.WinUI.CategoryForm;
using Project.WinUI.FormOperations;
using Project.WinUI.ProductForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WinUI.LoginForm
{
    public partial class ChooseForm : Form
    {
        public ChooseForm(AppUser user)
        {
            InitializeComponent();
            currentUser = user;
        }

        AppUser currentUser;

        private void BtnAddCategory_Click(object sender, EventArgs e)
        {
            FormOperation.CreateForm(this, new AddCategory());
        }

        private void BtnUpdateCategory_Click(object sender, EventArgs e)
        {
            FormOperation.CreateForm(this, new UpdateCategory());
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            FormOperation.CreateForm(this, new AddProduct());
        }

        private void BtnUpdateProduct_Click(object sender, EventArgs e)
        {
            FormOperation.CreateForm(this, new UpdateProduct());
        }

        private void ChooseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
