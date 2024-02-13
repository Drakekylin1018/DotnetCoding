using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDetails>> GetAllProducts();
        Task<IEnumerable<ProductDetails>> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate);
        public void CreateProduct(ProductDetails product);
        public void UpdateProduct(ProductDetails product);
        public void DeleteProduct(int productId);
    }
}
