using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Dto.AppUserDtos
{
    public class CreateAppUserDto
    {
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
