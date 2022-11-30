using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        protected readonly PawsDayContext Dbcontext;

        public ProductRepository(PawsDayContext pawsdaycontext)
        {
            Dbcontext = pawsdaycontext;
        }

        public async Task CreateProductDetailById(
            IEnumerable<ProductServiceArea> productServiceArea,
            IEnumerable<ProductServicePetType> productServicePetType,
            IEnumerable<ProductServiceTime> productServiceTime,
            IEnumerable<ProductImage> productImage)
        {
            var produdct = Dbcontext.Products.First(p => p.ProductId == productImage.First().ProductId);

            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    await Dbcontext.ProductServiceAreas.AddRangeAsync(productServiceArea);
                    await Dbcontext.ProductServicePetTypes.AddRangeAsync(productServicePetType);
                    await Dbcontext.ProductServiceTimes.AddRangeAsync(productServiceTime);
                    await Dbcontext.ProductImages.AddRangeAsync(productImage);

                    await Dbcontext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch
                {
                    Dbcontext.Products.Remove(produdct);
                    await Dbcontext.SaveChangesAsync();
                    transaction.Rollback();
                }

            }

        }

        public async Task UpdateProductDetailById(int productid,
            IEnumerable<ProductServiceArea> productServiceArea,
            IEnumerable<ProductServicePetType> productServicePetType,
            IEnumerable<ProductServiceTime> productServiceTime,
            IEnumerable<ProductImage> productImage)
        {
            var oldarea = Dbcontext.Set<ProductServiceArea>().Where(a => a.ProductId == productid);
            var oldprice = Dbcontext.Set<ProductServicePetType>().Where(p => p.ProductId == productid);
            var oldtime = Dbcontext.Set<ProductServiceTime>().Where(t => t.ProductId == productid);
            var oldimg = Dbcontext.Set<ProductImage>().Where(i => i.ProductId == productid);
            using (var transaction = Dbcontext.Database.BeginTransaction())
            {
                try
                {
                    Dbcontext.ProductServiceAreas.RemoveRange(oldarea);
                    Dbcontext.ProductServicePetTypes.RemoveRange(oldprice);
                    Dbcontext.ProductServiceTimes.RemoveRange(oldtime);
                    Dbcontext.ProductImages.RemoveRange(oldimg);

                    await Dbcontext.ProductServiceAreas.AddRangeAsync(productServiceArea);
                    await Dbcontext.ProductServicePetTypes.AddRangeAsync(productServicePetType);
                    await Dbcontext.ProductServiceTimes.AddRangeAsync(productServiceTime);
                    await Dbcontext.ProductImages.AddRangeAsync(productImage);

                    await Dbcontext.SaveChangesAsync();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }

            }
        }
    }
}
