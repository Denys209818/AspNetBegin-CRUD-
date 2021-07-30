using AspNetCoreFinalApp.Domain;
using AspNetCoreFinalApp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Controllers
{
    public class TestController : Controller
    {
        public EFDataContext _context { get; set; }
        public IMapper _mapper { get; set; }
        public TestController(EFDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Delete(long id) 
        {
            var car = _context.Cars.FirstOrDefault(x => x.Id == id);

            CarModelView model = _mapper.Map<CarModelView>(car);

            return View();
        }

        [HttpGet]
        public IActionResult UserData() 
        {
            return Ok(JsonConvert.SerializeObject(new
            {
                Name = "Denys209818",
                Surname = "Kravchuk"
            }));
        }
    }
}
