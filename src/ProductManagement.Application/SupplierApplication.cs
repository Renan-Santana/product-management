using System;
using System.Linq;
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
    public class SupplierApplication : ISupplierApplication
    {
        private readonly IValidator<SupplierRequestDto> _validator;
        private readonly IMapper _mapper;
        private readonly ISupplierService _supplierService;

        public SupplierApplication(IValidator<SupplierRequestDto> validator, IMapper mapper, ISupplierService supplierService)
        {
            _validator = validator;
            _mapper = mapper;
            _supplierService = supplierService;
        }

        public BaseResponseDto Add(SupplierRequestDto supplier)
        {
            try
            {
                var validationResult = _validator.Validate(supplier);

                if (!validationResult.IsValid)
                {
                    return new()
                    {
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                        StatusCode = HttpStatusCode.BadRequest
                    };
                }

                var supplierEntity = _mapper.Map<Supplier>(supplier);

                _supplierService.Add(supplierEntity);

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
    }
}
