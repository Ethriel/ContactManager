using AutoMapper;
using ContactManager.Services.Abstraction;
using ContactManager.Services.Model.DTO;
using ContactManagerDB.Model;

namespace ContactManager.Services.Implementation
{
    public class MapperService : IMapperService
    {
        private readonly IMapper mapper;

        public MapperService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public Contact MapContact(ContactDto contactDto)
        {
            return mapper.Map<ContactDto, Contact>(contactDto);
        }

        public async Task<Contact> MapContactAsync(ContactDto contactDto)
        {
            return await Task.FromResult(MapContact(contactDto));
        }

        public ContactDto MapContactDto(Contact contact)
        {
            return mapper.Map<Contact, ContactDto>(contact);
        }

        public async Task<ContactDto> MapContactDtoAsync(Contact contact)
        {
            return await Task.FromResult(MapContactDto(contact));
        }

        public IEnumerable<ContactDto> MapContactDtos(IEnumerable<Contact> contacts)
        {
            return mapper.Map<IEnumerable<Contact>, IEnumerable<ContactDto>>(contacts);
        }

        public async Task<IEnumerable<ContactDto>> MapContactDtosAsync(IEnumerable<Contact> contacts)
        {
            return await Task.FromResult(MapContactDtos(contacts));
        }

        public IEnumerable<Contact> MapContacts(IEnumerable<ContactDto> contactDtos)
        {
            return mapper.Map<IEnumerable<ContactDto>, IEnumerable<Contact>>(contactDtos);
        }

        public async Task<IEnumerable<Contact>> MapContactsAsync(IEnumerable<ContactDto> contactDtos)
        {
            return await Task.FromResult(MapContacts(contactDtos));
        }
    }
}
