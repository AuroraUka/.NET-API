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

        public DbSet<PhoneBook>  PhoneBooks { get; set; }  

         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map enum values to string representation for Type property
        modelBuilder.Entity<PhoneBook>()
            .Property(p => p.Type)
            .HasConversion(
                v => v.ToString(), // Convert enum to string
                v => (PhoneType)Enum.Parse(typeof(PhoneType), v) // Convert string to enum
            );

        base.OnModelCreating(modelBuilder);
    }    
//     protected override void OnModelCreating(ModelBuilder modelBuilder)
// {
//     modelBuilder
//         .Entity<PhoneBook>()
//         .Property(e => e.Type)
//         .HasConversion<string>();
// }
        public DbSet<Test>  Tests { get; set; }

    }
}