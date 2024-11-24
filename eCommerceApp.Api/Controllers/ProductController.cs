using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Application.Services;
using Microsoft.AspNetCore.Mvc;


namespace eCommerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var data =await productService.GetAllAsync();
            return data.Any() ? Ok(data) : NotFound(data);

        }

        [HttpGet("single/{id}")]
        public async Task<IActionResult> GetSingle(Guid id)
        {
            var data=await productService.GetByIdAsync(id);
            return data!=null ? Ok(data) : NotFound(data);

        }
        [HttpPost("add")]
        public async Task<IActionResult>Add(CreateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res=await productService.AddAsync(productDto);
            return res.Success ? Ok(res) : BadRequest(res);
        }
        [HttpPut("update")]
        public async Task<IActionResult>Update(UpdateProductDto productDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var res= await productService.UpdateAsync(productDto);
            return res.Success ? Ok(res) : BadRequest(res);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res= await productService.DeleteAsync(id);
            return res.Success ? Ok(res) :BadRequest(res);
        }
        

    }
}
