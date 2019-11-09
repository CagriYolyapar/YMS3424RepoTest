using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Strategy
{
    public class MyInit : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            AppUser user = new AppUser();
            user.UserName = "YMS3424";
            user.Password = "123";
            context.AppUsers.Add(user);
            context.SaveChanges();



            for (int i = 0; i < 10; i++)
            {
                Category c = new Category();
                c.CategoryName = FakeData.TextData.GetAlphabetical(10);
                c.Description = FakeData.TextData.GetSentence();
                context.Categories.Add(c);
                context.SaveChanges();

                EntityAttribute et = new EntityAttribute();
                et.AttributeName = FakeData.TextData.GetAlphabetical(10);



                for (int x = 0; x < 10; x++)
                {
                    Product pro = new Product();
                    pro.ProductName = FakeData.TextData.GetAlphabetical(10);
                    pro.UnitPrice = Convert.ToDecimal(FakeData.NumberData.GetDouble());
                    pro.CategoryID = c.ID;
                    context.Products.Add(pro);
                    context.SaveChanges();

                    ProductDetail pd = new ProductDetail();
                    pd.AttributeID = et.ID;
                    pd.ProductID = pro.ID;
                    context.ProductDetails.Add(pd);
                    context.SaveChanges();

                    Order o = new Order();
                    o.ShippedAddress= FakeData.TextData.GetAlphabetical(10);
                    o.AppUserID = user.ID;
                    context.Orders.Add(o);
                    context.SaveChanges();

                    OrderDetail od = new OrderDetail();
                    od.ProductID = pro.ID;
                    od.OrderID = o.ID;
                    context.OrderDetails.Add(od);
                    context.SaveChanges();
                }
            }
        }
    }
}
