using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Domain;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public required DbSet<Activity> Activities { get; set; }


    }
}
