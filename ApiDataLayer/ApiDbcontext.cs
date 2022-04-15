using ApiDataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDataLayer
{
    internal class ApiDbcontext : DbContext
    {

        public DbSet<Category> Categories { get; set; } = default!;

    }
}
