using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceApp.Application.DTOs
{
    public class ServiceResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; } = null!;

        public ServiceResponse() { }

        public ServiceResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }


}
