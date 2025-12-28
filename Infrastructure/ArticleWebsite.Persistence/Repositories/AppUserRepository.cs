using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using ArticleWebsite.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Persistence.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ArticleWebsiteContext _context;

        public AppUserRepository(ArticleWebsiteContext context)
        {
            _context = context;
        }


        public List<AppUser> GetAppUsersWithRelations()
        {
            return _context.AppUsers
                .Include(u => u.Department)
                .Include(u => u.AppRole)
                .ToList();
        }


        public async Task<List<AppUser>> GetByFilterAsync(Expression<Func<AppUser, bool>> filter)
        {
            throw new NotImplementedException();
        }
        public async Task<AppUser> GetUserProfileAsync(int appUserId)
        {
            return await _context.AppUsers
                .Include(u => u.Department)
                .Include(u => u.AppRole)
                .FirstOrDefaultAsync(u => u.AppUserId == appUserId);
        }

        

    }
}
