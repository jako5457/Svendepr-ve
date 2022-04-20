﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApiDbcontext _Dbcontext;

        public ProductController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _Dbcontext.Products.ToListAsync();
        }

        [HttpGet]
        [Route("list/category/{CategoryId}")]
        public async Task<List<Product>> GetProductsByCategoryAsync(int CategoryId)
        {
            var query = _Dbcontext.Products.Where(p => p.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault() != null);
            return await query.ToListAsync();
        }

        [HttpGet]
        [Route("Page/{Items}/{Page}")]
        public async Task<List<Product>> GetProductPagesAsync(int Items, int Page)
        {
            return await _Dbcontext.Products
                                    .GetPage(Page,Items)
                                    .AsQueryable()
                                    .ToListAsync();
        }

        [HttpGet]
        [Route("Pagecount/{Items}")]
        public Task<int> GetProductPageCountAsync(int Items)
        {
            return Task.FromResult(_Dbcontext.Products.PageCount(Items));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductAsync(ProductModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            Product product = new Product()
            {
                BuyPrice = model.BuyPrice,
                Description = model.Description,
                EAN = model.EAN,
                Name = model.Name,
            };

            if (model.Categories.Count() != 0)
            {
                foreach (int Categoryid in model.Categories)
                {
                    if (product.Categories != null)
                    {
                        product.Categories.Add(new ProductCategory() { CategoryId = Categoryid });
                    }
                    else
                    {
                        product.Categories = new List<ProductCategory>() { new ProductCategory() { CategoryId = Categoryid } };
                    }
                    
                }
            }

            try
            {
                _Dbcontext.Products.Add(product);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Product Created");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> EditProductAsync(Product product)
        {
            _Dbcontext.Products.Attach(product);
            _Dbcontext.Entry(product).State = EntityState.Modified;

            try
            {
                await _Dbcontext.SaveChangesAsync();
                return Ok("Product changed.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(int ProductId)
        {
            Product? product = await _Dbcontext.Products.Where(p => p.ProductId == ProductId).FirstOrDefaultAsync();

            if (product == null)
                return BadRequest();

            try
            {
                _Dbcontext.Remove(product);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Product removed");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}