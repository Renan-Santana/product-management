using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductManagement.Application;
using ProductManagement.Application.DTOs.Request;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Mappers;
using ProductManagement.Application.Validators;
using ProductManagement.Domain.Interfaces.Repositories;
using ProductManagement.Domain.Interfaces.Services;
using ProductManagement.Domain.Services;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Data.Repositories;
using ProductManagement.Presentation.Middlewares;

namespace ProductManagement.Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductManagement.Presentation", Version = "v1" });
            });

            #region Application
            services.AddScoped<IProductApplication, ProductApplication>();
            services.AddScoped<ISupplierApplication, SupplierApplication>();
            #endregion

            #region Service
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ISupplierService, SupplierService>();
            #endregion

            #region Repositories
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            #endregion

            #region Validator
            services.AddScoped<IValidator<ProductRequestDto>, ProductValidator>();
            services.AddScoped<IValidator<SupplierRequestDto>, SupplierValidator>();
            #endregion

            services.AddDbContext<ProductManagementContext>(options => options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionString")));
            services.AddAutoMapper(typeof(MappingProfile));
            
            services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement.Presentation v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
