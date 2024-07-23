using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL;
using Project.Models.DTO;
using Project.Models;
using Project.DAL;
using System.Data;
using Microsoft.AspNetCore.Authorization;

namespace Project.Controllers
{
   [ ApiController]
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService CategoryS;
        private readonly IMapper mapper;

        public CategoryController(ICategoryService category, IMapper mapper)
        {
            this.mapper = mapper;
            this.CategoryS = category;
        }
       
        [HttpGet("getAll")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            try
            {
                return Ok(await CategoryS.GetAllCategories());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("getById")]
        [AllowAnonymous]

        public async Task<ActionResult<Category>> GetCategoryById(int Id)
        {
            try
            {
                return Ok(await CategoryS.GetCategoryById(Id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        //[HttpGet("getByName")]
        //[AllowAnonymous]
        //public async Task<ActionResult<Category>> GetCategoryByName(string name)
        //{
        //    try
        //    {
        //        return Ok(await CategoryS.GetCategoryByName(name));
        //    }
        //    catch (Exception)
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost("add")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Category>> AddCategory(CategoryDto categoryDto)
        {
            try
            {
                Category category = mapper.Map<Category>(categoryDto);
                return Ok(await CategoryS.AddCategory(category));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        [HttpDelete("delete")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Category>> DeleteCategory(int IdCategory)
        {
            try
            {
                return Ok(await CategoryS.DeleteCategory(IdCategory));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("updateDetails")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Category>> ChangeName(int categoryId,string newName)
        {
            try
            {
                return Ok(await CategoryS.ChangeName(categoryId, newName));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
