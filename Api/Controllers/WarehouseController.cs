using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {

        private readonly ApiDbcontext _Dbcontext;

        public WarehouseController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<List<Warehouse>> GetWarehousesAsync()
        {
            return await _Dbcontext.Warehouses.ToListAsync();
        }

        [HttpGet]
        [Route("{WarehouseId}")]
        public async Task<IActionResult> GetWarehouseAsync(int WarehouseId)
        {
            Warehouse? warehouse = await _Dbcontext.Warehouses.Where(x => x.WarehouseId == WarehouseId).FirstOrDefaultAsync();

            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateWarehouseAsync(WarehouseModel model)
        {
            Warehouse warehouse = new Warehouse()
            {
                Address = model.Address,
                CompanyId = model.CompanyId,
                Email = model.Email,
                Name = model.Name,
                Phone = model.Phone,
                Zipcode = model.Zipcode,
            };

            try
            {
                _Dbcontext.Warehouses.Add(warehouse);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Warehouse created..");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWarehouseAsync(Warehouse warehouse)
        {
            _Dbcontext.Warehouses.Attach(warehouse);
            _Dbcontext.Entry(warehouse).State = EntityState.Modified;

            try
            {
                await _Dbcontext.SaveChangesAsync();
                return Ok("Warehouse updated.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteWarehouseAsync(int WarehouseId)
        {
            Warehouse? warehouse = await _Dbcontext.Warehouses.Where(x => x.WarehouseId == WarehouseId).FirstOrDefaultAsync();

            if (warehouse == null)
                return NotFound();

            _Dbcontext.Remove(warehouse);

            try
            {
                await _Dbcontext.SaveChangesAsync();
                return Ok("Warehouse removed.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #region Products

        [HttpGet]
        [Route("{WarehouseId}/products")]
        public async Task<List<WarehouseProductListModel>> GetWarehouseProducts(int WarehouseId)
        {
            return await _Dbcontext.WarehouseProducts
                                .Where(wp => wp.WarehouseId == WarehouseId)
                                .Select(wp => new WarehouseProductListModel
                                {
                                    WarehouseId = wp.WarehouseId,
                                    ProductId = wp.ProductId,
                                    Amount = wp.Amount,
                                    Name = wp.Product.Name,
                                    EAN = wp.Product.EAN,
                                    Description = wp.Product.Description,
                                    BuyPrice = wp.Product.BuyPrice,
                                })
                                .ToListAsync();
        }

        [HttpPost]
        [Route("product")]
        public async Task<IActionResult> AddPrductAsync(WarehouseProductModel product)
        {
            WarehouseProduct? warehouseProduct = new WarehouseProduct()
            {
                WarehouseId = product.WarehouseId,
                ProductId = product.ProductId,
                Amount = product.Amount,
            };

            _Dbcontext.WarehouseProducts.Add(warehouseProduct);

            try
            {
                await _Dbcontext.SaveChangesAsync();
                return Ok("Product added");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("product")]
        public async Task<IActionResult> UpdateWarehousePrductAsync(WarehouseProductModel product)
        {
            WarehouseProduct? warehouseProduct = await _Dbcontext.WarehouseProducts
                                                                .Where(wp => wp.WarehouseId == product.WarehouseId)
                                                                .Where(wp => wp.ProductId == product.ProductId)
                                                                .FirstOrDefaultAsync();

            if (warehouseProduct == null)
                return NotFound();

            warehouseProduct.Amount = product.Amount;

            try
            {
                await _Dbcontext.SaveChangesAsync();
                return Ok("Warehouse product updated");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpDelete]
        [Route("product")]
        public async Task<IActionResult> RemoveWarehouseProduct(WarehouseProductModel product)
        {
            WarehouseProduct? warehouseProduct = await _Dbcontext.WarehouseProducts
                                                                .Where(wp => wp.WarehouseId == product.WarehouseId)
                                                                .Where(wp => wp.ProductId == product.ProductId)
                                                                .FirstOrDefaultAsync();
            if (warehouseProduct == null)
                return NotFound();

            try
            {
                _Dbcontext.WarehouseProducts.Remove(warehouseProduct);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Warehouse product removed.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        #endregion Products
    }
}
