using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;
using FakeItEasy;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Domain.Services;

namespace ProductManagement.Test
{
    public class ProductServiceTest
    {
        private readonly IProductRepository _productRepository;
        private readonly ProductService _productService;

        public ProductServiceTest()
        {
            _productRepository = A.Fake<IProductRepository>();
            _productService = new ProductService(_productRepository);
        }

        [Fact]
        public void Add_ShouldCallInsert_WhenCalled()
        {
            _productService.Add(ProductMockFactory.emptyProduct);

            A.CallTo(() => _productRepository.Insert(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void SoftDelete_ShouldCallSoftDelete_WhenCalled()
        {
            _productService.SoftDelete(ProductMockFactory.emptyProduct);

            A.CallTo(() => _productRepository.SoftDelete(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Update_ShouldCallUpdate_WhenCalled()
        {
            _productService.Update(ProductMockFactory.emptyProduct);

            A.CallTo(() => _productRepository.Update(ProductMockFactory.emptyProduct)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void Get_ShouldReturnProduct_WhenProductExists()
        {

            A.CallTo(() => _productRepository.GetByIdIncluding(ProductMockFactory.productId)).Returns(ProductMockFactory.emptyProduct);

            var result = _productService.Get(ProductMockFactory.productId);

            Assert.Equal(ProductMockFactory.emptyProduct, result);
        }

        [Fact]
        public void Get_ShouldReturnNull_WhenProductDoesNotExist()
        {
            A.CallTo(() => _productRepository.GetByIdIncluding(ProductMockFactory.productId)).Returns(null as Product);

            var result = _productService.Get(ProductMockFactory.productId);

            Assert.Null(result);
        }

        [Fact]
        public void GetList_ShouldReturnProductsAndTotal_WhenProductsExist()
        {
            var pageNumber = 1;
            var pageSize = 10;
            var totalProducts = 1;

            A.CallTo(() => _productRepository.GetAllIncluding(pageNumber, pageSize)).Returns((ProductMockFactory.validProductList, totalProducts));

            var (pageProducts, total) = _productService.GetList(pageNumber, pageSize);

            Assert.Equal(ProductMockFactory.validProductList, pageProducts);
            Assert.Equal(totalProducts, total);
        }

        [Theory]
        [InlineData("vidro", true, "2023-01-01", "2023-12-31", 1, 1, 10)]
        [InlineData("farol", false, "2023-06-01", "2024-06-01", 2, 2, 5)]
        [InlineData("pneu", true, "2023-03-01", "2024-03-01", 3, 3, 20)]
        public void GetList_ShouldApplyMultipleFilters(string description, bool situation, string manufacturingDate, string expirationDate, short supplierId, int pageNumber, int pageSize)
        {

            var manufacturingDateParsed = DateTime.Parse(manufacturingDate);
            var expirationDateParsed = DateTime.Parse(expirationDate);

            Expression<Func<Product, bool>> predicate = p =>
                p.Description.Contains(description) &&
                p.Situation == situation &&
                p.ManufacturingDate == manufacturingDateParsed &&
                p.ExpirationDate == expirationDateParsed &&
                p.SupplierId == supplierId;

            var products = new List<Product>
            {
                new Product
                {
                    Description = description,
                    Situation = situation,
                    ManufacturingDate = manufacturingDateParsed,
                    ExpirationDate = expirationDateParsed,
                    SupplierId = supplierId
                }
            };
            var totalProducts = products.Count;

            A.CallTo(() => _productRepository.GetByFilterIncluding(predicate, pageNumber, pageSize))
                .Returns((products, totalProducts));

            var (pageProducts, total) = _productService.GetList(pageNumber, pageSize, predicate);

            Assert.Single(pageProducts);
            Assert.Contains(pageProducts, p =>
                p.Description == description &&
                p.Situation == situation &&
                p.ManufacturingDate == manufacturingDateParsed &&
                p.ExpirationDate == expirationDateParsed &&
                p.SupplierId == supplierId);
        }
    }
}
