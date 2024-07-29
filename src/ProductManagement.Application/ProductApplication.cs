using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using AutoMapper;
using FluentValidation;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;
using ProductManagement.Application.Interfaces;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Services;

namespace ProductManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IValidator<ProductRequestDto> _validator;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductApplication(IMapper mapper, IProductService productService, IValidator<ProductRequestDto> validator)
        {
            _mapper = mapper;
            _productService = productService;
            _validator = validator;
        }

        public BaseResponseDto Add(ProductRequestDto product)
        {
            try
            {
                var validationResult = _validator.Validate(product);

                if (!validationResult.IsValid)
                {
                    return new()
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var productEntity = _mapper.Map<Product>(product);

                _productService.Add(productEntity);

                return new()
                {
                    StatusCode = HttpStatusCode.Created,
                };
            }
            catch (Exception e)
            {

                return new()
                {
                    Errors = new() { e.Message },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public BaseResponseDto Delete(int id)
        {
            try
            {
                var product = _productService.Get(id);

                if (product is null)
                {
                    return new()
                    {
                        StatusCode = HttpStatusCode.NotFound,
                    };
                }

                _productService.SoftDelete(product);

                return new() { StatusCode = HttpStatusCode.NoContent };
            }
            catch (Exception e)
            {
                return new()
                {
                    Errors = new() { e.Message },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public BaseResponseDto Update(int id, ProductRequestDto product)
        {
            try
            {
                var validationResult = _validator.Validate(product);

                if (!validationResult.IsValid)
                {
                    return new()
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var productEntity = _mapper.Map<Product>(product);
                productEntity.Id = id;

                _productService.Update(productEntity);

                return new()
                {
                    StatusCode = HttpStatusCode.Created,
                };
            }
            catch (Exception e)
            {

                return new()
                {
                    Errors = new() { e.Message },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public BaseResponseDto<ProductResponseDto> GetById(int id)
        {
            try
            {
                var product = _productService.Get(id);

                if (product is null)
                {
                    return new()
                    {
                        StatusCode = HttpStatusCode.NotFound
                    };
                }

                var productDto = _mapper.Map<ProductResponseDto>(product);

                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = productDto
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Errors = new() { e.Message },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        public BaseResponseDto<ResultsPageResponseDto<ProductResponseDto>> GetList(ProductFilterRequestDto productFilterRequestDto)
        {
            try
            {
                var (pageNumber, pageSize) = (productFilterRequestDto.PageNumber, productFilterRequestDto.PageSize);

                var (products, totalProducts) = _productService.GetList(pageNumber, pageSize, CreateFilterExpression(productFilterRequestDto));

                if (products is null || products.ToList().Count == 0)
                {
                    return new()
                    {
                        StatusCode = HttpStatusCode.NoContent
                    };
                }

                var productsDto = _mapper.Map<List<ProductResponseDto>>(products.ToList());

                return new()
                {
                    StatusCode = HttpStatusCode.OK,
                    Data = new()
                    {
                        Items = productsDto,
                        TotalItems = totalProducts,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                    }
                };
            }
            catch (Exception e)
            {
                return new()
                {
                    Errors = new() { e.Message },
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }
        }

        private Expression<Func<Product, bool>> CreateFilterExpression(ProductFilterRequestDto filterDto)
        {
            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(filterDto.Description) || p.Description.Contains(filterDto.Description)) &&
                (!filterDto.Situation.HasValue || p.Situation == filterDto.Situation.Value) &&
                (!filterDto.ManufacturingDate.HasValue || p.ManufacturingDate == filterDto.ManufacturingDate.Value) &&
                (!filterDto.ExpirationDate.HasValue || p.ExpirationDate == filterDto.ExpirationDate.Value) &&
                (!filterDto.SupplierId.HasValue || p.SupplierId == filterDto.SupplierId.Value);

            return filter;
        }
    }
}
