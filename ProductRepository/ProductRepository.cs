using ProductCatalog.ViewModels.ProductViewModels;
using ProductCatalog.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using ProductCatalog.Models;

namespace ProductCatalog.Repositories
{
    public class ProductRepository
    {

        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task<List<ListProductViewModel>> Get()
        {
            return await _context.Products
            .Include(x => x.Category)
            .Select(x => new ListProductViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                Category = x.Category.Title,
                CategoryId = x.Category.Id
            })
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task Save(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            _context.Entry<Product>(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

    }
}