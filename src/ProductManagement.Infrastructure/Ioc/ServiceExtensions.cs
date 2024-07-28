using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Domain.Interfaces.Services;
using ProductManagement.Domain.Services;
using ProductManagement.Infrastructure.Data.Repositories;

namespace ProductManagement.Infrastructure.Ioc
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            #region Application
            #endregion

            #region Service
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            #endregion
        }
    }
}
