using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IPhoneBookService
    {
        Task<List<PhoneBookEntry>> GetPhoneBooks();
        Task<List<PhoneBookEntry>> GetPhoneBooksAlphabetically();
        Task<List<PhoneBookEntry>> GetPhoneBooksByFirstName();
        Task<List<PhoneBookEntry>> GetPhoneBooksByLastName();
        Task AddPhoneBook(PhoneBookEntry entry);
        Task EditPhoneBook(PhoneBookEntry entry);
        Task DeletePhoneBook(int id);
        Task<PhoneBookEntry> GetPhoneBook(int id);
    }
}