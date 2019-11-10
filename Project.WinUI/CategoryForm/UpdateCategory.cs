using Project.BLL.DesignPatterns.RepositoryPattern.ConcRep;
using Project.MODEL.Entities;
using Project.WinUI.LoginForm;
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
    public partial class UpdateCategory : Form
    {
        public UpdateCategory()
        {
            InitializeComponent();
            categoryRepository = new CategoryRepository();
        }

        CategoryRepository categoryRepository;
        Category category;

        private void UpdateCategory_Load(object sender, EventArgs e)
        {
            ListBoxLoad();
        }

        void ListBoxLoad()
        {
            lstCategories.DataSource = categoryRepository.GetActives();
            lstCategories.SelectedIndex = -1;
        }

        private void LstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstCategories.SelectedIndex > -1)
            {
                category = lstCategories.SelectedItem as Category;
                txtCategoryName.Text = category.CategoryName;
                txtCategoryDescription.Text = category.Description;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if(category != null)
            {
                categoryRepository.Delete(category);
                ListBoxLoad();
            }
            else
            {
                MessageBox.Show("Kategori Seçiniz!");
            }
            
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (category != null)
            {
                categoryRepository.Update(category);
                ListBoxLoad();
            }
            else
            {
                MessageBox.Show("Kategori Seçiniz!");
            }
        }

        private void UpdateCategory_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChooseForm chooseForm = new ChooseForm();
            chooseForm.Show();
        }
    }
}
