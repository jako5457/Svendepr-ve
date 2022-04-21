using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductRequestController : ControllerBase
    {

        private readonly ApiDbcontext _Dbcontext;

        public ProductRequestController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<ProductRequest>> GetProductRequests()
        {
            return await _Dbcontext.ProductRequests.ToListAsync();
        }

        [HttpGet]
        [Route("{ProductRequestId}")]
        public async Task<IActionResult> GetProductRequest(int ProductRequestId)
        {
            var productRequest = await _Dbcontext.ProductRequests
                                                 .Where(x => x.ProductRequestId == ProductRequestId)
                                                 .FirstOrDefaultAsync();

            if (productRequest == null)
                return NotFound();

            return Ok(productRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductRequest(ProductRequestModel model)
        {
            ProductRequest request = new ProductRequest()
            {
                EmployeeId = model.EmployeeId,
                Location = model.Location,
            };

            if (model.Products != null)
            {
                if (model.Products.Count != 0)
                {
                    foreach (var product in model.Products)
                    {
                        if (request.Products != null)
                        {
                            request.Products.Add(new ProductRequestProduct()
                            {
                                ProductId = product.ProductId,
                                Amount = product.Amount,
                            });
                        }
                        else
                        {
                            request.Products = new List<ProductRequestProduct>
                            {
                                new ProductRequestProduct()
                                {
                                    ProductId = product.ProductId,
                                    Amount = product.Amount,
                                }
                            };
                        }
                    }
                }
            }

            try
            {
                _Dbcontext.ProductRequests.Add(request);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Request created.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("{ProductRequestId}")]
        public async Task<IActionResult> DeleteProductRequest(int ProductRequestId)
        {
            var productRequest = await _Dbcontext.ProductRequests
                                                 .Where(x => x.ProductRequestId == ProductRequestId)
                                                 .FirstOrDefaultAsync();

            if (productRequest == null)
                return NotFound();

            try
            {
                _Dbcontext.ProductRequests.Remove(productRequest);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Request removed.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
