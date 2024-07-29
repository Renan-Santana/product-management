using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById([FromRoute] int id)
        { 
            var result = _productApplication.GetById(id);
            return CreateResponse(result);
        }

        [HttpGet]
        public IActionResult Get([FromQuery] ProductFilterRequestDto filterRequestDto)
        {
            var result = _productApplication.GetList(filterRequestDto);
            return CreateResponse(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] ProductRequestDto productRequestDto)
        {
            var result = _productApplication.Add(productRequestDto);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put([FromRoute] int id, [FromBody] ProductRequestDto productRequestDto)
        {
            var result = _productApplication.Update(id, productRequestDto);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var result = _productApplication.Delete(id);
            return CreateResponse(result);
        }
    }
}
