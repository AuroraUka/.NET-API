using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace API.Data
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext( DbContextOptions options) : base(options)
        {
        }

        public DbSet<PhoneBookEntry>  PhoneBooks { get; set; }  

    }
}