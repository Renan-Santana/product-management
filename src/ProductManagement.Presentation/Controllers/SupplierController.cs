using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.Interfaces;

namespace ProductManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : BaseController
    {
        private readonly ISupplierApplication _supplierApplication;

        public SupplierController(ISupplierApplication supplierApplication)
        {
            _supplierApplication = supplierApplication;
        }

        [HttpPost]
        public IActionResult Post([FromBody] SupplierRequestDto supplierRequestDto)
        {
            var result = _supplierApplication.Add(supplierRequestDto);
            return CreateResponse(result);
        }
    }
}
