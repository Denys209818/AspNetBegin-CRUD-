using AspNetCoreFinalApp.Domain;
using AspNetCoreFinalApp.Domain.Entities;
using AspNetCoreFinalApp.Models;
using AutoMapper;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Controllers
{
    [Authorize]
    public class CarController : Controller
    {
        public List<CarModelView> models { get; set; }
        public EFDataContext _context { get; set; }
        private IMapper _mapper { get; set; }
        public CarController(EFDataContext context, IMapper mapper)
        {
            _context = context;
            //CreateCars();
            _mapper = mapper;
            models = GetCars();


            #region Static fill

            //models = new List<CarModelView> {
            //    new CarModelView
            //    {
            //        Id = 1,
            //        Developer = "BMW",
            //        Model = "X6",
            //        Image = "bmwx6.jpg",
            //        Price = 2000000,
            //        Year = 2021
            //    },
            //    new CarModelView
            //    {
            //        Id = 2,
            //        Developer = "Mercedes",
            //        Model = "GLA",
            //        Image = "mercedesgla.jpg",
            //        Price = 1800000,
            //        Year = 2020
            //    },
            //};
            #endregion
        }
        public IActionResult Index()
        {
            models = GetCars()?.OrderBy(x => x.Id)?.ThenBy(x => x)?.ToList();
            if (models != null) 
            {
                return View(models);
            }
            return View(new List<CarModelView>());
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarCreateModelView carCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(carCreate);

                decimal price = decimal.Parse(carCreate.Price.Trim(' ', '₴'));

                _context.Cars.Add(new Domain.Entities.AppCar
                {
                    DateCreate = DateTime.Now,
                    Developer = carCreate.Developer,
                    Image = carCreate.Image,
                    Model = carCreate.Model,
                    Price = price,
                    Year = carCreate.Year
                });

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public IActionResult Edit(int id = -1) 
        {
            try
            {
                if (id != -1)
                {
                    var modal = models.FirstOrDefault(x => x.Id == id);
                    if (modal != null)
                    {
                        return View(new CarUpdateModelView
                        {
                            Id = id,
                            Developer = modal.Developer,
                            Image = modal.Image,
                            Model = modal.Model,
                            Price = modal.Price.ToString(),
                            Year = modal.Year
                        });
                    }
                }
                return View();
            }
            catch 
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Edit(CarUpdateModelView modal)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(modal);

                var car = _context.Cars.FirstOrDefault(x => x.Id == modal.Id);

                car.Image = modal.Image;
                car.Model = modal.Model;
                car.Developer = modal.Developer;
                car.Year = modal.Year;
                car.Price = decimal.Parse(modal.Price.Trim('₴').Trim(' '));

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch 
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Delete(int id) 
        {
            try
            {
                var car = _context.Cars.FirstOrDefault(x => x.Id == id);
                if (car != null)
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            catch 
            {
                return BadRequest();
            }
        }
        public List<CarModelView> GetCars() 
        {
            try
            {
                var obj = new List<CarModelView>(_mapper
                    .Map<List<CarModelView>>(_context.Cars.ToList()));

                return obj;
                //return new List<CarModelView>(_context.Cars.Select(x => new CarModelView
                //{
                //    Id = x.Id,
                //    Developer = x.Developer,
                //    Image = x.Image,
                //    Model = x.Model,
                //    Price = x.Price,
                //    Year = x.Year
                //}).ToList());
            }
            catch 
            {
                return null;
            }
        }
        private void CreateCars() 
        {
            Faker<AppCar> faker = new Faker<AppCar>("uk")
                    .RuleFor(x => x.Developer, f => f.PickRandomParam<string>("BMW", "Mercedes", "Audi", "Volvo", "Pegeuo", "Volkswagen"))
                    .RuleFor(x => x.Model, f => f.PickRandomParam<string>("X7", "GLA", "Q7", "X90", "Partner", "GOLF"))
                    .RuleFor(x => x.Year, f => f.Random.Int(2000, DateTime.Now.Year))
                    .RuleFor(x => x.Price, f => f.PickRandomParam<decimal>(2000000, 1500000, 1000000, 1300000, 1250000))
                    .RuleFor(x => x.Image, f => f.Image.LoremPixelUrl())
                    .RuleFor(x => x.DateCreate, DateTime.Now);

            for (int i = 0; i < 100; i++)
            {
                _context.Cars.Add(faker.Generate());
                _context.SaveChanges();
            }
        }
    }
}
