using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Models.DTO;
using Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DonorController : Controller
    {
        private readonly IDonorService DonorS;
        private readonly IMapper mapper;

        public DonorController(IDonorService donor, IMapper mapper)
        {
            this.mapper = mapper;
            this.DonorS = donor;
        }


        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Customer>>> GetAllDonors()
        {
            try
            {
                return Ok(await DonorS.GetAllDonors());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<Customer>> DeleteDonor(int IdDonor)
        {
            //להעביר ללקוח חזרה
            try
            {
                return Ok(await DonorS.DeleteDonor(IdDonor));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("updateDetails")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<Customer>> ChangeDetails(DonorDto donorDto)
        {
            try
            {
                return Ok(await DonorS.ChangeDetails(donorDto));
            }
            catch (Exception)
            {
                return BadRequest();
            }     
        }

        [HttpPut("ChangeToDonor")]
        [Authorize(Roles = "Manager,Donor")]
        public async Task<ActionResult<Customer>> ChangeToDonor(int IdDonor)
        {
            try
            {
                return Ok(await DonorS.ChangeToDonor(IdDonor));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getByGift")]

        public async Task<ActionResult<Customer>> GetDonorByGift(int GiftId)
        {
            try
            {
                return Ok(await DonorS.GetDonorByGift(GiftId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
