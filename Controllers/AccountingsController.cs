using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApi.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BankApi.Services;

namespace BankApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingsController : ControllerBase
    {
        
        public AccountService _accountService;
        public AccountingsController(AccountService accountService)
        {
            _accountService = accountService;
        }
           
        [HttpGet]
        public IActionResult GetAllAccountings()
        {
            var allAccountings = _accountService.GetAllAccountings();
            return Ok(allAccountings);
        }
        [HttpGet("{id}")]
        public IActionResult GetAccountingById(int id)
        {
            var accounting = _accountService.GetAccountingById(id);
            return Ok(accounting);
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountingVM accounting) 
        {
            _accountService.AddAccounting(accounting);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAccountingById(int id, [FromBody]AccountingVM accounting)
        {
            var updatedAccounting = _accountService.UpdateAccountingById(id, accounting);
            return Ok(updatedAccounting);
        }
          
    }
}
