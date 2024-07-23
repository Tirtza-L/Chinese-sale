using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.BLL;
using Project.Models;
using Project.Models.DTO;
using System.CodeDom.Compiler;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService customerS;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;


        public CustomerController(ICustomerService customer, IMapper mapper, IConfiguration configuration)
        {
            this.mapper = mapper;
            this.customerS = customer;
            this.configuration = configuration; 
         
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer>> Login(loginDto a)
        {
            Console.WriteLine("123");
            try 
	        
            {
                var user = await customerS.Login(a.name, a.password);
                if (user!=null)
                {
                    var token = Generate(user);
                    //var aaa = GetCustomerByToken();
                    return Ok(token);

                }
                return NotFound("user not found!");
            }
            catch (Exception e)
	        {
                return BadRequest(e);
            }
          
        }


        private string Generate(Customer user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {    
                //*****************
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                //*******************
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.StreetAddress, user.Address),
                new Claim(ClaimTypes.OtherPhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);


        }

        [HttpPost("addCustomer")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerDto customerDto)
        {
            try
            {
                Customer customer = mapper.Map<Customer>(customerDto);
                 return Ok (await customerS.AddCustomer(customer));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getAllCustomer")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                return Ok(await customerS.GetAllCustomers());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("deleteCustomer")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int IdCustomer)
        {
            try
            {
                return Ok(await customerS.DeleteCustomer(IdCustomer));

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Authorize(Roles = "Manager")]

        public async Task<ActionResult<Customer>> ChangeDetails(int IdCustomer,CustomerDto customerDto)
        {
            try
            {
                Customer customer = mapper.Map<Customer>(customerDto);

                return Ok(await customerS.ChangeDetails(IdCustomer, customer));
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }


        [HttpGet("getCustomerById")]
        [Authorize(Roles = "Manager")]
        public async Task<ActionResult<Customer>> GetCustomerById(int CustomerId)
        {
            try
            {
                return Ok(await customerS.GetCustomerById(CustomerId));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
    

       
        
    
}
