using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_14.f.mod
{
    internal class Db: DbContext
    {
        public DbSet<Product> Products { get; set; }

    }
}
