using Microsoft.AspNetCore.Mvc;
using DotnetCoding.Core.Models;
using DotnetCoding.Services.Interfaces;

namespace DotnetCoding.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Get the list of product
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if(productDetailsList == null)
            {
                return NotFound();
            }
            return Ok(productDetailsList);
        }

        /// <summary>
        /// search products
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="minPrice"></param>
        /// <param name="maxPrice"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string productName, decimal? minPrice, decimal? maxPrice, DateTime? startDate, DateTime? endDate)
        {
            var searchResults = _productService.SearchProducts(productName, minPrice, maxPrice, startDate, endDate);
            return Ok(searchResults);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductDetails product)
        {
            try
            {
                _productService.CreateProduct(product);
                return CreatedAtAction(nameof(GetProductList), new { id = product.Id }, product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to create product: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPut("Update/{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductDetails product)
        {
            try
            {
                product.Id = id;
                _productService.UpdateProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to update product: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to delete product: {ex.Message}");
            }
        }
    }
}
