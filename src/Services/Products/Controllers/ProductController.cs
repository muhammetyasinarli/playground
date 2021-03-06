using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Products.Entities;
using Products.Repositories.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Linq;


namespace Products.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController :  ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> ProductCreate([FromBody] Product product)
        {
            var result = await _productRepository.AddAsync(product);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogError($"Product with id : {id},hasn't been found in databasei");
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.UpdateAsync(product));
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> DeleteProductsAsync([FromQuery] int[] ids)
        {
            if(ids == null || !ids.Any())
            {
                _logger.LogError($"There aren't any product id in order to delete");
                return NotFound();
            }

            int deletedCount = 0;
            foreach (var id in ids)
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product != null)
                {
                    await _productRepository.DeleteAsync(product);
                    ++deletedCount; 
                }
            }

            return Ok(deletedCount);
        }
    }
}
