// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace FranceConnect.DataProvider.Middleware
{
    public class ChecktokenResponse
    {
        public string[] Scope { get; set; }
        public Identity Identity { get; set; }
        public Client Client { get; set; }
        public string Identity_provider_id { get; set; }
        public string Identity_provider_host { get; set; }
        public string acr { get; set; }
    }

    public class Identity
    {
        public string Given_name { get; set; }
        public string Family_name { get; set; }
        public DateTimeOffset Birthdate { get; set; }
        public string Gender { get; set; }
        public string Birthplace { get; set; }
        public string Birthcountry { get; set; }
        public string Email { get; set; }
    }

    public class Client
    {
        public string Client_id { get; set; }
        public string Client_name { get; set; }
    }
}
