// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FranceConnect.DataProvider.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebApi_Data_Provider_DotNet.Models;

namespace WebApi_Data_Provider_DotNet.Controllers
{
    public class BaseApiController : BaseController
    {
        public BaseApiController(ApplicationDbContext context) : base(context) { }

        protected ApplicationUser FindUser()
        {
            var email = HttpContext.Items["email"] as string;
            return _context.Users.SingleOrDefault(u => u.Email == email);
        }

        protected ObjectResult ReturnUserNotFound()
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return new ObjectResult(new Error
            {
                error = "user_not_found",
                message = "User not found"
            });
        }
    }
}
