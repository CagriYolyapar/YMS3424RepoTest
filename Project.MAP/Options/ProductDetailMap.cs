using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductDetailMap : BaseMap<ProductDetail>
    {
        public ProductDetailMap()
        {
            ToTable("Ürün Detayları");

            Ignore(x => x.ID);
            HasKey(x => new
            {
                x.EntityAttributeID,
                x.ProductID

            });
        }
    }
}
