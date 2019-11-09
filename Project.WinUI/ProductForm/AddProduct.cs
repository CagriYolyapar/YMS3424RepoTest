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
            //List<Product> products = productRepository.GetActives();
            List<Category> categories = categoryRepository.GetActives();
            List<EntityAttribute> entityAttributes = entityAttributeRepository.GetActives();

            foreach (Category item in categories)
            {
                CheckBox c = new CheckBox();
                c.Text = item.CategoryName;
                c.Tag = item.ID;

                clbCategory.Items.Add(c);
            }
            foreach (EntityAttribute item in entityAttributes)
            {
                CheckBox c = new CheckBox();
                c.Text = item.AttributeName;
                c.Tag = item.ID;

                clbAttribute.Items.Add(c);
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Product product = new Product();
            product.ProductName = txtProductName.Text;
            product.Description = txtProductDescription.Text;

            foreach (CheckBox item in clbCategory.CheckedItems)
            {
                ProductCategory productCategory = new ProductCategory();
                productCategory.CategoryID = Convert.ToInt32(item.Tag);
                productCategory.ProductID = product.ID;
                productCategoryRepository.Add(productCategory);
            }

            foreach (CheckBox item in clbAttribute.CheckedItems)
            {
                ProductDetail productDetail = new ProductDetail();
                productDetail.AttributeID = Convert.ToInt32(item.Tag);
                productDetail.ProductID = product.ID;
                productDetailRepository.Add(productDetail);
            }
        }
    }
}
