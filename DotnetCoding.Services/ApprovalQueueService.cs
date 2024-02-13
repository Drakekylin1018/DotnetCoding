using DotnetCoding.Core.Interfaces;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services
{
    public class ApprovalQueueService : IApprovalQueueService
    {
        public IUnitOfWork _unitOfWork;

        public ApprovalQueueService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ApprovalQueue>> GetAllApprovalQueue()
        {
            var approvalQueueList = await _unitOfWork.ApprovalQueues.GetAllApprovalQueue();
            return approvalQueueList;
        }

        public void CreateNewApprovalQueueProduct(ApprovalQueue approvalQueue)
        {
            try
            {
                _unitOfWork.ApprovalQueues.CreateNewApprovalQueueProduct(approvalQueue);
            }
            catch (Exception ex) 
            { 
                throw new Exception(ex.Message, ex);
            }
        }

        public void ApproveProduct(int productId)
        {
            try
            {
                _unitOfWork.ApprovalQueues.ApproveProduct(productId);
            }
            catch(Exception ex) { throw new Exception(ex.Message, ex);}
        }

        public void RejectProduct(int productId)
        {
            try
            {
                _unitOfWork.ApprovalQueues.RejectProduct(productId);
            }
            catch (Exception ex) { throw new Exception(ex.Message, ex);}
        }
    }
}
