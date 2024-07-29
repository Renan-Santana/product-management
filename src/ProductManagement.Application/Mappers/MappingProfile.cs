using AutoMapper;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ProductRequestDto, Product>()
                .ForMember(dest => dest.SupplierId, opt => opt.MapFrom(src => src.SupplierId))
                .ForMember(dest => dest.Supplier, opt => opt.Ignore());

            CreateMap<Product, ProductResponseDto>()
                .ForMember(dest => dest.Supplier, opt => opt.MapFrom(src => src.Supplier));
            
            
            CreateMap<SupplierRequestDto, Supplier>();
            CreateMap<Supplier, SupplierResponseDto>();
        }
    }
}
