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
                    FirstName = "John",
                    LastName = "Doe",
                    Type = PhoneType.Work.ToString(),
                    Number = "1234567890"
                },
                new PhoneBookEntry
                {
                    FirstName = "John2",
                    LastName = "Doe2",
                    Type = PhoneType.Cellphone.ToString(),
                    Number = "123456120"
                }
            };

            // AddRange() method is used to add a collection of entities
            context.PhoneBooks.AddRange(books);
            context.SaveChanges();
        }
    }
}