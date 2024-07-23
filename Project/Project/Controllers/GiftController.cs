using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.DAL;
using Project.Models;
using Project.Models.DTO;
using System.Data;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

//https://primereact.org/datatable/#sort
namespace Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GiftController : Controller
    {
        private readonly IGiftService GiftS;
        private readonly IMapper mapper;
        private readonly IHostingEnvironment env;

        public GiftController(IGiftService gift, IMapper mapper, IHostingEnvironment env)
        {
            this.mapper = mapper;
            this.GiftS = gift;
            this.env = env;
        }

        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Gift>>> GetAllGifts()
        {
            try
            {
                return Ok(await GiftS.GetAllGifts());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getAllNo")]
        [Authorize(Roles ="Manager")]
        public async Task<ActionResult<List<Gift>>> GetAllNoGifts()
        {
            try
            {
                return Ok(await GiftS.GetAllNoGifts());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{image}")]
        [AllowAnonymous]
        public IActionResult GetImage(string image)
        {
            if (image == null)
            {
                return NotFound();
            }

            var imagePath = $"wwwroot/Images/{image}";

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg"); // You can adjust the content type based on the image type
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("getByDonor")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Gift>>> GetGiftsByDonor(int DonorId)
        {
            try
            {
                return Ok(await GiftS.GetGiftsByDonor(DonorId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = "Manager,Custumer,Donor")]
        public async Task<ActionResult<Gift>> AddGift([FromForm] GiftWithImageDto giftWithImageDto)
        {
            try
            {
                var file = giftWithImageDto.Image;
                if (file != null && file.Length > 0)
                {
                    // Generate a unique filename
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = Path.GetExtension(file.FileName);
                    var newFileName = $"{fileName}_{Guid.NewGuid()}{extension}";

                    // Get the path to the wwwroot/images directory
                    var filePath = Path.Combine(env.WebRootPath, "Images", newFileName);
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Save the filename in the database
                    var giftDto = new GiftDto
                    {
                       CustomerId = giftWithImageDto.CustomerId,
                       Price = -1,
                       Name = giftWithImageDto.Name,
                       Description = giftWithImageDto.Description,
                       CategoryId = 51,
                       Image = newFileName
                        // Add other properties as necessary
                    };
                    Gift gift = mapper.Map<Gift>(giftDto);
                    return Ok(await GiftS.AddGift(gift));
                    // return Ok(newFileName);
                }
                else
                {
                    throw new InvalidOperationException("Must put Image");
                }
                // string path = env.WebRootFileProvider.GetFileInfo("Images/yellow_flower.png")?.PhysicalPath;


            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("delete")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Gift>> DeleteGift(int IdGift)
        {
            try
            {
                return Ok(await GiftS.DeleteGift(IdGift));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("updateDetails")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Gift>> ChangeDetails(int IdGift, GiftDto giftDto)
        {
            try
            {
                Gift gift = mapper.Map<Gift>(giftDto);
                return Ok(await GiftS.ChangeDetails(IdGift, gift));
            }
            catch (Exception)   
            {
                return BadRequest();
            }
        }

        [HttpPut("ChangeStatusT")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Gift>> ChangeStatusT(ChangeGiftStatusDto changeGiftStatusDto)
        {
            try
            {
                return Ok(await GiftS.ChangeStatusT(changeGiftStatusDto));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

