using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithLinq.Data;
using WebApiWithLinq.Models;

namespace WebApiWithLinq.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Product> Create(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }
        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);

            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<Product> Patch(Product product)
        {
            await _context.SaveChangesAsync();

            return product;
        }
        public async Task<bool> Delete(int id)
        {
            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == id);

            if (product == null)
                return false;

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<Product> GetThirdHighestSalaryAsync()
        {
            return await _context.Products
                .OrderByDescending(x => x.Salary)
                .Skip(2)
                .FirstOrDefaultAsync();
        }

        }
}
