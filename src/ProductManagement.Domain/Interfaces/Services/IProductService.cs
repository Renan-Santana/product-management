﻿using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;

namespace ProductManagement.Domain.Interfaces.Services
{
    public interface IProductService
    {
        void Add(Product product);
        void Update(Product product);
        void SoftDelete(Product product);
        Product Get(int id);
        (IEnumerable<Product> pageProducts, int totalProducts) GetList(int pageNumber, int pageSize, Expression<Func<Product, bool>>? predicate = null);
    }
}
