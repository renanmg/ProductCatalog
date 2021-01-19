using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> Get();
        Task<Category> Get(int id);
        Task<List<Product>> GetProducts(int id);
        Task Save(Category category);
        Task Update(Category category);
        Task Delete(Category category);
    }
}