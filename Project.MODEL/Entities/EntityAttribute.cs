﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class EntityAttribute:BaseEntity
    {
        public string AttributeName { get; set; }

        //Relational Properties
        public virtual List<ProductDetail> ProductDetails { get; set; }
    }
}
