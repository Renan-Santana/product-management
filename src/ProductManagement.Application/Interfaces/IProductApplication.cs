using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;

namespace ProductManagement.Application.Interfaces
{
    public interface IProductApplication
    {
        BaseResponseDto Add(ProductRequestDto product);
        BaseResponseDto Update(int id, ProductRequestDto product);
        BaseResponseDto Delete(int id);
        BaseResponseDto<ProductResponseDto> GetById(int id);
        BaseResponseDto<ResultsPageResponseDto<ProductResponseDto>> GetList(ProductFilterRequestDto productFilterRequestDto);

    }
}
