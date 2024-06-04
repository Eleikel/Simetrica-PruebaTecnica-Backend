using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Common
{
    public class UserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int Id => Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("Sid")?.Value);

        public string? UserName => _httpContextAccessor.HttpContext?.User.FindFirst("FullName") != null
       ? _httpContextAccessor.HttpContext.User.FindFirst("FullName")?.Value
       : "";

    }
}
