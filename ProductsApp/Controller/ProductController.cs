using dataLibrary;
using Microsoft.AspNetCore.Mvc;
using ProductsApp.data;
using Microsoft.Extensions.Configuration;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IDataAccess _dataAccess;
    private readonly IConfiguration _config; // Replace with your connection string

    public ProductController(IDataAccess dataAccess , IConfiguration config)
    {
        _dataAccess = dataAccess;
        _config = config;
    }

    // GET: /api/products
    [HttpGet] // Route for GetProducts method
   // [Route("Products")]
    public async Task<IActionResult> GetProducts()
    {
         string sql = "SELECT * FROM product";
        // var products = await _dataAccess.LoadData<ProductModel, dynamic>(sql, new { }, _config.GetConnectionString("default"));
         var products = await _dataAccess.LoadData<ProductModel, dynamic>(sql, new { });
        
        return Ok(products);
    }

    // POST: /api/products
    [HttpPost("new")]
  //  [Route("new")]
    public async Task<IActionResult> CreateProduct(ProductModel newProduct)
    {
        string sql = "INSERT INTO product (product_name) VALUES (@product_name)";
        //  await _dataAccess.SaveData(sql, new { newProduct }, _config.GetConnectionString("default"));
        
        await _dataAccess.SaveData(sql, new { product_name = newProduct.product_name });
        return StatusCode(201); // Return HTTP 201 Created
    }

    // Other actions (PUT, DELETE) can be similarly implemented using SaveData method
}
