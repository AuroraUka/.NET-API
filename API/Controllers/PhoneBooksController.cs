using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class PhoneBooksController : BaseApiController
    {
         private readonly IPhoneBookService _phoneBookService;

        public PhoneBooksController(IPhoneBookService phoneBookService)
        {
            _phoneBookService = phoneBookService;
        }


       [HttpGet]
        public async Task<ActionResult<List<PhoneBookEntry>>> GetPhoneBooks()
        {   
            try
            {
            var entries = await _phoneBookService.GetPhoneBooks();
            return Ok(entries);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("alphabetical")]
        public async Task<ActionResult<List<PhoneBookEntry>>> GetPhoneBooksAlphabetically()
        {
            try
            {
                var entries = await _phoneBookService.GetPhoneBooksAlphabetically();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("firstName")]
        public async Task<ActionResult<List<PhoneBookEntry>>> GetPhoneBooksByFirstName()
        {
            try
            {
                var entries = await _phoneBookService.GetPhoneBooksByFirstName();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("lastName")]
        public async Task<ActionResult<List<PhoneBookEntry>>> GetPhoneBooksByLastName()
        {
            try
            {
                var entries = await _phoneBookService.GetPhoneBooksByLastName();
                return Ok(entries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PhoneBookEntry>> GetPhoneBook(int id)
        {
           var entry = await _phoneBookService.GetPhoneBook(id);
                return Ok(entry);
        }

       [HttpPost]
        public async Task<ActionResult> AddPhoneBook(PhoneBookEntry entry)
        {
            try
            {
                await _phoneBookService.AddPhoneBook(entry);
                return CreatedAtAction(nameof(AddPhoneBook), new { id = entry.Id }, entry);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> EditPhoneBook(int id, PhoneBookEntry entry)
        {
            try
            {
                if (id != entry.Id)
                {
                    return BadRequest("ID mismatch");
                }
                await _phoneBookService.EditPhoneBook(entry);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }            
        }



        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePhoneBook(int id)
        {
            try
            {
                await _phoneBookService.DeletePhoneBook(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

    }
}