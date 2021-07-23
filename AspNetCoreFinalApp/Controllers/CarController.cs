using AspNetCoreFinalApp.Domain;
using AspNetCoreFinalApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Controllers
{
    public class CarController : Controller
    {
        public List<CarModelView> models { get; set; }
        public EFDataContext _context { get; set; }

        public CarController(EFDataContext context)
        {
            _context = context;
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
            models = GetCars().OrderBy(x => x.Id).ThenBy(x => x).ToList();

            return View(models);
        }
        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CarCreateModelView carCreate)
        {
            if (!ModelState.IsValid)
                return View(carCreate);

            decimal price = decimal.Parse(carCreate.Price.Trim(' ', '₴'));

            _context.Cars.Add(new Domain.Entities.AppCar { 
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
        [HttpGet]
        public IActionResult Edit(int id = -1) 
        {
            if (id != -1) 
            {
                var modal = models.FirstOrDefault(x => x.Id == id);
                if (modal != null) 
                {
                    return View(new CarUpdateModelView { 
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

        [HttpPost]
        public IActionResult Edit(CarUpdateModelView modal)
        {
            if(!ModelState.IsValid)
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

        [HttpGet]
        public IActionResult Delete(int id = -1) 
        {
            if (id != -1) 
            {
                var car = _context.Cars.FirstOrDefault(x => x.Id == id);
                if (car != null) 
                {
                    _context.Cars.Remove(car);
                    _context.SaveChanges();
                    return View(new CarModelView { 
                        Id = car.Id,
                        Developer = car.Developer,
                        Model = car.Model,
                        Price = car.Price,
                        Image = car.Image,
                        Year = car.Year
                    });
                }
            }
            return View();
        }

        public List<CarModelView> GetCars() 
        {
            return new List<CarModelView>(_context.Cars.Select(x => new CarModelView
            {
                Id = x.Id,
                Developer = x.Developer,
                Image = x.Image,
                Model = x.Model,
                Price = x.Price,
                Year = x.Year
            }).ToList());
        }
    }
}
