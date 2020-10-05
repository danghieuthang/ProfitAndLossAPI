using AutoMapper;
using ProfitAndLoss.WebApi.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfitAndLoss.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class BaseController : ControllerBase
    {
        private readonly IMapper _mapper;
        public BaseController(IMapper mapper)
        {
            _mapper = mapper;
            

    }
        [HttpGet]
        public IActionResult Get()
        {
            var user = new User()
            {
                Id = 1,
                Address = "hic",
                Email = "ema",
                FirstName = "hung",
                LastName = "z"
            };
            UserViewModel userViewModel = _mapper.Map<UserViewModel>(user);
            return Ok(userViewModel);
        }
}
// Source
public class User
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string Address { get; set; }
}

// Destination
public class UserViewModel
{
    public string _First_Name { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }
}
}
