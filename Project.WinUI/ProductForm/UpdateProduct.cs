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


        private void LstProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstProducts.SelectedIndex > -1)
            {
                clbAttribute.Items.Clear();
                clbCategory.Items.Clear();

                product = lstProducts.SelectedItem as Product;
                txtProductName.Text = product.ProductName;
                txtProductDescription.Text = product.Description;

                productCategories = productCategoryRepository.Where(x=>x.ProductID == product.ID);

                foreach (CheckBox c in clbAttribute.Items)
                {
                    if (productDetails.Contains(productDetailRepository.Find(Convert.ToInt32(c.Tag)))) {
                        c.Checked = true;
                    }
                }

                productDetails = productDetailRepository.Where(x=>x.ProductID == product.ID);

                foreach (CheckBox c in clbCategory.Items)
                {
                    if (productCategories.Contains(productCategoryRepository.Find(Convert.ToInt32(c.Tag))))
                    {
                        c.Checked = true;
                    }
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (product != null)
            {
                productRepository.Delete(product);
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
                foreach (CheckBox c in clbCategory.Items)
                {
                    if (productCategories.Contains(productCategoryRepository.Find(Convert.ToInt32(c.Tag))))
                    {
                        if (!c.Checked)
                        {
                            productCategoryRepository.Delete(productCategoryRepository.Select(x=>x.CategoryID == Convert.ToInt32(c.Tag) && x.ProductID == product.ID) as ProductCategory);
                        }
                    }
                }

                //Daha önce kayıtlı olan, şu an uncheck olan attributeleri, productdetail'den sil.
                foreach (CheckBox c in clbAttribute.Items)
                {
                    if (productDetails.Contains(productDetailRepository.Find(Convert.ToInt32(c.Tag))))
                    {
                        if (!c.Checked)
                        {
                            productDetailRepository.Delete(productDetailRepository.Select(x => x.AttributeID == Convert.ToInt32(c.Tag) && x.ProductID == product.ID) as ProductDetail);
                        }
                    }
                }

                
                //Yeni seçilen attributeları, productDetail'e ekle.
                foreach (CheckBox c in clbAttribute.CheckedItems)
                {
                    if (!productDetails.Contains(productDetailRepository.Find(Convert.ToInt32(c.Tag))))
                    {
                        if (c.Checked)
                        {
                            ProductDetail productDetail = new ProductDetail();
                            productDetail.AttributeID = Convert.ToInt32(c.Tag);
                            productDetail.ProductID = product.ID;
                            productDetailRepository.Add(productDetail);
                        }
                    }
                }

                //Yeni seçilen kategorileri, productcategory'e ekle.
                foreach (CheckBox c in clbCategory.CheckedItems)
                {
                    if (!productCategories.Contains(productCategoryRepository.Find(Convert.ToInt32(c.Tag))))
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
            }
            else
            {
                MessageBox.Show("Ürün Seçiniz!");
            }
        }
    }
}
