// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;

namespace WebApi_Data_Provider_DotNet.Models
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public ApplicationUser(string email) : this()
        {
            Email = email;
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string ValueOne { get; set; }
        public string ValueTwo { get; set; }
    }
}
