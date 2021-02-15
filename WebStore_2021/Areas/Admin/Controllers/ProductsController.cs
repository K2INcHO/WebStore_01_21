﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities.Identity;
using WebStore_2021.Infrastructure.Interfaces;

namespace WebStore_2021.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData;

        public ProductsController(IProductData productData) => _ProductData = productData;

        public IActionResult Index => View(_ProductData.GetProducts());
    }
}