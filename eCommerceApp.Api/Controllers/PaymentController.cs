﻿using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerceApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController(IPaymentMethodService paymentMethodService) : ControllerBase
    {
        [HttpGet("payment-methods")]
        public async Task<ActionResult<IEnumerable<GetPaymentMethodDto>>> GetPaymentMethods()
        {
            var methods=await paymentMethodService.GetPaymentMethods();
            if (!methods.Any())
                return NotFound();
            else return Ok(methods);
        }


    }
}