using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations
{
    internal class ProductService(IGeneric<Product> productInterface) : IProductService
    {
        public Task<ServiceResponse> AddAsync(CreateProductDto productDto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CreateProductDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<GetProductDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(UpdateProductDto productDto)
        {
            throw new NotImplementedException();
        }
    }
}
