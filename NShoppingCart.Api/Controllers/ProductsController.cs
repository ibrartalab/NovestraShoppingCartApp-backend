using Microsoft.AspNetCore.Mvc;
using NShoppingCart.Core.Interfaces.Services;
using NShoppingCart.Core.Entities;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {   
        var products = await _productService.GetCatalogAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _productService.GetProductDetailsAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPatch("{id}/inventory")]
    public async Task<IActionResult> UpdateStock(int id, [FromBody] int adjustment)
    {
        await _productService.UpdateInventoryAsync(id, adjustment);
        return Ok();
    }
}