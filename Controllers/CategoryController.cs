using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] StoreDataContext context)
        {
            return await context.Categories.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get([FromServices] StoreDataContext context, int id)
        {
            return await context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpGet]
        [Route("{id:int}/products")]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<Product>>> GetProducts([FromServices] StoreDataContext context, int id)
        {
            return await context.Products.AsNoTracking().Where(x => x.CategoryId == id).ToListAsync();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] StoreDataContext context, [FromBody] Category model)
        {
            context.Categories.Add(model);
            await context.SaveChangesAsync();

            return model;
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Category>> Put([FromServices] StoreDataContext context, [FromBody] Category model)
        {
            context.Entry<Category>(model).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return model;
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<Category>> Delete([FromServices] StoreDataContext context, [FromBody] Category model)
        {
            context.Categories.Remove(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}