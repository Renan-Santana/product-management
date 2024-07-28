using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Domain.Interfaces.Services;

namespace ProductManagement.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Add(Product product)
        {
            _productRepository.Insert(product);
        }

        public void Remove(int id)
        {
            _productRepository.Delete(id);
        }

        public void Update(Product product)
        {
            _productRepository.Update(product);
        }

        public Product Get(int id)
        {
            var product = _productRepository.GetById(id);
            return product;
        }

        public (IEnumerable<Product> pageProducts, int totalProducts) GetList(int pageNumber, int pageSize, Expression<Func<Product, bool>> predicate = null)
        {
            var (pageProducts, totalProducts) = predicate != null
                ? _productRepository.GetByFilter(predicate, pageNumber, pageSize)
                : _productRepository.GetAll(pageNumber, pageSize);

            return (pageProducts, totalProducts);
        }
    }
}
