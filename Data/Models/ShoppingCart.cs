﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkAndGo.Data.Models
{
    public class ShoppingCart
    {
        public string ShoppingCartId { get; set; }

        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
