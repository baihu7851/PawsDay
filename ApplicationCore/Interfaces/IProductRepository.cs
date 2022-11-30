using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface IProductRepository
    {
        Task CreateProductDetailById(
            IEnumerable<ProductServiceArea> productServiceArea,
            IEnumerable<ProductServicePetType> productServicePetType,
            IEnumerable<ProductServiceTime> productServiceTime,
            IEnumerable<ProductImage> productImage);

        Task UpdateProductDetailById(int productid,
            IEnumerable<ProductServiceArea> productServiceArea,
            IEnumerable<ProductServicePetType> productServicePetType,
            IEnumerable<ProductServiceTime> productServiceTime,
            IEnumerable<ProductImage> productImage);

    }
}
