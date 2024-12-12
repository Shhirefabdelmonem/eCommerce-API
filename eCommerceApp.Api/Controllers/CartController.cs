using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartService) : ControllerBase
    {
        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(CheckoutDto checkout)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);
            var res=await cartService.Checkout(checkout);
            return res.Success ? Ok(res) : BadRequest(res);

        }

        [HttpPost("save-checkout")]
        public async Task<IActionResult>SaveCheckout(IEnumerable<CreateAchieve> achieves)
        {
            var res = await cartService.SaveCheckoutHistory(achieves);
            return res.Success? Ok(res) : BadRequest(res);
        }

    }
}
