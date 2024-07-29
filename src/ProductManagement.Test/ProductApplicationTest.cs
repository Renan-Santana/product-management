using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using Xunit;
using AutoMapper;
using FakeItEasy;
using FluentValidation;
using ProductManagement.Application;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.DTOs.Response;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Services;

namespace ProductManagement.Test
{
    public class ProductApplicationTest
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IValidator<ProductRequestDto> _validator;
        private readonly ProductApplication _productApplication;
        public ProductApplicationTest()
        {
            _mapper = A.Fake<IMapper>();
            _productService = A.Fake<IProductService>();
            _validator = A.Fake<IValidator<ProductRequestDto>>();
            _productApplication = new ProductApplication(_mapper, _productService, _validator);
        }

        [Fact]
        public void Add_ShouldReturnBadRequest_WhenValidationFails()
        {
            var productRequest = new ProductRequestDto();
            A.CallTo(() => _validator.Validate(productRequest)).Returns(ProductMockFactory.GetValidationResult());

            var result = _productApplication.Add(productRequest);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Add_ShouldReturnCreated_WhenValid()
        {
            A.CallTo(() => _validator.Validate(ProductMockFactory.emptyProductRequestDto)).Returns(ProductMockFactory.emptyValidationResult);
            A.CallTo(() => _mapper.Map<Product>(ProductMockFactory.emptyProductRequestDto)).Returns(ProductMockFactory.emptyProduct);

            var result = _productApplication.Add(ProductMockFactory.emptyProductRequestDto);

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);
            A.CallTo(() => _productService.Add(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Delete_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            A.CallTo(() => _productService.Get(ProductMockFactory.productId)).Returns(null as Product);

            var result = _productApplication.Delete(ProductMockFactory.productId);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void Delete_ShouldReturnNoContent_WhenProductExists()
        {
            A.CallTo(() => _productService.Get(ProductMockFactory.productId)).Returns(ProductMockFactory.emptyProduct);

            var result = _productApplication.Delete(ProductMockFactory.productId);

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            A.CallTo(() => _productService.SoftDelete(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_ShouldReturnBadRequest_WhenValidationFails()
        {
            A.CallTo(() => _validator.Validate(ProductMockFactory.emptyProductRequestDto)).Returns(ProductMockFactory.GetValidationResult());

            var result = _productApplication.Update(ProductMockFactory.productId, ProductMockFactory.emptyProductRequestDto);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
            Assert.NotEmpty(result.Errors);
        }

        [Fact]
        public void Update_ShouldReturnNoContent_WhenValid()
        {
            A.CallTo(() => _validator.Validate(ProductMockFactory.emptyProductRequestDto)).Returns(ProductMockFactory.emptyValidationResult);
            A.CallTo(() => _mapper.Map<Product>(ProductMockFactory.emptyProductRequestDto)).Returns(ProductMockFactory.emptyProduct);

            var result = _productApplication.Update(ProductMockFactory.productId, ProductMockFactory.emptyProductRequestDto);

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
            A.CallTo(() => _productService.Update(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void GetById_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            A.CallTo(() => _productService.Get(ProductMockFactory.productId)).Returns(null as Product);

            var result = _productApplication.GetById(ProductMockFactory.productId);

            Assert.Equal(HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public void GetById_ShouldReturnOk_WhenProductExists()
        {
            A.CallTo(() => _productService.Get(ProductMockFactory.productId)).Returns(ProductMockFactory.emptyProduct);
            A.CallTo(() => _mapper.Map<ProductResponseDto>(ProductMockFactory.emptyProduct)).Returns(ProductMockFactory.emptyProductResponseDto);

            var result = _productApplication.GetById(ProductMockFactory.productId);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.Equal(ProductMockFactory.emptyProductResponseDto, result.Data);
        }

        [Fact]
        public void GetList_ShouldReturnNoContent_WhenNoProductsExist()
        {
            A.CallTo(() => _productService.GetList(ProductMockFactory.emptyProductFilterRequestDto.PageNumber, ProductMockFactory.emptyProductFilterRequestDto.PageSize, null)).Returns((ProductMockFactory.emptyProductList, 0));

            var result = _productApplication.GetList(ProductMockFactory.emptyProductFilterRequestDto);

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);
        }

        [Fact]
        public void GetList_ShouldReturnOk_WhenProductsExist()
        {
            A.CallTo(() => _productService.GetList(ProductMockFactory.emptyProductFilterRequestDto.PageNumber, ProductMockFactory.emptyProductFilterRequestDto.PageSize, A<Expression<Func<Product, bool>>>._))
                .Returns((ProductMockFactory.validProductList, 1));

            A.CallTo(() => _mapper.Map<List<ProductResponseDto>>(ProductMockFactory.validProductList)).Returns(ProductMockFactory.validProductResponseDtoList);

            var result = _productApplication.GetList(ProductMockFactory.emptyProductFilterRequestDto);

            Assert.Equal(HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result.Data);
        }
    }
}
