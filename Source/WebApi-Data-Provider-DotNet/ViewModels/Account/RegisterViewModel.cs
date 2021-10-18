// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System.ComponentModel.DataAnnotations;

namespace WebApi_Data_Provider_DotNet.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Adresse email non valide")]
        [Display(Name = "Adresse email")]
        public string Email { get; set; }

        [Display(Name = "Valeur 1")]
        public string ValueOne { get; set; }

        [Display(Name = "Valeur 2")]
        public string ValueTwo { get; set; }
    }
}
