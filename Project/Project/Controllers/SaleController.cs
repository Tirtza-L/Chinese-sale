using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Models.DTO;
using Project.Models;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
    public class SaleController : Controller
    {
            
        private readonly ISaleService SaleS;
        private readonly IMapper mapper;

        public SaleController(ISaleService Sale, IMapper mapper)
        {
            this.mapper = mapper;
            this.SaleS = Sale;
        }

        //to the manager
        [HttpGet("getAllByStatusT")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Sale>>> GetAllSalesByStatusT()
        {
            try
            {
                return Ok(await SaleS.GetAllSalesByStatusT());
            }
            catch (Exception)
            {
                return BadRequest();
            }           
        }

        [HttpGet("getByCustomer")]
        [Authorize(Roles = "Manager,Custumer,Donor")]
        public async Task<ActionResult<List<Sale>>> GetSaleByCustomer(int CustomerId)
        {
            try
            {
                return Ok(await SaleS.GetSaleByCustomer(CustomerId));
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        [HttpGet("getByGift")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Sale>>> GetSaleByGift(int GiftId)
        {
            try
            {
                return Ok(await SaleS.GetSaleByGift(GiftId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("getById")]
        public async Task<ActionResult<Sale>> GetSaleById(int SaleId)
        {
            try
            {
                return Ok(await SaleS.GetSaleById(SaleId));
            }
            catch (Exception)
            {
                return BadRequest();
            }           
        }

        [HttpPost("addSale")]
        [Authorize(Roles = "Custumer,Manager,Donor")]
        public async Task<ActionResult<Sale>> AddSale([FromBody]SaleDto SaleDto)
        {
            try
            {
                Sale Sale = mapper.Map<Sale>(SaleDto);
                return Ok(await SaleS.AddSale(Sale));
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        //remember!!! the garbage icon only to the status false!!!
        [HttpDelete("deleteSale")]
        [Authorize(Roles = "Manager,Custumer,Donor")]
        public async Task<ActionResult<Sale>> DeleteSale(int IdSale)
        {
            try
            {
                return  Ok(await SaleS.DeleteSale(IdSale));
            }
            catch (Exception)
            {
                return BadRequest();
            }            
        }

        [HttpPut("ChangeToStatusT")]
        [Authorize(Roles = "Manager,Custumer,Donor")]
        public async Task<ActionResult<Sale>> ChangeToStatusT(int IdSale)
        {
            try
            {
                return Ok(await SaleS.ChangeToStatusT(IdSale));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("random")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Customer>> Random(int giftId)
        {
            try
            {
                return Ok(await SaleS.Random(giftId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
