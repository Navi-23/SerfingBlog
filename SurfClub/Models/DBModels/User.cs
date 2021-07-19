using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SurfClub.Models.dbModels
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Псевдоним обязательный")]
        [MaxLength(20), MinLength(3, ErrorMessage = "Минимимальная длина псевдонима 3 символа")]
        public String Nickname { get; set; }

        [Required(ErrorMessage = "E-mail обязательный")]
        [MaxLength(31, ErrorMessage = "Максимальная длина e-mail 31 символ")]
        [EmailAddress(ErrorMessage ="Почта не шаблонного вида")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Пароль обязательный")]
        [MaxLength(20), MinLength(6, ErrorMessage = "Минимимальная длина пароля 6 символа")]
        public String Password { get; set; }

        [NotMapped]
        public String ConfirmPassword { get; set; }

        [MaxLength(31, ErrorMessage = "Максимальная длина пароля 31 символ")]
        public String Surname { get; set; }

        [MaxLength(31, ErrorMessage = "Максимальная длина пароля 31 символ")]
        public String Name { get; set; }

        [MaxLength(255, ErrorMessage = "Максимальная длина пароля 255 символ")]
        public String Contact { get; set; }

        [MaxLength(255, ErrorMessage = "Максимальная длина пароля 255 символ")]
        public String AboutMe { get; set; }

        [MaxLength(255, ErrorMessage = "Максимальная длина пароля 255 символ")]
        public String Progress { get; set; }
        public Guid? Photo { get; set; }
    }
}
