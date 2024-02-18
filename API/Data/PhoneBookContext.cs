using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<PhoneBook>  PhoneBooks { get; set; }
        public DbSet<Test>  Tests { get; set; }
    }
}