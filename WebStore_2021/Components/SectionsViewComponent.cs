﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore_2021.Infrastructure.Interfaces;
using WebStore_2021.ViewModels;

namespace WebStore_2021.Components
{
    //[ViewComponent(Name = "Название")]
    public class SectionsViewComponent : ViewComponent
    {
        private IProductData _ProductData;

        public SectionsViewComponent(IProductData ProductData) => _ProductData = ProductData;

        public IViewComponentResult Invoke()
        {
            var sections = _ProductData.GetSections();

            var parent_sections = sections.Where(s => s.ParentId is null);

            var parent_sections_views = parent_sections
                .Select(
                    s => new SectionViewModel
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Order = s.Order,
                        ProductsCount = s.Products.Count()
                    })
                .ToList();

            int orderSortMethod(SectionViewModel a, SectionViewModel b) => Comparer<int>.Default.Compare(a.Order, b.Order);
            foreach (var parent_section in parent_sections_views)
            {
                var childs = sections.Where(s => s.ParentId == parent_section.Id);

                foreach (var child_section in childs)
                    parent_section.ChildSections.Add(new SectionViewModel
                    {
                        Id = child_section.Id,
                        Name = child_section.Name,
                        Order = child_section.Order,
                        Parent = parent_section,
                        ProductsCount = child_section.Products.Count()
                    });

                parent_section.ChildSections.Sort(orderSortMethod);
            }
            parent_sections_views.Sort(orderSortMethod);

            return View(parent_sections_views);
        }

        //public async Task<IViewComponentResult> InvokeAsync() => View();
    }
}
