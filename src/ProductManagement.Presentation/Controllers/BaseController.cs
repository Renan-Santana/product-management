using Microsoft.AspNetCore.Mvc;
using ProductManagement.Application.DTOs.Response;

namespace ProductManagement.Presentation.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
        }

        protected IActionResult CreateResponse(BaseResponseDto responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = (int)responseDto.StatusCode
            };
        }

        protected IActionResult CreateResponse<TData>(BaseResponseDto<TData> responseDto)
        {
            return new ObjectResult(responseDto)
            {
                StatusCode = (int)responseDto.StatusCode
            };
        }
    }
}
