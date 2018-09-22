using Instant.APIModels;
using Instant.EFData;
using Instant.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Instant.Controllers
{
    [Route("")]
    public partial class InstantController : Controller
    {
        private readonly IAccountService accountService;

        public InstantController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpGet("[action]")]
        public CardAccount giveMeCard([FromQuery] double amount, string id)
        {
            return accountService.ProvisionCardAccount(id, amount);
        }

        [HttpGet("[action]")]
        public IActionResult cardSpent([FromQuery] string cardNumber)
        {
            var amount = 0.0;

            // TODO: Exception Filters to handle bad arguments and such
            this.accountService.UpdateCardAccount(Convert.ToInt64(cardNumber), amount);
            return Ok();
        }

        [HttpGet("[action]")]
        public IList<CardAccountSummary> accounts()
        {
            return this.accountService.GetAccountSummaries();
        }
    }
}
