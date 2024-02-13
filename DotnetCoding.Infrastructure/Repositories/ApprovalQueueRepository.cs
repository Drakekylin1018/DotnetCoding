using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Infrastructure.Repositories
{
    public class ApprovalQueueRepository : GenericRepository<ApprovalQueue>, IApprovalQueueRepository
    {
        public IUnitOfWork _unitOfWork;
        public DataConstant.ProductStatusCons _statusDataConstant;
        public DataConstant.RequestReasonCons _requestReasonCons;
        public ApprovalQueueRepository(DbContextClass dbContext) : base(dbContext)
        {
        }

        /// <summary>
        /// get all pending approval itmes
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ApprovalQueue>> GetAllApprovalQueue()
        {
            return GetAll().Result.OrderBy(p => p.RequestDate)
                           .ToList();
        }

        /// <summary>
        /// Create new item to pending approval list
        /// </summary>
        /// <param name="approvalQueue"></param>
        public void CreateNewApprovalQueueProduct(ApprovalQueue approvalQueue)
        {
            Insert(approvalQueue);
        }

        /// <summary>
        /// Approve an item
        /// </summary>
        /// <param name="productId"></param>
        public void ApproveProduct(int productId)
        {
            var product = _unitOfWork.Products.GetById(productId).Result;
            var pendingItem = _unitOfWork.ApprovalQueues.GetById(productId).Result;

            if (product != null && product.ProductStatusTypeId == _statusDataConstant.PendingApproval)
            {
                if(pendingItem.RequestReasonTypeId == _requestReasonCons.Delete)
                {
                    product.ProductStatusTypeId = _statusDataConstant.Deactive;
                    _unitOfWork.Products.Update(product);
                }
                else
                {
                    product.ProductStatusTypeId = _statusDataConstant.Active;
                    _unitOfWork.Products.Update(product);
                }
                Delete(productId);
            }
        }


        /// <summary>
        /// Reject an item
        /// </summary>
        /// <param name="productId"></param>
        public void RejectProduct(int productId)
        {
            var product = _unitOfWork.Products.GetById(productId).Result;
            var pendingItem = _unitOfWork.ApprovalQueues.GetById(productId).Result;

            if (product != null && product.ProductStatusTypeId == _statusDataConstant.PendingApproval)
            {
                // Reset product to its original state
                product.ProductStatusTypeId = _statusDataConstant.Active;
                _unitOfWork.Products.Update(product);
            }
        }
    }
}
