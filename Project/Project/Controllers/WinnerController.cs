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
    public class WinnerController : Controller
    {
        private readonly IWinnerService IWinnerS;
        private readonly IMapper mapper;

        public WinnerController(IWinnerService winner, IMapper mapper) 
        { 
            this.IWinnerS=winner; 
            this.mapper = mapper;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Winner>>> GetAllWinners()
        {
            try
            {
                return Ok(await IWinnerS.GetAllWinners());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getByGift")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer>> GetWinnerByGift(int giftId)
        {
            try
            {
                return Ok(await IWinnerS.GetWinnerByGift(giftId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("addWinner")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Winner>> AddWinner(WinnerDto winner)
        {
            try
            {
                Winner w = mapper.Map<Winner>(winner);

                return Ok(await IWinnerS.AddWinner(w));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteAll")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Winner>>> DeleteAllWinners()
        {
            try
            {
                return Ok(await IWinnerS.DeleteAll());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
