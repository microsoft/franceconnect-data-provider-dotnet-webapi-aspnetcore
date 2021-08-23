// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi_Data_Provider_DotNet.Models;
using WebApi_Data_Provider_DotNet.ViewModels.Account;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi_Data_Provider_DotNet.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(ApplicationDbContext context) : base(context) { }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Tentative de création de compte non valide.");
                return View(model);
            }
            if (_context.Users.Any(user => user.Email == model.Email))
            {
                ModelState.AddModelError("", "Ce compte existe déjà.");
                return View(model);
            }

            try
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    ValueOne = model.ValueOne,
                    ValueTwo = model.ValueTwo
                };
                _context.Users.Add(user);
                _context.SaveChanges();

                ViewData["Message"] = "Le compte a été créé.";
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Une erreur est survenue lors de la création du compte. Veuillez réessayer.");
                return View(model);
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
