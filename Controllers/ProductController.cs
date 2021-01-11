using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Models;
using ProductCatalog.ViewModels;
using ProductCatalog.ViewModels.ProductViewModels;

namespace ProductCatalog.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<List<ListProductViewModel>>> Get([FromServices] StoreDataContext context)
        {
            return await context.Products
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

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Get([FromServices] StoreDataContext context, int id)
        {
            return await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Post([FromServices] StoreDataContext context, [FromBody] EditorProductViewModel model)
        {
            var product = new Product();
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            context.Products.Add(product);
            await context.SaveChangesAsync();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto cadastrado com sucesso!",
                Data = product
            };
        }

        [HttpPut]
        [Route("")]
        public async Task<ActionResult<ResultViewModel>> Put([FromServices] StoreDataContext context, [FromBody] EditorProductViewModel model)
        {
            var product = context.Products.Find(model.Id);
            product.Title = model.Title;
            product.CategoryId = model.CategoryId;
            //product.CreateDate = DateTime.Now;
            product.Description = model.Description;
            product.Image = model.Image;
            product.LastUpdateDate = DateTime.Now;
            product.Price = model.Price;
            product.Quantity = model.Quantity;

            context.Entry<Product>(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return new ResultViewModel
            {
                Success = true,
                Message = "Produto alterado com sucesso!",
                Data = product
            };
        }

        [HttpDelete]
        [Route("")]
        public async Task<ActionResult<Product>> Delete([FromServices] StoreDataContext context, [FromBody] Product model)
        {
            context.Products.Remove(model);
            await context.SaveChangesAsync();

            return model;
        }
    }
}