using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;

namespace ProductManagement.Application.Interfaces
{
    public interface ISupplierApplication
    {
        BaseResponseDto Add(SupplierRequestDto supplierRequestDto);
    }
}
