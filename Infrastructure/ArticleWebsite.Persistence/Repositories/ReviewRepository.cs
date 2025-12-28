using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using ArticleWebsite.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Persistence.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ArticleWebsiteContext _context;

        public ReviewRepository(ArticleWebsiteContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetReviewByAppUserIdAsync(int appUserId)
        {
            return await _context.Reviews
                .Include(r => r.Article) 
                .Include(r => r.Reviewer) 
                .Where(r => r.ReviewerId == appUserId)
                .ToListAsync();
        }

        public async Task<List<Review>> GetReviewByArticleIdAsync(int articleId)
        {
            return await _context.Reviews
                .Include(r => r.Reviewer)
                .Include(r => r.Article)
                .Where(x => x.ArticleId == articleId)
                .ToListAsync();
        }
    }
}
