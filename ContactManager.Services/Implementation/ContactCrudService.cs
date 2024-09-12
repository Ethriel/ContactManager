using ContactManager.Services.Abstraction;
using ContactManager.Services.Model.Utility;
using ContactManagerDB.Model;
using Microsoft.EntityFrameworkCore;

namespace ContactManager.Services.Implementation
{
    public class ContactCrudService : IContactCrudService
    {
        protected readonly DbContext _dbContext;
        protected DbSet<Contact> _contacts;

        public ContactCrudService(DbContext dbContext)
        {
            _dbContext = dbContext;
            _contacts = _dbContext.Set<Contact>();
        }

        public bool Create(Contact contact)
        {
            _contacts.Add(contact);
            return ConfirmChanges() > 0;
        }

        public async Task<bool> CreateAsync(Contact contact)
        {
            return await Task.FromResult(Create(contact));
        }

        public bool Delete(object contactId)
        {
            var contactToDelete = _contacts.Find(contactId);

            if (contactToDelete != null)
            {
                _contacts.Remove(contactToDelete);
                return ConfirmChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(object contactId)
        {
            return await Task.FromResult(Delete(contactId));
        }

        public IEnumerable<Contact> Read()
        {
            return _contacts.ToArray();
        }

        public async Task<IEnumerable<Contact>> ReadAsync()
        {
            return await _contacts.ToArrayAsync();
        }

        public bool Update(Contact newContactData)
        {
            var contactToUpdate = _contacts.Find(newContactData.Id);

            if (contactToUpdate != null)
            {
                contactToUpdate = UpdateHelper<Contact>.Update(_dbContext.Model, contactToUpdate, newContactData);
                return ConfirmChanges() > 0;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UpdateAsync(Contact newContactData)
        {
            return await Task.FromResult(Update(newContactData));
        }

        public int ConfirmChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> ConfirmChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
