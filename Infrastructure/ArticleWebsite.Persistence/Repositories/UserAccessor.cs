using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ArticleWebsite.Persistence.Repositories
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetUserDepartmentId()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var claim = user?.FindFirst("departmentId");
            return int.TryParse(claim?.Value, out int departmentId) ? departmentId : throw new Exception("Department ID not found.");
        }
    }
}