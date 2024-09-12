using ContactManager.Services.Abstraction;
using ContactManagerDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactManager.Services.Implementation
{
    public class ContactCrudExtendedService : ContactCrudService, IContactCrudExtendedService
    {
        public ContactCrudExtendedService(DbContext dbContext) : base(dbContext) { }

        public Contact ReadByCondition(Expression<Func<Contact, bool>> conditionExpression)
        {
            return _contacts.FirstOrDefault(conditionExpression);
        }

        public async Task<Contact> ReadByConditionAsync(Expression<Func<Contact, bool>> conditionExpression)
        {
            return await _contacts.FirstOrDefaultAsync(conditionExpression);
        }

        public Contact ReadById(object id)
        {
            return _contacts.Find(id);
        }

        public async Task<Contact> ReadByIdAsync(object id)
        {
            return await _contacts.FindAsync(id);
        }

        public IEnumerable<Contact> ReadEnumerableByCondition(Expression<Func<Contact, bool>> conditionExpression)
        {
            return _contacts.Where(conditionExpression).ToArray();
        }

        public async Task<IEnumerable<Contact>> ReadEnumerableByConditionAsync(Expression<Func<Contact, bool>> conditionExpression)
        {
            return await _contacts.Where(conditionExpression).ToArrayAsync();
        }

        public IEnumerable<Contact> ReadPortionEnumerable(int skip, int take)
        {
            return _contacts.Skip(skip).Take(take).ToArray();
        }

        public async Task<IEnumerable<Contact>> ReadPortionEnumerableAsync(int skip, int take)
        {
            return await _contacts.Skip(take).Take(skip).ToArrayAsync();
        }
    }
}
