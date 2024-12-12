using AutoMapper;
using eCommerceApp.Application.DTOs.Cart;
using eCommerceApp.Application.DTOs.Category;
using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.DTOs.Product;
using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Entities.Cart;
using eCommerceApp.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Mapping
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<CreateProductDto, Product>();

            CreateMap<Category, GetCategoryDto>();
            CreateMap<Product,GetProductDto>();
            CreateMap<CreateUser, AppUser>();
            CreateMap<LoginUser, AppUser>();
            
            CreateMap<PaymentMethod,GetPaymentMethodDto>();
            CreateMap<CreateAchieve,Achieve>();
        
        }
    }
}
