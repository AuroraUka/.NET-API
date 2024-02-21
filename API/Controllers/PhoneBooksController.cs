using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PhoneBooksController : ControllerBase
    {
        private readonly PhoneBookContext _context;

        public PhoneBooksController(PhoneBookContext context)
        {
            this._context = context;
        }


       [HttpGet]
        public async Task<ActionResult<List<PhoneBookDto>>> GetProducts()
        {
            var phoneBooks = await _context.PhoneBooks.ToListAsync();
            var phoneBookDTOs = phoneBooks.Select(PhoneBookToDto).ToList(); // Removed () after PhoneBookToDto
            return phoneBookDTOs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneBookDto>> GetProduct(int id)
        {
            var phoneBook = await _context.PhoneBooks.FindAsync(id);
            if (phoneBook == null)
            {
                return NotFound();
            }
            var phoneBookDTO = PhoneBookToDto(phoneBook); // Removed 's' from PhoneBookToDto
            return phoneBookDTO;
        }

        [HttpPost]
        public async Task<ActionResult<PhoneBook>> CreatePhoneBook(PhoneBook phoneBook)
        {
            _context.PhoneBooks.Add(phoneBook);
            _context.SaveChanges();

            return CreatedAtAction(nameof(CreatePhoneBook), new { id = phoneBook.Id }, phoneBook);
        }

         [HttpPut("{id}")]
        public async Task<ActionResult> EditPhoneBook(int id, PhoneBook phoneBook)
        {
            if (id != phoneBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(entity: phoneBook).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        
         [HttpPost("{id}")]
        public async Task<ActionResult> EditPhoneBookP(int id, PhoneBook phoneBook)
        {
            if (id != phoneBook.Id)
            {
                return BadRequest();
            }

            _context.Entry(entity: phoneBook).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult DeletePhoneBook(int id)
        {
            var phoneBook = _context.PhoneBooks.Find(id);

            if (phoneBook == null)
            {
                return NotFound();
            }

            _context.PhoneBooks.Remove(phoneBook);
            _context.SaveChanges();

            return NoContent();
        }



         private PhoneBookDto PhoneBookToDto(PhoneBook pb) // Removed static and Func<>
        {
            return new PhoneBookDto
            {
                Id = pb.Id,
                Name = pb.Name,
                Surname = pb.Surname,
                Type = pb.Type.ToString(),
                Number = pb.Number
            };
        }
    }
}