// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FranceConnect.DataProvider.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Data_Provider_DotNet.Models;

namespace WebApi_Data_Provider_DotNet.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : BaseApiController
    {
        public ValuesController(ApplicationDbContext context) : base(context) { }

        // GET: api/values
        [HttpGet]
        [ConsentFilter(Scope = new string[] { "value1", "value2" })]
        public IActionResult Get()
        {
            var user = FindUser();
            if (user == null)
            {
                return ReturnUserNotFound();
            }
            return new ObjectResult(new
            {
                ValueOne = user.ValueOne,
                ValueTwo = user.ValueTwo
            });
        }
    }
}
