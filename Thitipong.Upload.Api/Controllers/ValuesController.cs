using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thitipong.Upload.Api.Controllers
{
    // todo example configuration

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IConfiguration con;

        public ValuesController(IConfiguration con)
        {
            this.con = con;
        }

        [HttpGet]
        public string GetPin()
        {
            return con["pin"];
        }
    }
}
