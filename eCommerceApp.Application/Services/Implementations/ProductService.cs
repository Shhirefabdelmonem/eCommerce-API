using AutoMapper;
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
    public class ProductService(IGeneric<Product> productInterface,IMapper mapper) : IProductService
    {
        public async Task<ServiceResponse> AddAsync(CreateProductDto productDto)
        {
           
            var mappedData= mapper.Map<Product>(productDto);
            int res = await productInterface.AddAsync(mappedData);
            return res > 0 ? new ServiceResponse(true, "Product Added") :
               new ServiceResponse(false, "Product failed to be Add");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int res= await productInterface.DeleteAsync(id);
            return res>0? new ServiceResponse(true, "Product deleted"): 
                new ServiceResponse(false, "Product failed to be deleted");
            
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
           var rawData= await productInterface.GetAllAsync();
            if (!rawData.Any()) return [];
            
            return mapper.Map<IEnumerable<GetProductDto>>(rawData);//mapping from rawData(list of Categories to
                                                                   // IEnumrable<GetprductDto>)
        }

        public async Task<GetProductDto> GetByIdAsync(Guid id)
        {
            var rawData = await productInterface.GetByIdAsync(id);
            if (rawData==null) return new GetProductDto();
            
            return mapper.Map<GetProductDto>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateProductDto productDto)
        {
            var mappedData=mapper.Map<Product>(productDto);
            int res= await productInterface.UpdateAsync(mappedData);
            
            return res > 0 ? new ServiceResponse(true, "Product Updated") :
               new ServiceResponse(false, "Product failed to be Updated");


        }

        
    }
}
