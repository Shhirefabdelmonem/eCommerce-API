using eCommerceApp.Application.DTOs.Category;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCommerceApp.Application.DTOs.Product
{
    public class GetProductDto:ProductBaseDto
    {
        [Required]
        public Guid Id { get; set; }
        public GetCategoryDto ?Category { get; set; }
    }
}
