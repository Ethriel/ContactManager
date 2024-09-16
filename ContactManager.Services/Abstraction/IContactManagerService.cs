using ContactManager.Services.Model.DTO;
using ContactManager.Services.Model.Utility.ApiResult.Abstraction;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Services.Abstraction
{
    public interface IContactManagerService
    {
        IApiResult AddContact(ContactDto contactDto);
        Task<IApiResult> AddContactAsync(ContactDto contactDto);
        IApiResult GetContactById(object id);
        Task<IApiResult> GetContactByIdAsync(object id);
        IApiResult ListContacts();
        Task<IApiResult> ListContactsAsync();
        IApiResult GetPortion(int skip, int take);
        Task<IApiResult> GetPortionAsync(int skip, int take);
        IApiResult DeleteContact(object id);
        Task<IApiResult> DeleteContactAsync(object id);
        IApiResult UpdateContact(ContactDto contactDto);
        Task<IApiResult> UpdateContactAsync(ContactDto contactDto);
        IApiResult CreateContactsFromFile(IFormFile file);
        Task<IApiResult> CreateContactsFromFileAsync(IFormFile file);
        IApiResult GetContactsByCondition(Func<ContactDto, bool> condition);
        Task<IApiResult> GetContactsByConditionAsync(Func<ContactDto, bool> condition);
    }
}
