﻿using Project.DAL.Strategy;
using Project.MAP.Options;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Context
{
    public class MyContext:DbContext
    {
        public MyContext() : base("MyConnection")
        {
            Database.SetInitializer(new MyInit()); 
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AppUserMap());
            modelBuilder.Configurations.Add(new AppUserDetailMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new EntityAttributeMap());
            modelBuilder.Configurations.Add(new OrderDetailMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Configurations.Add(new ProductDetailMap());
            modelBuilder.Configurations.Add(new ProductMap());



        }
        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<AppUserDetail> Profiles { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<EntityAttribute> EntityAttributes { get; set; }

        public DbSet<Product> Products { get; set; }


        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet <OrderDetail> OrderDetails { get; set; }
    }
}
