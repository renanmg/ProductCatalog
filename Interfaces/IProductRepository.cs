

using System.Collections.Generic;
using System.Threading.Tasks;
using ProductCatalog.Models;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<ListProductViewModel>> Get();
        Task<Product> Get(int id);
        Task Save(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}