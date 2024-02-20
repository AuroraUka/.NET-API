using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
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
        public async Task<ActionResult<List<PhoneBook>>> GetProducts()
        {
            return await _context.PhoneBooks.ToListAsync();
        }
        
        [HttpGet("{id}")]
         public async Task<ActionResult<PhoneBook>>  GetProduct(int id)
        {
            return await _context.PhoneBooks.FindAsync(id);
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
    }
}