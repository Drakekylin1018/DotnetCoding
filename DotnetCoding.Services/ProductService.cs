using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDetails>> GetAllProducts()
        {
            var productDetailsList = await _unitOfWork.Products.GetAllActiveProducts();
            return productDetailsList;
        }

        public async Task<IEnumerable<ProductDetails>> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var searchResult = await _unitOfWork.Products.SearchProducts(productName, minPrice, maxPrice, startDate, endDate);
            return searchResult;
        }

        public void CreateProduct(ProductDetails product)
        {
            try
            {
                _unitOfWork.Products.CreateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void UpdateProduct(ProductDetails product)
        {
            try
            {
                _unitOfWork.Products.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                _unitOfWork.Products.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
