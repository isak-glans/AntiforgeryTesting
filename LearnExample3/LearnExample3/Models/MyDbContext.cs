using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnExample3.Models
{
    public class MyDbContext : DbContext
    {
        DbSet<Student> Students { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
    }
}
