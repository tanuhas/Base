﻿namespace WebApplication5.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string IdW { get; set; }

    }
}