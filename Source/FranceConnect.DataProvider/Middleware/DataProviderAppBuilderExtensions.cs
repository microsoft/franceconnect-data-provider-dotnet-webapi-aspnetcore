// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using Microsoft.AspNetCore.Builder;

namespace FranceConnect.DataProvider.Middleware
{
    public static class DataProviderAppBuilderExtensions
    {
        public static IApplicationBuilder UseDataProvider(this IApplicationBuilder app, DataProviderOptions options)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            return app.UseMiddleware<DataProviderMiddleware>(options);
        }

        public static IApplicationBuilder UseDataProvider(this IApplicationBuilder app, Action<DataProviderOptions> configureOptions)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var options = new DataProviderOptions();
            configureOptions?.Invoke(options);

            return app.UseDataProvider(options);
        }
    }
}
