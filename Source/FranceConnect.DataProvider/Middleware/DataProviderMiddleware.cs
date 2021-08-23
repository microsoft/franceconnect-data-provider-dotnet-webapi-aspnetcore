// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using FranceConnect.DataProvider.Models;
using System.Text;
using System.Text.Json;

namespace FranceConnect.DataProvider.Middleware
{
    public class DataProviderMiddleware
    {
        private readonly RequestDelegate _next;

        public DataProviderMiddleware(RequestDelegate next, DataProviderOptions options)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.ChecktokenEndpoint == null)
            {
                throw new ArgumentNullException(nameof(options.ChecktokenEndpoint));
            }

            _next = next;
            Options = options;
        }

        public DataProviderOptions Options { get; set; }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Value.StartsWith("/api"))
            {
                await _next(context);
                return;
            }

            string authorization = context.Request.Headers["Authorization"];
            string token = string.Empty;

            if (string.IsNullOrEmpty(authorization))
            {
                await InvalidAuthorizationHeader(context);
                return;
            }

            if (authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                token = authorization.Substring("Bearer ".Length).Trim();
            }

            if (string.IsNullOrEmpty(token))
            {
                await AccessTokenNotFound(context);
                return;
            }

            var client = new HttpClient();
            var httpContent = new
            {
                token = token
            };
            var response = await client.PostAsync(Options.ChecktokenEndpoint, new StringContent(JsonSerializer.Serialize(httpContent), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var checktokenResponse = JsonSerializer.Deserialize<ChecktokenResponse>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                context.Items["scope"] = checktokenResponse.Scope;
                context.Items["email"] = checktokenResponse.Identity.Email;
            }
            else
            {
                await SendFranceConnectError(context, response.StatusCode, response.Content.ReadAsStringAsync().Result);
                return;
            }
            if (!context.Response.HasStarted)
            {
                await _next(context);
            }
        }


        #region Helpers

        private static async Task InvalidAuthorizationHeader(HttpContext context)
        {
            await SendError(context, HttpStatusCode.BadRequest, new Error
            {
                error = "invalid_request",
                message = "The request does not contain authorization header"
            });
        }

        private static async Task AccessTokenNotFound(HttpContext context)
        {
            await SendError(context, HttpStatusCode.Unauthorized, new Error
            {
                error = "invalid_token",
                message = "Access token not found"
            });
        }


        private static async Task SendError(HttpContext context, HttpStatusCode statusCode, Error error)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
        }

        private static async Task SendFranceConnectError(HttpContext context, HttpStatusCode statusCode, string error)
        {
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(error);
        }

        #endregion
    }
}
