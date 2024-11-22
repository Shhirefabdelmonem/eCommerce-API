using eCommerceApp.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs.Category
{
    public class GetCategoryDto:CategoryBaseDto
    {
        public Guid Id { get; set; }
        public ICollection<GetProductDto> ?Products { get; set; }
    }
}
