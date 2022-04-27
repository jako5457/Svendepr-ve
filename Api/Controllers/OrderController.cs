using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ApiDbcontext _Dbcontext;

        public OrderController(ApiDbcontext dbcontext)
        {
            _Dbcontext = dbcontext;
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:read")]
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _Dbcontext.Orders.ToListAsync();
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:read")]
        [Route("employee/{EmployeeId}")]
        public async Task<List<Order>> GetOrderbyEmployeeAsync(int EmployeeId)
        {
            return await _Dbcontext.Orders
                                   .Where(o => o.EmployeeId == EmployeeId)
                                   .ToListAsync();
        }

        [HttpGet]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:read")]
        [Route("driver/{DriverId}")]
        public async Task<List<Order>> GetOrdersbyDriverAsync(int DriverId)
        {
            return await _Dbcontext.Orders
                                   .Where(o => o.DriverId == DriverId)
                                   .ToListAsync();
        }

        [HttpPost]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:write")]
        public async Task<IActionResult> CreateOrderAsync(OrderModel model)
        {
            Order order = new()
            {
                DriverId = model.DriverId,
                EmployeeId = model.EmployeeId,
                DeliveryAddress = model.DeliveryAddress,
                DeliveryLocation = model.DeliveryLocation,
                TrackingCode = Guid.NewGuid(),
            };

            if (model.Products != null)
            {
                if (model.Products.Count == 0)
                {
                    foreach (var Product in model.Products)
                    {
                        if (order.Products != null)
                        {
                            order.Products.Add(new OrderProduct()
                            {
                                ProductId = Product.ProductId,
                                Amount = Product.Amount,
                            });
                        }
                        else
                        {
                            order.Products = new List<OrderProduct>()
                            {
                                new OrderProduct()
                                {
                                    ProductId = Product.ProductId,
                                    Amount = Product.Amount,
                                }
                            };
                        }
                    }
                }
            }

            try
            {
                _Dbcontext.Orders.Add(order);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Order Created.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }


        }

        [HttpDelete]
        [RequiredScope(RequiredScopesConfigurationKey = "api:scopes:order:write")]
        public async Task<IActionResult> RemoveOrderAsync(int orderId)
        {
            Order? order = await _Dbcontext.Orders.Where(o => o.OrderId == orderId).FirstOrDefaultAsync();

            if (order == null)
                return NotFound();

            try
            {
                _Dbcontext.Orders.Remove(order);
                await _Dbcontext.SaveChangesAsync();
                return Ok("Order removed");
            }
            catch (Exception)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}
