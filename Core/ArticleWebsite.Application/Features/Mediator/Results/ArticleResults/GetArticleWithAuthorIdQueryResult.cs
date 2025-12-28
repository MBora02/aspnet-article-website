using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Application.Features.Mediator.Results.ArticleResults
{
    public class GetArticleWithAuthorIdQueryResult
    {
        public int ArticleId { get; set; }
       

        public int AuthorId { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorName { get; set; }
        public string AuthorSurname { get; set; }

        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        

        public int TagCloudId { get; set; }
        public string TagCloudTitle { get; set; }
    }
}
