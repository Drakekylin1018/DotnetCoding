using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApprovalQueueController : Controller
    {
        public readonly IApprovalQueueService _approvalService;
        public ApprovalQueueController(IApprovalQueueService approvalService)
        {
            _approvalService = approvalService;
        }

        /// <summary>
        /// Get the List of approval queue
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllApprovalQueue()
        {
            var approvalList = await _approvalService.GetAllApprovalQueue();
            return Ok(approvalList);
        }

        /// <summary>
        /// Create new approval queue item
        /// </summary>
        /// <param name="approvalQueue"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateNewApprovalQueueProduct(ApprovalQueue approvalQueue)
        {
            try
            {
                _approvalService.CreateNewApprovalQueueProduct(approvalQueue);
                return CreatedAtAction(nameof(ApprovalQueue), new { id = approvalQueue.id }, approvalQueue);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create approval item: {ex.Message}");
            }
        }

        /// <summary>
        /// Approve a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Approve/{id}")]
        public IActionResult ApproveProduct(int id )
        {
            try
            {
                _approvalService.ApproveProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to approve product: {ex.Message}");
            }
        }

        /// <summary>
        /// Reject a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Reject/{id}")]
        public IActionResult RejectPoduct(int id)
        {
            try
            {
                _approvalService.RejectProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to reject product: {ex.Message}");
            }
        }
    }
}
