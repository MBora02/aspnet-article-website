using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArticleWebsite.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;


namespace ArticleWebsite.Persistence.Repositories
{
    public class FileService : IFileService
    {
        private readonly string _uiRootPath = @"C:\Users\mbcmu\Desktop\Bitirme\ArticleWebsite\Frontends\ArticleWebsite.WebUI\wwwroot";

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var folderPath = Path.Combine(_uiRootPath, folderName);

            Directory.CreateDirectory(folderPath); // klasör yoksa oluştur

            var fullPath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/{folderName.Replace("\\", "/")}/{fileName}";
        }
    }
}
