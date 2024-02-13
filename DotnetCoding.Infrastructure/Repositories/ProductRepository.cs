using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.Extensions.Configuration;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<ProductDetails>, IProductRepository
    {
        public IUnitOfWork _unitOfWork;
        public DataConstant.ProductStatusCons _statusDataConstant;
        public DataConstant.RequestReasonCons _requestReasonCons;
        public ProductRepository(DbContextClass dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get all the items for active status
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDetails>> GetAllActiveProducts()
        {
            return GetAll().Result.Where(p => p.ProductStatusTypeId == _statusDataConstant.Active)
                           .OrderByDescending(p => p.CreateDate)
                           .ToList();
        }

        /// <summary>
        /// Search products base on conditions
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ProductDetails>> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var query = GetAll().Result.Where(p => p.ProductStatusTypeId == _statusDataConstant.Active);

            if (!string.IsNullOrEmpty(productName))
            {
                query = query.Where(p => p.ProductName.Contains(productName));
            }

            if (minPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                query = query.Where(p => p.ProductPrice <= maxPrice.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(p => p.CreateDate >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(p => p.CreateDate <= endDate.Value);
            }

            return query.ToList();
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="product"></param>
        public void CreateProduct(ProductDetails product)
        {
            if (product.ProductPrice >= 0 ||  product.ProductPrice <= 5000) 
            {
                product.ProductStatusTypeId = _statusDataConstant.Active;
                product.CreateDate = DateTime.Now;
                Insert(product);
            }
            else if (product.ProductPrice > 5000)
            {
                var newApprovalQueue = new ApprovalQueue();
                newApprovalQueue.id = product.Id;
                newApprovalQueue.RequestReasonTypeId = _requestReasonCons.FiveThsoundsMore;
                newApprovalQueue.RequestDate = DateTime.Now;
                _unitOfWork.ApprovalQueues.CreateNewApprovalQueueProduct(newApprovalQueue);

                product.ProductStatusTypeId = _statusDataConstant.PendingApproval;
                product.CreateDate = DateTime.Now;
                Insert(product);
            }
            else if (product.ProductPrice > 10000)
            {
                return;
            }
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(ProductDetails product)
        {
            var existingProduct = GetById(product.Id).Result;

            if (existingProduct != null)
            {
                if (product.ProductPrice > (existingProduct.ProductPrice * 1.5))
                {
                    product.ProductStatusTypeId = _statusDataConstant.PendingApproval;

                    var newApprovalQueue = new ApprovalQueue();
                    newApprovalQueue.id = product.Id;
                    newApprovalQueue.RequestReasonTypeId = _requestReasonCons.FiftyPercentMore;
                    newApprovalQueue.RequestDate = DateTime.Now;
                    _unitOfWork.ApprovalQueues.CreateNewApprovalQueueProduct(newApprovalQueue);
                }

                // Update existing product properties
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductPrice = product.ProductPrice;

                Update(existingProduct);
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="productId"></param>
        public void DeleteProduct(int productId)
        {
            var product = GetById(productId).Result;

            if (product != null)
            {
                var newApprovalQueue = new ApprovalQueue();
                newApprovalQueue.id = product.Id;
                newApprovalQueue.RequestReasonTypeId = _requestReasonCons.Delete;
                newApprovalQueue.RequestDate = DateTime.Now;
                _unitOfWork.ApprovalQueues.CreateNewApprovalQueueProduct(newApprovalQueue);

                product.ProductStatusTypeId = _statusDataConstant.PendingApproval;
                Update(product);
            }
        }

    }
}
