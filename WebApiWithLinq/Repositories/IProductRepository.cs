using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithLinq.Models;

namespace WebApiWithLinq.Repositories
{
   public  interface IProductRepository
    {
        Task<List<Product>> GetAll();
        Task<Product> GetById(int id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<bool> Delete(int id);
        Task<Product> GetThirdHighestSalaryAsync();
        Task<Product> Patch(Product product);
    }
}
