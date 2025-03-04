﻿using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services
{
    public interface IProductService
    {
        Task<IEnumerable<GetProductDto>> GetAllAsync();
        Task<GetProductDto> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateProductDto productDto);
        Task<ServiceResponse> UpdateAsync(UpdateProductDto productDto);
        Task<ServiceResponse> DeleteAsync(Guid id);
    } 
}
