using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> Get()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> Get(int id)
        {
            return await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Product>> GetProducts(int id)
        {
            return await _context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
        }

        public async Task Save(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Category category)
        {
            _context.Entry<Category>(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}