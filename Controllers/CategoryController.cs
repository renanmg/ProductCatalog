using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<List<Category>>> Get([FromServices] ICategoryRepository repository)
        {
            return await repository.Get();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Get([FromServices] ICategoryRepository repository, int id)
        {
            return await repository.Get(id);
        }

        [HttpGet]
        [Route("{id:int}/products")]
        [ResponseCache(Duration = 30)]
        public async Task<ActionResult<List<Product>>> GetProducts([FromServices] ICategoryRepository repository, int id)
        {
            return await repository.GetProducts(id);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] ICategoryRepository repository, [FromBody] Category model)
        {
            await repository.Save(model);
            return model;
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<Category>> Put([FromServices] ICategoryRepository repository, [FromBody] Category model)
        {
            await repository.Update(model);
            return model;
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<Category>> Delete([FromServices] ICategoryRepository repository, [FromBody] Category model)
        {
            await repository.Delete(model);
            return model;
        }
    }
}