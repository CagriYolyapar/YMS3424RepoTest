using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class ProductMap:BaseMap<Product>
    {
        public ProductMap()
        {
            ToTable("Ürünler");

            Property(x => x.ProductName).HasColumnName("Ürün İsmi").IsRequired();

            Property(x => x.UnitPrice).HasColumnName("Ürün Fiyatı").IsOptional();
        }
    }
}
