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

namespace Project.WinUI.ProductForm
{
    public partial class AddProduct : Form
    {
        public AddProduct()
        {
            InitializeComponent();
            productRepository = new ProductRepository();
            categoryRepository = new CategoryRepository();
            entityAttributeRepository = new EntityAttributeRepository();
            productCategoryRepository = new ProductCategoryRepository();
            productDetailRepository = new ProductDetailRepository();
        }

        ProductRepository productRepository;
        CategoryRepository categoryRepository;
        ProductCategoryRepository productCategoryRepository;
        ProductDetailRepository productDetailRepository;
        EntityAttributeRepository entityAttributeRepository;

        private void AddProduct_Load(object sender, EventArgs e)
        {
            foreach (Category item in categoryRepository.GetActives())
            {
                CheckBox c = new CheckBox();
                c.Text = item.CategoryName;
                c.Tag = item.ID;
                flpCategory.Controls.Add(c);
            }

            foreach (EntityAttribute item in entityAttributeRepository.GetActives())
            {
                CheckBox c = new CheckBox();
                c.Text = item.AttributeName;
                c.Tag = item.ID;
                flpAttribute.Controls.Add(c);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = txtProductName.Text;
            product.Description = txtProductDescription.Text;
            productRepository.Add(product);

            foreach (CheckBox item in flpCategory.Controls)
            {
                if (item.Checked)
                {
                    ProductCategory productCategory = new ProductCategory();
                    productCategory.CategoryID = Convert.ToInt32(item.Tag);
                    productCategory.ProductID = product.ID;
                    productCategoryRepository.Add(productCategory);
                }

            }
            UncheckAllItems(flpCategory);

            foreach (CheckBox item in flpAttribute.Controls)
            {
                if (item.Checked)
                {
                    ProductDetail productDetail = new ProductDetail();
                    productDetail.EntityAttributeID = Convert.ToInt32(item.Tag);
                    productDetail.ProductID = product.ID;
                    productDetailRepository.Add(productDetail);
                }
            }
            UncheckAllItems(flpAttribute);

            ClearTextBoxes();
        }
        void UncheckAllItems(FlowLayoutPanel flp)
        {
            for (int i = 0; i < flp.Controls.Count; i++)
            {
                if ((flp.Controls[i] as CheckBox).Checked)
                {
                    (flp.Controls[i] as CheckBox).Checked = false;
                }
            }
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

        private void AddProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChooseForm chooseForm = new ChooseForm();
            chooseForm.Show();
        }
    }
}
