﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.Exceptions
{
    public class ItemNorFoundException:Exception
    {
        public ItemNorFoundException(string message):base(message) 
        {
            
        }
    }
}
