using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerceApp.Application.DTOs.Category;

namespace eCommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoryDto>> GetAllAsync();
        Task<GetCategoryDto> GetByIdAsync(Guid id);
        Task<ServiceResponse> AddAsync(CreateCategoryDto categoryDto);
        Task<ServiceResponse> UpdateAsync(UpdateCategoryDto categoryDto);
        Task<ServiceResponse> DeleteAsync(Guid id);
    }
}
