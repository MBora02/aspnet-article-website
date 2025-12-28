using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Interfaces;
using ArticleWebsite.Domain.Entities;
using ArticleWebsite.Persistence.Context;

namespace ArticleWebsite.Persistence.Repositories
{
    public class TagCloudRepository : ITagCloudRepository
    {
        private readonly ArticleWebsiteContext _context;

        public TagCloudRepository(ArticleWebsiteContext context)
        {
            _context = context;
        }

        
    }
}
