using Mercadinho.Data;
using Mercadinho.Models;
using Mercadinho.Models.ViewModels.Categories;
using Mercadinho.Models.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mercadinho.Controllers
{
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("v1/products")]
        public async Task<IActionResult> GetProductsAsync(
            [FromServices] MercadinhoDataContext context,
            [FromQuery] int page = 0,
            [FromQuery] int pageSize = 25)
        {
            try
            {
                var count = await context.Products.AsNoTracking().CountAsync();
                var products = await context
                    .Products
                    .AsNoTracking()
                    .Select(x => new ListProductsViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Picture = x.Picture,
                        Unit = x.Unit,
                        Price = x.Price,
                        Description = x.Description
                    })
                    .Skip(page * pageSize)
                    .Take(pageSize)
                    .OrderByDescending(x => x.Title)
                    .ToListAsync();
                return Ok(new ResultViewModel<dynamic>(new
                {
                    total = count,
                    page,
                    pageSize,
                    products
                }));
            }
            catch
            {
                return StatusCode(500, new ResultViewModel<List<Product>>("05X04 - Falha interna no servidor"));
            }
        }
    }
}