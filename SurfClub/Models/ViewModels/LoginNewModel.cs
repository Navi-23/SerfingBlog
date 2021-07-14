using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SurfClub.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Псевдоним обязательный")]
        [MaxLength(20), MinLength(3, ErrorMessage = "Минимимальная длина псевдонима 3 символа")]

        public String Nickname { get; set; }


        [Required(ErrorMessage = "Пароль обязательный")]
        [MaxLength(20), MinLength(3,ErrorMessage ="Минимимальная длина пароля 3 символа")]
        public String Password { get; set; }


        public bool RememberMe { get; set; }

    }
}
