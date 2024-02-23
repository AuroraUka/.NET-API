using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public static class SeedData
    {
         public static void Initialize(PhoneBookContext context)
        {
            if (context.PhoneBooks.Any()) return;


            var books = new List<PhoneBookEntry>
            {
                new PhoneBookEntry
                {
                    FirstName = "Alice",
                    LastName = "Smith",
                    Type = PhoneType.Work.ToString(),
                    Number = "1234567890"
                },
                new PhoneBookEntry
                {
                    FirstName = "Bob",
                    LastName = "Doe",
                    Type = PhoneType.Cellphone.ToString(),
                    Number = "123456120"
                },
                new PhoneBookEntry
                {
                    FirstName = "Charlie",
                    LastName = "Johnson",
                    Type = PhoneType.Home.ToString(),
                    Number = "123143120"
                }
            };

            // AddRange() method is used to add a collection of entities
            context.PhoneBooks.AddRange(books);
            context.SaveChanges();
        }
    }
}