using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;

namespace ArticleWebsite.Application.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviewByArticleIdAsync(int articleId);
        Task<List<Review>> GetReviewByAppUserIdAsync(int appUserId);
    }
}
