using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithLinq.Models;
using WebApiWithLinq.Repositories;

namespace WebApiWithLinq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var produts =  await _productRepository.GetAll();
            return Ok(produts);
        }
        [HttpGet("ThirdSalary")]
        public async Task<IActionResult> ThirdSalary()
        {
            var data = await _productRepository.GetThirdHighestSalaryAsync();

            if (data == null)
                return NotFound("Third highest salary not found");

            return Ok(data);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productRepository.GetById(id);
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var result = await _productRepository.Create(product);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            var existing = await _productRepository.GetById(id);

            if (existing == null)
                return NotFound("Product not found");
            existing.Name = product.Name;
            existing.Price = product.Price;
            existing.Quantity = product.Quantity;
            existing.Salary = product.Salary;
            var result = await _productRepository.Update(existing);

            return Ok(result);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, ProductPatchDto product)
        {
            var existing = await _productRepository.GetById(id);

            if (existing == null)
                return NotFound("Product not found");

            if (product.Name != null)
                existing.Name = product.Name;

            if (product.Price.HasValue)
                existing.Price = product.Price.Value;

            if (product.Quantity.HasValue)
                existing.Quantity = product.Quantity.Value;

            if (product.Salary.HasValue)
                existing.Salary = product.Salary.Value;

            var result = await _productRepository.Patch(existing);

            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productRepository.Delete(id);
            if (!result)
                return NotFound();

            return Ok("Product deleted successfully");
        }
    }
}
