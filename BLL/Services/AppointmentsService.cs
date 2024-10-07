using BLL.Iservices;
using Dal_Repository.Irepository;
using Dal_Repository.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
namespace BLL.Services
{
    public class AppointmentsService : IAppointmentsService
    {
        Dal_Repository.Irepository.Irepository<Appointment> idal;
        IMapper mapper;
        public AppointmentsService(Irepository<Appointment> idal, IMapper mapper)
        {
            this.idal = idal;
            this.mapper = mapper;
        }

        public async Task AddAsync(AppointmentsDto p)
        {
            try
            {  //שתשלוף את הנתונים ממסד הנתוניםdal זימון פונקצית משכבת ה


                await idal.AddAsync(ToAppointment(p));
            }

            catch (Exception ex)
            { throw; }

        }
        public async Task<List<AppointmentsDto>> SelectAllAsync()
        {
            try
            {

                var q1 = await idal.SelectAllAsync();
                //convert with auto mapper: 
                return mapper.Map<List<Appointment>, List<DTO.AppointmentsDto>>(q1);

                //simple convert:
                //return ToAppointmentDtoList(q1);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task UpdateAsync(AppointmentsDto p, short id)
        {
            try
            {
                await idal.UpdateAsync(ToAppointment(p), id);
            }
            catch (Exception ex) { throw; }
        }

        public async Task DeleteAsync(short id)
        {
            try
            {

                await idal.DeleteAsync(id);
            }
            catch (Exception ex) { throw; }
        }

        //---------------פונקציות המרות ידניות------------
        private Dal_Repository.models.Appointment ToAppointment(DTO.AppointmentsDto p)
        {
            Dal_Repository.models.Appointment dal = new Dal_Repository.models.Appointment();
            dal.Patient = p.Patient;
            dal.Medicine = p.Medicine;
            dal.Doctor = p.Doctor;

            return dal;

        }

        private DTO.AppointmentsDto ToAppointmentDto(Dal_Repository.models.Appointment p)
        {
            DTO.AppointmentsDto dto = new DTO.AppointmentsDto();

            dto.Doctor = p.Doctor;
            dto.Patient = p.Patient;
            dto.Medicine = p.Medicine;


            //navigations fields:
            dto.DoctorName = p.Doctor != null ? p.DoctorNavigation.Name : "";
            dto.PatientName = p.Patient != null ? p.PatientNavigation.Name : "";
            dto.MedicineName = p.Medicine != null ? p.MedicineNavigation.Name : "";
            return dto;

        }

        private List<DTO.AppointmentsDto> ToAppointmentDtoList(List<Dal_Repository.models.Appointment> lp)
        {
            //DTOהמרת אוסף ממבנה מסד הנתונים לאוסף מסוג 
            List<DTO.AppointmentsDto> lpnew = new List<DTO.AppointmentsDto>();
            foreach (var item in lp)
            {
                lpnew.Add(ToAppointmentDto(item));
            }
            return lpnew;
        }

    }
}
