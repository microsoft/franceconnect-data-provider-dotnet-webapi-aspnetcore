// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using FranceConnect.DataProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;

namespace FranceConnect.DataProvider.Filters
{
    public class ConsentFilter : ActionFilterAttribute
    {
        public string[] Scope { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var receivedScope = context.HttpContext.Items["scope"] as string[];

            if (receivedScope == null || !Scope.All(scope => receivedScope.Contains(scope)))
            {
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.HttpContext.Response.ContentType = "application/json";
                context.Result = new JsonResult(new Error
                {
                    error = "invalid_scope",
                    message = "The request does not contain required scopes"
                });
            }
        }
    }
}
