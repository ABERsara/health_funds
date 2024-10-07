using BLL.Iservices;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        IAppointmentsService ips;
        public AppointmentsController(IAppointmentsService ips)
        {
            this.ips = ips;
        }
        [HttpGet]
        public async Task<List<AppointmentsDto>> GetAsync()
        {
            try
            {

                return await ips.SelectAllAsync();
            }
            catch (Exception ex) { throw; }
        }

        [HttpPost]
        public async Task AddAsync(DTO.AppointmentsDto p)
        {
            try
            {

                await ips.AddAsync(p);
            }
            catch (Exception ex) { throw; }

        }
    }
}
