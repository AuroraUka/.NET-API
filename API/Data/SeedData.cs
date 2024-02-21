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


            var books = new List<PhoneBook>
            {
                new PhoneBook
                {
                    Name = "John",
                    Surname = "Doe",
                    Type = PhoneType.Work,
                    Number = "1234567890"
                },
                new PhoneBook
                {
                    Name = "John2",
                    Surname = "Doe2",
                    Type = PhoneType.Cellphone,
                    Number = "123456120"
                }
            };

            // AddRange() method is used to add a collection of entities
            context.PhoneBooks.AddRange(books);
            context.SaveChanges();
        }
    }
}