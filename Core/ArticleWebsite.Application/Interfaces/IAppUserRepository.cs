using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;

namespace ArticleWebsite.Application.Interfaces
{
    public interface IAppUserRepository
    {
        Task<List<AppUser>> GetByFilterAsync(Expression<Func<AppUser, bool>> filter);
        List<AppUser> GetAppUsersWithRelations();
        Task<AppUser> GetUserProfileAsync(int appUserId);

        
    }

}
