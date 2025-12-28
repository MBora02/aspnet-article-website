using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Domain.Entities
{
    public class AppUser
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }

        public ICollection<Article> Articles { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
