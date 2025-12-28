using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticleWebsite.Dto.RegisterDto
{
    public class CreateRegisterDto
    {
        [Required(ErrorMessage = "Ad boş geçilemez")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyad boş geçilemez")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email boş geçilemez")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        public string Password { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir bölüm seçiniz")]
        public int DepartmentId { get; set; }
    }
}

