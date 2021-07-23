using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Models
{
    public class CarModelView
    {
        public int Id { get; set; }
        public string Developer { get; set; }
        public string Model { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Year { get; set; }
    }

    public class CarCreateModelView
    {
        [Display(Name ="Виробник")]
        public string Developer { get; set; }
        [Display(Name = "Модель")]
        public string Model { get; set; }
        [Display(Name = "Фотографія")]
        public string Image { get; set; }
        [Display(Name = "Ціна")]
        public string Price { get; set; }
        [Display(Name ="Рік випуску")]
        public int Year { get; set; }
    }

    public class CarCreateValidator : AbstractValidator<CarCreateModelView> 
    {
        public CarCreateValidator()
        {
            RuleFor(x => x.Developer).NotEmpty().WithMessage("Поле \"Виробник\" не коректне!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Поле \"Модель\" не коректне!");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Поле \"Фотографія\" не коректне!");
            RuleFor(x => x.Price).NotEmpty().MinimumLength(3).WithMessage("Поле \"Ціна\" не коректне!");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Поле \"Рік випуску\" не коректне!");
            RuleFor(x => x.Year).Must(x => { return CorectYear(x);}).WithMessage("Рік не допустимий!");
        }

        public bool CorectYear(int year) 
        {
            return year >+ 1000 && year <= 9999;
        }
    }

    //Edit

    public class CarUpdateModelView
    {
        public int Id { get; set; }
        [Display(Name = "Виробник")]
        public string Developer { get; set; }
        [Display(Name = "Модель")]
        public string Model { get; set; }
        [Display(Name = "Фотографія")]
        public string Image { get; set; }
        [Display(Name = "Ціна")]
        public string Price { get; set; }
        [Display(Name = "Рік випуску")]
        public int Year { get; set; }
    }

    public class CarUpdateValidator : AbstractValidator<CarUpdateModelView>
    {
        public CarUpdateValidator()
        {
            RuleFor(x => x.Developer).NotEmpty().WithMessage("Поле \"Виробник\" не коректне!");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Поле \"Модель\" не коректне!");
            RuleFor(x => x.Image).NotEmpty().WithMessage("Поле \"Фотографія\" не коректне!");
            RuleFor(x => x.Price).NotEmpty().MinimumLength(3).WithMessage("Поле \"Ціна\" не коректне!");
            RuleFor(x => x.Year).NotEmpty().WithMessage("Поле \"Рік випуску\" не коректне!");
            RuleFor(x => x.Year).Must(x => { return CorectYear(x); }).WithMessage("Рік не допустимий!");
        }

        public bool CorectYear(int year)
        {
            return year > +1000 && year <= 9999;
        }
    }
    
}
