using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetCoding.Core.Models;

namespace DotnetCoding.Services.Interfaces
{
    public interface IApprovalQueueService
    {
        Task<IEnumerable<ApprovalQueue>> GetAllApprovalQueue();
        public void CreateNewApprovalQueueProduct(ApprovalQueue approvalQueue);
        public void ApproveProduct(int productId);
        public void RejectProduct(int productId);
    }
}
