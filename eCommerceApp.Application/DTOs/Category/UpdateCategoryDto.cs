﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs.Category
{
    public class UpdateCategoryDto:CategoryBaseDto
    {
        [Required]
        public Guid Id { get; set; }
    }
}
