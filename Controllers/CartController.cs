using Mercadinho.Data;
using Mercadinho.Extensions;
using Mercadinho.Models;
using Mercadinho.Models.ViewModels.CartItems;
using Mercadinho.Models.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mercadinho.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        [HttpGet("v1/cart_items")]
        public async Task<IActionResult> GetCartItemsAsync(
            [FromServices] MercadinhoDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
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
                
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    cart_items
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<CartItems>>("05X04 - Falha interna no servidor"));
            }
        }

        [HttpGet("v1/cart_items/{id:int}")]
        public async Task<IActionResult> GetByIdAsync(
            [FromRoute] int id,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var cartItem = await context
                    .CartItems
                    .Include(x=> x.Product)
                    .FirstOrDefaultAsync(x => x.ProductId == id);

                if (cartItem == null)
                    return NotFound(new ResultViewModel<CartItems>("Conteúdo não encontrado"));

                return Ok(new ResultViewModel<CartItems>(cartItem));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<CartItems>("Falha interna no servidor"));
            }
        }

        [HttpPost("v1/cart_items")]
        public async Task<IActionResult> PostAsync(
            [FromBody] EditorCartItemsViewModel model,
            [FromServices] MercadinhoDataContext context)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResultViewModel<CartItems>(ModelState.GetErrors()));

            try
            {
                var cart_item = new CartItems
                {
                    CartId = 1,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity,
                    PriceUnit = model.PriceUnit,
                    UnMed = model.UnMed
                };
                await context.CartItems.AddAsync(cart_item);
                await context.SaveChangesAsync();

                return Created($"v1/cart_items/{cart_item.CartId}", new ResultViewModel<CartItems>(cart_item));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05XE9 - Não foi possível incluir o carrinho"));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05X10 - Falha interna no servidor"));
            }
        }

        [HttpPut("v1/cart_items/{cartid:int}/{productid:int}")]
        public async Task<IActionResult> PutAsync(
            [FromRoute] int cartid,
            [FromRoute] int productid,
            [FromBody] EditorCartItemsViewModel model,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var cart_item = await context
                    .CartItems
                    .FirstOrDefaultAsync(x => x.CartId == cartid && x.ProductId == productid);

                if (cart_item == null)
                    return NotFound(new ResultViewModel<CartItems>("Conteúdo não encontrado"));

                cart_item.Quantity = model.Quantity;
                cart_item.PriceUnit = model.PriceUnit;
                cart_item.UnMed = model.UnMed;

                context.CartItems.Update(cart_item);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<CartItems>(cart_item));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05XE8 - Não foi possível alterar o carrinho"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05X11 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/cart_items/{cartid:int}/{productid:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int cartid,
            [FromRoute] int productid,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var cart_item = await context
                    .CartItems
                    .FirstOrDefaultAsync(x => x.CartId == cartid && x.ProductId == productid);

                if (cart_item == null)
                    return NotFound(new ResultViewModel<CartItems>("Conteúdo não encontrado"));

                context.CartItems.Remove(cart_item);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<CartItems>(cart_item));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05XE7 - Não foi possível excluir o carrinho"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05X12 - Falha interna no servidor"));
            }
        }

        [HttpDelete("v1/cart_items/{cartid:int}")]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int cartid,
            [FromServices] MercadinhoDataContext context)
        {
            try
            {
                var cart_item = await context
                    .CartItems
                    .Where(x => x.CartId == cartid)
                    .ToListAsync();

                if (cart_item == null)
                    return NotFound(new ResultViewModel<CartItems>("Conteúdo não encontrado"));

                context.CartItems.RemoveRange(cart_item);
                await context.SaveChangesAsync();

                return Ok(new ResultViewModel<CartItems>(new CartItems()));
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05XE7 - Não foi possível excluir o carrinho"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResultViewModel<CartItems>("05X12 - Falha interna no servidor"));
            }
        }
    }
}