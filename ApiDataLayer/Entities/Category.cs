﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ApiDataLayer.Entities
{
    public class Category
    {

        public int CategoryId { get; set; }
        
        public string Name { get; set; } = default!;

        [JsonIgnore]
        public List<ProductCategory> Products = default!;

    }
}
