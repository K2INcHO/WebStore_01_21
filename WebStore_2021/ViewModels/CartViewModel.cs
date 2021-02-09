using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore_2021.ViewModels
{
    public class CartViewModel
    {
        public IEnumerable<(ProductViewModel Product, int Quantity)> Items { get; set; }

        public int ItemsCout => Items?.Sum(item => item.Quantity) ?? 0;

        public decimal TotalPrice => Items?.Sum(item => item.Product.Price * item.Quantity) ?? 0m;
    }
}
