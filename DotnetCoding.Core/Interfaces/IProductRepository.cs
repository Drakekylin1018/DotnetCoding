using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IProductRepository : IGenericRepository<ProductDetails>
    {
        Task<IEnumerable<ProductDetails>> GetAllActiveProducts();
        Task<IEnumerable<ProductDetails>> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate);
        public void CreateProduct(ProductDetails product);
        public void UpdateProduct(ProductDetails product);
        public void DeleteProduct(int productId);

    }
}
