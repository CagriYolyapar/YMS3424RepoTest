using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MAP.Options
{
    public class AppUserMap:BaseMap<AppUser>
    {
        public AppUserMap()
        {
            ToTable("Kullanıcılar");

            HasOptional(x => x.AppUserDetail).WithRequired(x => x.AppUser);

            Property(x => x.UserName).HasColumnName("Kullanıcı İsmi").IsRequired();

            Property(x => x.Password).HasColumnName("Şifre").IsRequired();

            Property(x => x.Role).HasColumnName("Rol").IsOptional();
        }
    }
}
