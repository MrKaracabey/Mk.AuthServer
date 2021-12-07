using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mk.AuthServer.Core.Dtos;
using Mk.AuthServer.Core.models;
using Mk.AuthServer.Core.Services;
using SharedLibrary.Dtos;

namespace Mk.AuthServer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDto> _productService;

        public ProductController(IServiceGeneric<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> InsertProduct(ProductDto productDto)
        {
            return  ActionResultInstance(await _productService.AddAsync(productDto)) ;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            return ActionResultInstance(await _productService.GetAllAsync());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productService.Update(productDto, productDto.Id));
        }
        
        [HttpDelete("{id}")] //Bunu Vermez isem Direk Query Stringten alıcaktım
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionResultInstance(await _productService.Remove(id));
        }
        
        
    }
}