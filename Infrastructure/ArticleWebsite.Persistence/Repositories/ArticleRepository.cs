using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using ArticleWebsite.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ArticleWebsite.Persistence.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleWebsiteContext _context;

        public ArticleRepository(ArticleWebsiteContext context)
        {
            _context = context;
        }

        public List<Article> GetArticleByAuthorId(int AuthorId)
        {
            var values = _context.Articles.Include(x => x.Author)
        .Include(x => x.Department)
        .Include(x => x.Status)
        .Include(x => x.TagCloud)
        .Where(x => x.AuthorId == AuthorId)
        .ToList();
            return values;
        }

        public List<Article>GetArticleWithAuthorId(int Id)
        {
            var values = _context.Articles.Include(x => x.Author)
        .Include(x => x.Department)
        .Include(x => x.Status)
        .Include(x => x.TagCloud)
        .Where(x => x.ArticleId == Id)
        .ToList();
            return values;
        }

        public List<Article> GetArticleByDepartmentId(int departmentId)
        {
            var values = _context.Articles
        .Include(x => x.Author)
        .Include(x => x.Department)
        .Include(x => x.Status)
        .Include(x => x.TagCloud)
        .Where(x => x.DepartmentId == departmentId)
        .ToList();

            return values;
        }

        public async Task<Article> GetArticleByIdAsync(int articleId)
        {
            return _context.Articles
        .Include(x => x.Author)
        .Include(x => x.Department)
        .Include(x => x.Status)
        .Include(x => x.TagCloud)
        .FirstOrDefault(x => x.ArticleId == articleId);
        }

        public async Task<List<Article>> GetArticleByTagCloudIdAsync(int TagCloudId)
        {
            return await _context.Articles
                .Include(x => x.Author)
               .Include(x => x.Department)
               .Include(x => x.Status)
               .Include(x => x.TagCloud)
               .Where(x => x.TagCloudId == TagCloudId)
               .ToListAsync();

        }

        public async Task<List<Article>> GetArticleWithAllAsync()
        {
            return await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.Department)
                .Include(a => a.Status)
                .Include(a => a.TagCloud)
                .ToListAsync();
        }


        public async Task<List<Article>> GetLast3ArticleWithAuthorsAsync()
        {
            return await _context.Articles
        .Include(x => x.Author)
        .Include(x => x.Department)
        .OrderByDescending(x => x.ArticleId)
        .Take(3)
        .ToListAsync();
        }

        public async Task<List<Article>> GetPendingArticlesByDepartmentAsync(int departmentId)
        {
            return await _context.Articles
            .Include(a => a.Author)
            .Include(a => a.Department)
            .Include(a => a.Status)
            .Include(a => a.TagCloud)
            .Where(a => a.StatusId == 2 && a.DepartmentId == departmentId)
            .ToListAsync();
        }
    }
}
