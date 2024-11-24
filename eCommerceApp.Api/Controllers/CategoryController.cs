using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services;
using eCommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        
            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
                var data = await categoryService.GetAllAsync();
                return data.Any() ? Ok(data) : NotFound(data);

            }

            [HttpGet("single/{id}")]
            public async Task<IActionResult> GetSingle(Guid id)
            {
                var data = await categoryService.GetByIdAsync(id);
                return data !=null ? Ok(data) : NotFound(data);

            }
            [HttpPost("add")]
            public async Task<IActionResult> Add(CreateCategoryDto categoryDto)
            {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
                var res = await categoryService.AddAsync(categoryDto);
                return res.Success ? Ok(res) : BadRequest(res);
            }
            [HttpPut("update")]
            public async Task<IActionResult> Update(UpdateCategoryDto categoryDto)
            {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res = await categoryService.UpdateAsync(categoryDto);
                return res.Success ? Ok(res) : BadRequest(res);
            }

            [HttpDelete("delete/{id}")]
            public async Task<IActionResult> Delete(Guid id)
            {
                var res = await categoryService.DeleteAsync(id);
                return res.Success ? Ok(res) : BadRequest(res);
            }


        
    }
}
