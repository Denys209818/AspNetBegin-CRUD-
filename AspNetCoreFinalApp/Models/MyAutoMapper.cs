using AspNetCoreFinalApp.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreFinalApp.Models
{
    public class MyAutoMapper : Profile
    {
        public MyAutoMapper()
        {
            CreateMap<AppCar, CarModelView>()
                .ForMember(x => x.Image, m => m.MapFrom(x => x.Image))
                .ReverseMap();
        }
    }
}
