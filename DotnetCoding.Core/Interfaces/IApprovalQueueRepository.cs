using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Core.Interfaces
{
    public interface IApprovalQueueRepository : IGenericRepository<ApprovalQueue>
    {
        Task<IEnumerable<ApprovalQueue>> GetAllApprovalQueue();
        public void CreateNewApprovalQueueProduct(ApprovalQueue approvalQueue);
        public void ApproveProduct(int productId);
        public void RejectProduct(int productId);
    }
}
