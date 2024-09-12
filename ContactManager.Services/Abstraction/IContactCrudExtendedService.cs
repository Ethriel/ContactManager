using ContactManagerDB.Model;
using System.Linq.Expressions;

namespace ContactManager.Services.Abstraction
{
    public interface IContactCrudExtendedService : IContactCrudService
    {
        Contact ReadById(object id);
        Task<Contact> ReadByIdAsync(object id);

        Contact ReadByCondition(Expression<Func<Contact, bool>> conditionExpression);
        Task<Contact> ReadByConditionAsync(Expression<Func<Contact, bool>> conditionExpression);

        IEnumerable<Contact> ReadEnumerableByCondition(Expression<Func<Contact, bool>> conditionExpression);
        Task<IEnumerable<Contact>> ReadEnumerableByConditionAsync(Expression<Func<Contact, bool>> conditionExpression);

        IEnumerable<Contact> ReadPortionEnumerable(int skip, int take);
        Task<IEnumerable<Contact>> ReadPortionEnumerableAsync(int skip, int take);
    }
}
