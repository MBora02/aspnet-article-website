using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Domain.Entities;

namespace ArticleWebsite.Application.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetLast3ArticleWithAuthorsAsync();
        public List<Article> GetArticleByAuthorId(int AuthorId);
        public List<Article> GetArticleByDepartmentId(int DepartmentId);
        public Task<List<Article>> GetArticleByTagCloudIdAsync(int tagCloudId);
        public List<Article> GetArticleWithAuthorId(int AuthorId);

        Task<List<Article>> GetArticleWithAllAsync();

        public Task<Article> GetArticleByIdAsync(int articleId);
        Task<List<Article>> GetPendingArticlesByDepartmentAsync(int departmentId);
    }
}
