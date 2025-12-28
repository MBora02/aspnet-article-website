using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Dto.FaqDtos
{
    public class CreateFaqDto
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
