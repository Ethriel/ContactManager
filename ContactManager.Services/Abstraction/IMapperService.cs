using ContactManager.Services.Model.DTO;
using ContactManagerDB.Model;

namespace ContactManager.Services.Abstraction
{
    public interface IMapperService
    {
        public Contact MapContact(ContactDto contactDto);
        public Task<Contact> MapContactAsync(ContactDto contactDto);
        public ContactDto MapContactDto(Contact contact);
        public Task<ContactDto> MapContactDtoAsync(Contact contact);
        public IEnumerable<Contact> MapContacts(IEnumerable<ContactDto> contactDtos);
        public Task<IEnumerable<Contact>> MapContactsAsync(IEnumerable<ContactDto> contactDtos);
        public IEnumerable<ContactDto> MapContactDtos(IEnumerable<Contact> contacts);
        public Task<IEnumerable<ContactDto>> MapContactDtosAsync(IEnumerable<Contact> contacts);
    }
}
