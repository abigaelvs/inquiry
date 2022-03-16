using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

using CNDS.SqlPaging;

using InqService.Model;
using InqService.Entity;
using InqService.Repository;

namespace InqService.Controllers
{
    [ApiController]
    [Route("")]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryRepository _inquiryRepository;
        public InquiryController(IInquiryRepository inquiryRepository)
        {
            _inquiryRepository = inquiryRepository;
        }

        [HttpPost("dologin")]
        public IActionResult GetDologin([FromBody] LoginRequest request)
        {
            LoginResponse result = _inquiryRepository.DoLogin(request);
            return Ok(result);
        }

        [HttpPost("selectUnit")]
        public IActionResult SelectUnit([FromBody] SelectUnitRequest request)
        {
            SelectUnitResponse result = _inquiryRepository.SelectUnit(request);
            return Ok(result);
        }
    }
}
