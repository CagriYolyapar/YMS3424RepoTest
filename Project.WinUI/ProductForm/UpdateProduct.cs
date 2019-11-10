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
    public partial class UpdateProduct : Form
    {
        public UpdateProduct()
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
        EntityAttributeRepository entityAttributeRepository;
        Product product;
        List<ProductCategory> productCategories;
        List<ProductDetail> productDetails;
        List<Category> categories;
        List<EntityAttribute> entityAttributes;
        ProductCategoryRepository productCategoryRepository;
        ProductDetailRepository productDetailRepository;

        private void UpdateProduct_Load(object sender, EventArgs e)
        {


            categories = categoryRepository.GetActives();
            entityAttributes = entityAttributeRepository.GetActives();

            ListBoxLoad();
        }

        private void ListBoxLoad()
        {
            lstProducts.DataSource = productRepository.GetActives();
            lstProducts.SelectedIndex = -1;
        }

        List<int> currentProductCategories = new List<int>();
        List<int> currentProductAttributes = new List<int>();
        private void LstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (product != null)
            {
                productRepository.Delete(product);
                foreach (ProductCategory item in productCategoryRepository.Where(x => x.ProductID == product.ID))
                {
                    productCategoryRepository.Delete(item);
                }

                foreach (ProductDetail item in productDetailRepository.Where(x => x.ProductID == product.ID))
                {
                    productDetailRepository.Delete(item);
                }

                UncheckAllItems(flpAttribute);
                UncheckAllItems(flpCategory);
                ListBoxLoad();
            }
            else
            {
                MessageBox.Show("Ürün Seçiniz!");
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (product != null)
            {
                productRepository.Update(product);

                //Daha önce kayıtlı olan, şu an uncheck olan categorileri, productcategory'den sil.
                foreach (CheckBox c in flpCategory.Controls)
                {
                    int ID = Convert.ToInt32(c.Tag);
                    if (productCategories.Contains(productCategoryRepository.FirstOrDefault(x => x.CategoryID == ID && x.ProductID == product.ID)))
                    {
                        if (!c.Checked)
                        {
                            productCategoryRepository.Delete(productCategoryRepository.FirstOrDefault(x => x.CategoryID == ID && x.ProductID == product.ID));
                        }

                    }
                }

                //Daha önce kayıtlı olan, şu an uncheck olan attributeleri, productdetail'den sil.
                foreach (CheckBox c in flpAttribute.Controls)
                {
                    int ID = Convert.ToInt32(c.Tag);
                    if (productDetails.Contains(productDetailRepository.FirstOrDefault(x => x.EntityAttributeID == ID && x.ProductID == product.ID)))
                    {
                        if (!c.Checked)
                        {
                            productDetailRepository.Delete(productDetailRepository.FirstOrDefault(x => x.EntityAttributeID == ID && x.ProductID == product.ID));
                        }
                    }
                }


                //Yeni seçilen attributeları, productDetail'e ekle.
                foreach (CheckBox c in flpAttribute.Controls)
                {
                    int ID = Convert.ToInt32(c.Tag);
                    if (!productDetails.Contains(productDetailRepository.FirstOrDefault(x => x.EntityAttributeID == ID && x.ProductID == product.ID)))
                    {
                        if (c.Checked)
                        {
                            ProductDetail productDetail = new ProductDetail();
                            productDetail.EntityAttributeID = Convert.ToInt32(c.Tag);
                            productDetail.ProductID = product.ID;
                            productDetailRepository.Add(productDetail);
                        }
                    }
                }

                //Yeni seçilen kategorileri, productcategory'e ekle.
                foreach (CheckBox c in flpCategory.Controls)
                {
                    int ID = Convert.ToInt32(c.Tag);
                    if (!productCategories.Contains(productCategoryRepository.FirstOrDefault(x => x.CategoryID == ID && x.ProductID == product.ID)))
                    {
                        if (c.Checked)
                        {
                            ProductCategory productCategory = new ProductCategory();
                            productCategory.CategoryID = Convert.ToInt32(c.Tag);
                            productCategory.ProductID = product.ID;
                            productCategoryRepository.Add(productCategory);
                        }
                    }
                }

                UncheckAllItems(flpAttribute);
                UncheckAllItems(flpCategory);

                ListBoxLoad();
            }
            else
            {
                MessageBox.Show("Ürün Seçiniz!");
            }
        }

        private void UpdateProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChooseForm chooseForm = new ChooseForm();
            chooseForm.Show();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach (Control item in flpAttribute.Controls)
            {
                (item as CheckBox).Checked = true;
            }
        }

        private void LstProducts_Click(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex > -1)
            {
                flpAttribute.Controls.Clear();
                flpCategory.Controls.Clear();

                product = lstProducts.SelectedItem as Product;
                txtProductName.Text = product.ProductName;
                txtProductDescription.Text = product.Description;

                productDetails = productDetailRepository.Where(x => x.ProductID == product.ID && x.Status != MODEL.Enums.DataStatus.Deleted);

                productCategories = productCategoryRepository.Where(x => x.ProductID == product.ID && x.Status != MODEL.Enums.DataStatus.Deleted);

                foreach (Category item in categories)
                {
                    ProductCategory pc = productCategoryRepository.FirstOrDefault(x => x.ProductID == product.ID && x.CategoryID == item.ID);

                    CheckBox c = new CheckBox();
                    c.Text = item.CategoryName;
                    c.Tag = item.ID;
                    if (productCategories.Contains(pc))
                    {
                        c.Checked = true;
                    }
                    flpCategory.Controls.Add(c);
                }

                foreach (EntityAttribute item in entityAttributes)
                {
                    ProductDetail pd = productDetailRepository.FirstOrDefault(x => x.ProductID == product.ID && x.EntityAttributeID == item.ID);

                    CheckBox c = new CheckBox();
                    c.Text = item.AttributeName;
                    c.Tag = item.ID;
                    if (productDetails.Contains(pd))
                    {
                        c.Checked = true;
                    }

                    flpAttribute.Controls.Add(c);
                }
            }
        }
    }
}
