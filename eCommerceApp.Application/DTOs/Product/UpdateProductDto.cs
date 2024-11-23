using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs.Product
{
    public class UpdateProductDto:ProductBaseDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
