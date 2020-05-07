using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.Models;
using Cw10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Cw10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentDbService _server;

        public IConfiguration Configuration { get; set; }

        public StudentsController(IStudentDbService context, IConfiguration configuration)
        {
            _server = context;
            Configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetStudentsList()
        {
            return Ok(_server.GetStudents());
        }
    }
}