using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class PhoneBookService : IPhoneBookService
    {
        private readonly PhoneBookContext _dbContext;
        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        private readonly SemaphoreSlim _getSemaphore = new SemaphoreSlim(10, 10); 
        private readonly ILogger<PhoneBookService> _logger;

        public PhoneBookService(PhoneBookContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPhoneBook(PhoneBookEntry entry)
        {
            await _semaphore.WaitAsync();
            try
            {
                entry.Type = entry.Type.ToString();
                _dbContext.PhoneBooks.Add(entry);
                await _dbContext.SaveChangesAsync(); 
            }
            catch ( Exception ex )
            {
                _logger.LogError(ex, "An error occurred while adding a phone book entry.");
                throw;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task DeletePhoneBook(int id)
        {
            await _semaphore.WaitAsync();
            try
            {
                var entry = await _dbContext.PhoneBooks.FindAsync(id);
                if (entry != null)
                {
                    _dbContext.PhoneBooks.Remove(entry);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch ( Exception ex ) 
            {
                 _logger.LogError(ex, "An error occurred while deleting a phone book entry.");
                 throw;
            } 
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task EditPhoneBook(PhoneBookEntry entry)
        {
            await _semaphore.WaitAsync();
            try
            {
                entry.Type = entry.Type.ToString();
                _dbContext.Entry(entry).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch ( Exception ex )
            {
                 _logger.LogError(ex, "An error occurred while updating a phone book entry.");
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async Task<List<PhoneBookEntry>> GetPhoneBooks()
        {
             await _getSemaphore.WaitAsync();
            try
            {
                return await _dbContext.PhoneBooks.ToListAsync();
            }
            catch ( Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while fetching  phone book entries.");
                 throw;
            }
            finally
            {
                _getSemaphore.Release();
            }
        }

        public async Task<PhoneBookEntry> GetPhoneBook(int id)
        {
            await _getSemaphore.WaitAsync();
            try
            {
                return await _dbContext.PhoneBooks.FindAsync(id);
            }
            catch ( Exception ex ) 
            {
                 _logger.LogError(ex, "An error occurred while fetching your phone book entry.");
                 throw;
            } 
            finally
            {
                _getSemaphore.Release();
            }
        }

        public async Task<List<PhoneBookEntry>> GetPhoneBooksAlphabetically()
        {
            await _getSemaphore.WaitAsync();
            try
            {
                return await _dbContext.PhoneBooks.OrderBy(e => e.FirstName).ThenBy(e => e.LastName).ToListAsync();
            }
            catch ( Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while fetching  phone book entries alphabetically.");
                 throw;
            }
            finally
            {
            _getSemaphore.Release();
            }
        }

        public async Task<List<PhoneBookEntry>> GetPhoneBooksByFirstName()
        {
            await _getSemaphore.WaitAsync();
            try
            {
                return await _dbContext.PhoneBooks.OrderBy(e => e.FirstName).ToListAsync();
            }
             catch ( Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while fetching  phone book entries by first name.");
                 throw;
            }
            finally
            {
                _getSemaphore.Release();
            }
        }

        public async Task<List<PhoneBookEntry>> GetPhoneBooksByLastName()
        {
            await _getSemaphore.WaitAsync();
            try
            {
                return await _dbContext.PhoneBooks.OrderBy(e => e.LastName).ToListAsync();
            }
            catch ( Exception ex)
            {
                 _logger.LogError(ex, "An error occurred while fetching  phone book entries by last name.");
                 throw;
            }
            finally
            {
                _getSemaphore.Release();
            }
        }
    }
}