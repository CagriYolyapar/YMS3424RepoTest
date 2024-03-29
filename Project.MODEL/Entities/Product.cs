﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Product:BaseEntity
    {
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Description { get; set; }
        //Product-category çoka çok ilişkide olduğu için, bu alanı kaldırdım.
        //public int? CategoryID { get; set; }


        //Relational Properties
        public virtual List<ProductDetail> ProductDetails { get; set; }
        //Product-category çoka çok ilişkide olduğu için, bu alanı kaldırdım.
        //public virtual Category Category { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
        public virtual List<ProductCategory> ProductCategories { get; set; }

        public override string ToString()
        {
            return $"{ProductName}->{UnitPrice}";
        }

    }
}
