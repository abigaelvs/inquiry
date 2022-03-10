﻿using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;

using CNDS.SqlPaging;

using InqService.Model;
using InqService.Entity;
using InqService.Repository;

namespace InqService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpPost]
        public IActionResult GetCustomers([FromBody] SQLPage page)
        {
            CustomerResponse result = _customerRepository.GetCustomers(page);
            return Ok(result);
        }
    }
}
