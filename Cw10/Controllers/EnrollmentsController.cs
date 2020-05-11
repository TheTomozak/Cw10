using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.DTOs;
using Cw10.DTOs.Requests;
using Cw10.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Cw10.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {

        private readonly IDbService _server;

        public IConfiguration Configuration { get; set; }

        public EnrollmentsController(IDbService context, IConfiguration configuration)
        {
            _server = context;
            Configuration = configuration;
        }


        [HttpPost]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            HelperRequests helperRequests = _server.EnrollStudent(request);

            switch (helperRequests.Number)
            {
                case 1:
                    return BadRequest("Studies not exist");
                case 2:
                    return BadRequest($"Student with {request.IndexNumber} exist");
                default:
                    return Ok(request);
            }
        }

        [HttpPost("promotions")]
        public IActionResult PromoteStudents(EnrollPromotionsRequest enrollPromotionsRequest)
        {
            _server.PromoteStudent(enrollPromotionsRequest);

            return Ok("Success with promotions");
        }

    }
}