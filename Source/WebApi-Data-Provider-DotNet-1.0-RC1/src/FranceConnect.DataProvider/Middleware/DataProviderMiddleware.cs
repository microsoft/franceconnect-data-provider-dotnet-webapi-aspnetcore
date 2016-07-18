//
// The MIT License (MIT)
// Copyright (c) 2016 Microsoft France
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
// You may obtain a copy of the License at https://opensource.org/licenses/MIT
//

using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using FranceConnect.DataProvider.Models;

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
            var response = await client.PostAsJsonAsync<object>(Options.ChecktokenEndpoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var checktokenResponse = JsonConvert.DeserializeObject<ChecktokenResponse>(json);
                context.Items["scope"] = checktokenResponse.Scope;
                context.Items["email"] = checktokenResponse.Identity.Email;
            }
            else
            {
                await SendFranceConnectError(context, response.StatusCode, response.Content.ReadAsStringAsync().Result);
            }

            await _next(context);
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
            await context.Response.WriteAsync(JsonConvert.SerializeObject(error));
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
