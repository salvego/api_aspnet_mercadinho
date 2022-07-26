using Mercadinho.Data;
using Mercadinho.Models;
using Mercadinho.Models.ViewModels.CartItems;
using Mercadinho.Models.ViewModels.Categories;
using Mercadinho.Models.ViewModels.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mercadinho.Controllers
{
    public class OrderController : ControllerBase
    {
        [HttpPost("v1/checkout")]
        public async Task<IActionResult> PostOrderAsync(
            [FromServices] MercadinhoDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 100)
        {
            try
            {
                var count = await context.CartItems.AsNoTracking().CountAsync();
                var cart_items = await context
                    .CartItems
                    .AsNoTracking()
                    .Include(x => x.Cart)
                    .Include(x => x.Product)
                    .Select(x => new ListCartItemsViewModel
                    {
                        CartId = x.CartId,
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        PriceUnit = x.PriceUnit,
                        UnMed = x.UnMed
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.CartId)
                    .ToListAsync();

                var total = 0.0;

                foreach (var item in cart_items)
                {

                    total += item.Quantity * item.PriceUnit;

                }

                if (count == 0)
                    return NotFound(new ResultViewModel<CartItems>("Carrinho não contém itens"));

                var dueSeconds = 3600;
                var due = DateTime.Now.AddSeconds(dueSeconds);

                var order = new Order
                {
                    CreatedDateTime = DateTime.Now,
                    OverdueDateTime = due,
                    StatusOrder = "pending_payment",
                    CopyAndPaste = "Copy",
                    Total = total
                };
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();

                var saveOrder = order.Id;

                foreach (var item in cart_items)
                {

                    var orderItem = new OrderItem
                    {
                        OrderId = saveOrder,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        Price = item.PriceUnit
                    };

                    await context.OrderItems.AddAsync(orderItem);
                    await context.SaveChangesAsync();
                }

                var cart_item = await context
                    .CartItems
                    .Where(x => x.CartId == 1)
                    .ToListAsync();

                context.CartItems.RemoveRange(cart_item);
                await context.SaveChangesAsync();

                return Created($"v1/checkout/{order.Id}", new ResultViewModel<Order>(order));

            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Order>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/orders")]
        public async Task<IActionResult> GetOrdersAsync(
            [FromServices] MercadinhoDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 100)
        {
            try
            {
                var count = await context.Orders.AsNoTracking().CountAsync();
                var orders = await context
                    .Orders
                    .AsNoTracking()
                    .Select(x => new ListOrdersViewModel
                    {
                        OrderId = x.Id,
                        CreatedDateTime = x.CreatedDateTime,
                        StatusOrder = x.StatusOrder,
                        Total = x.Total
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.OrderId)
                    .ToListAsync();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    orders
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<CartItems>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/orders/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var order = await context
                    .Orders
                    .FirstOrDefaultAsync(x => x.Id == id);

                if (order == null)
                    return NotFound(new ResultViewModel<Order>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<Order>(order));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<Order>("Falha interna no servidor"));
            }
        }

        [HttpGet("v1/ordersItems/{id:int}")]
        public async Task<IActionResult> GetOrdersItemsAsync(
            [FromRoute] int id,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var ordersItems = await context
                    .OrderItems
                    .Where(x => x.OrderId == id)
                    .ToListAsync();

                if (ordersItems == null)
                    return NotFound(new ResultViewModel<OrderItem>("Conteúdo não encontrado"));

                var count = ordersItems.Count();

                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    ordersItems
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<OrderItem>("Falha interna no servidor"));
            }
        }

    }
}