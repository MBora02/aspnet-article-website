using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;

namespace ArticleWebsite.Application.Features.Mediator.Results.AppUserResults
{
    public class GetAppUserQueryResult
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public int AppRoleId { get; set; }
        public string AppRoleName { get; set; }
    }
}
