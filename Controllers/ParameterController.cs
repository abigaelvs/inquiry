using Microsoft.AspNetCore.Mvc;

using System;
using System.Text.Json;
using System.Collections.Generic;

using CNDS.SqlPaging;

using InqService.Entity;
using InqService.Repository;

namespace InqService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParameterController : ControllerBase
    {
        [HttpPost]
        public IActionResult GetParam([FromQuery] SQLPage page,
            [FromBody] JsonElement requestBody)
        {
            List<ParameterLevel1> result = ParameterRepository.GetPageParameterLvl1(page);
            return Ok(result);
        }
    }
}
