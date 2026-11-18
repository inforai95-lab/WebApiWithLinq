using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        public ProductController(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient();
        }
        //[HttpGet]
        //public async Task<IActionResult> GetProduct()
        //{
        //    var pro = await _httpClient.GetStringAsync("https://localhost:5001/api/Product");
        //    return Ok(new
        //    {
        //        Message = "Prodect Create Successfully",
        //        Data = pro
        //    }) ;
        //}
        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var pro = await _httpClient.GetStringAsync(
                "https://localhost:5001/api/Product"
            );

            var data = JsonSerializer.Deserialize<JsonElement>(pro);

            return Ok(new
            {
                Message = "Products Retrieved Successfully",
                Data = data
            });
        }
      [HttpGet("{id}")]
public async Task<IActionResult> GetProductById(int id)
{
    try
    {
        var product = await _httpClient.GetFromJsonAsync<Product>(
            $"https://localhost:5001/api/Product/{id}");

        if (product == null)
        {
            return NotFound("Product not found.");
        }

        return Ok(product);
    }
    catch (HttpRequestException ex)
    {
        return StatusCode(500, ex.Message);
    }
}
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "https://localhost:5001/api/Product", product);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Product>();
                return Ok(result);
            }

            return StatusCode((int)response.StatusCode,
                await response.Content.ReadAsStringAsync());
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"https://localhost:5001/api/Product/{id}", product);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Product updated successfully");
            }

            return StatusCode(
                (int)response.StatusCode,
                await response.Content.ReadAsStringAsync()
            );
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchProduct(int id, [FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest("Product data is required.");
            }

            var request = new HttpRequestMessage(
                HttpMethod.Patch,
                $"https://localhost:5001/api/Product/{id}"
            );

            request.Content = JsonContent.Create(product);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return Ok("Product updated successfully.");
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound("Product not found.");
            }

            return StatusCode((int)response.StatusCode,
                "Error updating product.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _httpClient.DeleteAsync(
                $"https://localhost:5001/api/Product/{id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode,
                    "Unable to delete product");
            }

            return Ok("Product deleted successfully");
        }
    }
}

