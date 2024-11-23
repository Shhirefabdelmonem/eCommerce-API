using AutoMapper;
using eCommerceApp.Application.DTOs;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services.Interfaces;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Services.Implementations
{
    internal class CategoryService(IMapper mapper,IGeneric<Category>categoryInterface) : ICategoryService
    {
        public async Task<ServiceResponse> AddAsync(CreateCategoryDto categoryDto)
        {
           
            var mappedData= mapper.Map<Category>(categoryDto);
            int res = await categoryInterface.AddAsync(mappedData);
            return res > 0 ? new ServiceResponse(true, "Category Added") :
               new ServiceResponse(false, "Category failed to be Add");

        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int res = await categoryInterface.DeleteAsync(id);
            return res > 0 ? new ServiceResponse(true, "Category deleted") :
                new ServiceResponse(false, "Category failed to be deleted");
        }

        public async Task<IEnumerable<GetProductDto>> GetAllAsync()
        {
            var rawData=await categoryInterface.GetAllAsync();//List of Category
            if (!rawData.Any())
                return [];
            return mapper.Map<IEnumerable<GetProductDto>>(rawData);//<TDestination>(TSource)
        }

        public async Task<GetCategoryDto> GetByIdAsync(Guid id)
        {
            var rawData=await categoryInterface.GetByIdAsync(id);
            return mapper.Map<GetCategoryDto>(rawData);
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategoryDto categoryDto)
        {
            var mappedData= mapper.Map<Category>(categoryDto);
            var res= await categoryInterface.UpdateAsync(mappedData);
            return res > 0 ? new ServiceResponse(true, "Category Updated") :
                new ServiceResponse(false, "Category failed to be Update");

        }
    }
}
