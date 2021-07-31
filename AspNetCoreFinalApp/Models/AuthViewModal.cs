using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Models
{
    public class AuthViewModal
    {
        [Display(Name = "Логін")]
        public string Login { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        
        public string ReturnUrl { get; set; }
    }

    public class AuthValidator : AbstractValidator<AuthViewModal> 
    {
        public AuthValidator()
        {
            RuleFor(x => x.Login).NotEmpty()
                .WithMessage("Поле 'Логін' не може бути пустим!");
            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Поле 'Пароль' не може бути пустим!");
        }
    }
}
