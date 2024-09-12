using ContactManagerDB.Model;

namespace ContactManager.Services.Abstraction
{
    public interface IContactCrudService
    {
        bool Create(Contact contact);
        Task<bool> CreateAsync(Contact contact);

        IEnumerable<Contact> Read();
        Task<IEnumerable<Contact>> ReadAsync();

        bool Update(Contact newContactData);
        Task<bool> UpdateAsync(Contact newContactData);

        bool Delete(object contactId);
        Task<bool> DeleteAsync(object contactId);
    }
}
