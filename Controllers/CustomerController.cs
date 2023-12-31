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
        public IActionResult GetCustomer([FromBody] CustomerRequest customer)
        {
            CustomerResponse result = _customerRepository.GetCustomer(customer);
            return Ok(result);
        }

        [HttpPost("all")]
        public IActionResult GetCustomers([FromBody] StandardMessage customer)
        {
            CustomerResponse result = _customerRepository.GetCustomers(customer);
            return Ok(result);
        }
    }
}
