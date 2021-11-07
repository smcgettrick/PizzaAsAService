using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaAsAService.MenuService.Api.Data.Repositories.Interfaces;
using PizzaAsAService.MenuService.Api.Entities;

namespace PizzaAsAService.MenuService.Api.Controllers;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IProductRepository _repository;

        public MenuController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [Route("[action]/{name}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProductByName(string name)
        {
            var product = await _repository.GetProductByName(name);

            if (product.Any())
                return Ok(product);

            return NotFound(name);
        }

        [Route("[action]/{category}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<Product>>> GetProductByCategory(string category)
        {
            if (!Enum.TryParse(category, out Categories categoryEnum)) 
                return NotFound(category);

            var product = await _repository.GetProductByCategory(category);
            return Ok(product);

        }
}

