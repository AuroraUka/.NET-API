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
    }
}