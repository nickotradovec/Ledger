using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using AppDatabase;
using Ledger.DataObjects;

namespace Ledger.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {   
        [HttpGet("[action]")]
        public async Task<IActionResult> AccountsList()
        {
            using (var db = new AppDb())
            {
                await db.Connection.OpenAsync();
                var query = new AccountQuery(db);
                var result = await query.GetAllAccounts();
                return new OkObjectResult(result);
            }
        }
    }
}
