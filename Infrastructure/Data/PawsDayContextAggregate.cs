using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public partial class PawsDayContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().HasData(SeedData.ServiceTypes());
            modelBuilder.Entity<County>().HasData(SeedData.Counties());
            modelBuilder.Entity<District>().HasData(SeedData.Districts());
            modelBuilder.Entity<Schedule>().HasData(SeedData.Schedules());
            modelBuilder.Entity<RegisterType>().HasData(SeedData.RegisterTypes());
            modelBuilder.Entity<Member>().HasData(SeedData.Members());
            modelBuilder.Entity<PetInfomation>().HasData(SeedData.PetInfomations());
            modelBuilder.Entity<Disposition>().HasData(SeedData.Dispositions());
            modelBuilder.Entity<PetDisposition>().HasData(SeedData.PetDispositions());
            modelBuilder.Entity<RegisterSitter>().HasData(SeedData.RegisterSitters());
            modelBuilder.Entity<Aptitude>().HasData(SeedData.Aptitudes());
            modelBuilder.Entity<Product>().HasData(SeedData.Products());
            modelBuilder.Entity<ProductImage>().HasData(SeedData.ProductImages());
            modelBuilder.Entity<ProductServiceArea>().HasData(SeedData.ProductServiceAreass());
            modelBuilder.Entity<ProductServicePetType>().HasData(SeedData.ProductServicePetTypes());
            modelBuilder.Entity<ProductServiceTime>().HasData(SeedData.ProductServiceTimes());
            modelBuilder.Entity<ProductDiscount>().HasData(SeedData.ProductDiscounts());
            modelBuilder.Entity<AdProject>().HasData(SeedData.AdProject());
            modelBuilder.Entity<Cart>().HasData(SeedData.Carts());
            modelBuilder.Entity<CartDetail>().HasData(SeedData.CartDetails());
            modelBuilder.Entity<CartSchedule>().HasData(SeedData.CartSchedules());
            modelBuilder.Entity<Order>().HasData(SeedData.Orders());
            modelBuilder.Entity<OrderPetDetail>().HasData(SeedData.OrderPetDetails());
            modelBuilder.Entity<Evaluation>().HasData(SeedData.Evaluations());
            modelBuilder.Entity<InvoiceType>().HasData(SeedData.InvoiceTypes());
            modelBuilder.Entity<OrderSchedule>().HasData(SeedData.OrderSchedules());
            modelBuilder.Entity<Role>().HasData(SeedData.Roles());
            modelBuilder.Entity<UserRole>().HasData(SeedData.UserRoles());
        }
    }
}
