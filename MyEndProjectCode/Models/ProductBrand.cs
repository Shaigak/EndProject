﻿namespace MyEndProjectCode.Models
{
    public class ProductBrand:BaseEntity
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
