using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.WinUI.CategoryForm
{
    public partial class AddCategory : Form
    {
        public AddCategory()
        {
            InitializeComponent();
            categoryRepository = new CategoryRepository();
        }
        CategoryRepository categoryRepository;

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Category category = new Category();

            category.CategoryName = txtCategoryName.Text;
            category.Description = txtCategoryDescription.Text;
            categoryRepository.Add(category);
            ClearTextBoxes();

        }
        void ClearTextBoxes()
        {
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    ((TextBox)c).Text = String.Empty;
                }
            }
        }
    }
}
