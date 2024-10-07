using AutoMapper;
using Dal_Repository.models;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class MyMapper : Profile
    {
          public MyMapper()
            {
                CreateMap<Appointment, AppointmentsDto>().ReverseMap();
               
                CreateMap<AppointmentsDto, Appointment>();
            }
    }
}
