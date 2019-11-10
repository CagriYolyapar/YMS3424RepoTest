using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        //Relational Properties
        //Product-category çoka çok ilişkide olduğu için, bu alanı kaldırdım.
        //public virtual List<Product> Product { get; set; }
        public virtual List<ProductCategory> ProductCategories { get; set; }

        public override string ToString()
        {
            return $"{CategoryName}";
        }
    }
}
