using ContactManager.Services.Abstraction;
using ContactManager.Services.Model.DTO;
using ContactManager.Services.Model.Utility.ApiResult.Abstraction;
using ContactManager.Services.Model.Utility.ApiResult.Implementation;
using Microsoft.AspNetCore.Http;

namespace ContactManager.Services.Implementation
{
    public class ContactManagerService : IContactManagerService
    {
        private readonly IContactCrudExtendedService contactService;
        private readonly ICsvService<ContactDto> csvService;
        private readonly IMapperService contactMapperService;

        public ContactManagerService(IContactCrudExtendedService contactService, ICsvService<ContactDto> csvService, IMapperService contactMapperService)
        {
            this.contactService = contactService;
            this.csvService = csvService;
            this.contactMapperService = contactMapperService;
        }
        public IApiResult AddContact(ContactDto contactDto)
        {
            var result = default(IApiResult);
            var existingContact = contactService.ReadByCondition(x => x.Phone == contactDto.Phone || x.Id == contactDto.Id);

            if (existingContact != null)
            {
                var errorMessage = $"Contact with phone {contactDto.Phone} already exists!";
                return new ApiErrorResult(ApiResultStatus.Conflict, errorMessage, errorMessage, new string[] { errorMessage });
            }

            var contactToAdd = contactMapperService.MapContact(contactDto);
            var createResult = contactService.Create(contactToAdd);

            if (!createResult)
            {
                var errorMessage = "An error occurred while creating a new contact";
                result = new ApiErrorResult(ApiResultStatus.BadRequest, errorMessage, errorMessage);
            }
            else
            {
                result = new ApiOkResult(ApiResultStatus.Ok, "Success");
            }

            return result;
        }

        public async Task<IApiResult> AddContactAsync(ContactDto contactDto)
        {
            return await Task.Run(() => AddContact(contactDto));
        }

        public IApiResult CreateContactsFromFile(IFormFile file)
        {
            var result = default(IApiResult);

            var contactDtosFromFile = csvService.ReadFromStream(file.OpenReadStream());

            if (contactDtosFromFile == null || !contactDtosFromFile.Any())
            {
                var errorMessage = "Could not create contacts from file";
                result = new ApiErrorResult(ApiResultStatus.BadRequest, errorMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var createResult = default(IApiResult);

                foreach (var contact in contactDtosFromFile)
                {
                    createResult = AddContact(contact);

                    if (createResult.ApiResultStatus != ApiResultStatus.Ok)
                    {
                        return createResult;
                    }
                }

                result = new ApiOkResult(ApiResultStatus.Ok, "Successfully created contacts from file");
            }

            return result;
        }

        public async Task<IApiResult> CreateContactsFromFileAsync(IFormFile file)
        {
            return await Task.FromResult(CreateContactsFromFile(file));
        }

        public IApiResult DeleteContact(ContactDto contactDto)
        {
            var result = default(IApiResult);
            var existingContact = contactService.ReadById(contactDto.Id);

            if (existingContact != null)
            {
                var loggerMessage = $"Contact (Id = {contactDto.Id}) does not exist!";
                var errorMessage = "Contact does not exist!";
                result = new ApiErrorResult(ApiResultStatus.NotFound, errorMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var deleteResult = contactService.Delete(contactDto.Id);

                if (!deleteResult)
                {
                    var errorMessage = "An error occurred while deleting a contact";
                    result = new ApiErrorResult(ApiResultStatus.BadRequest, errorMessage, errorMessage);
                }
                else
                {
                    result = new ApiOkResult(ApiResultStatus.Ok, "Success");
                }
            }

            return result;
        }

        public async Task<IApiResult> DeleteContactAsync(ContactDto contactDto)
        {
            return await Task.FromResult(DeleteContact(contactDto));
        }

        public IApiResult GetContactById(object id)
        {
            var result = default(IApiResult);
            var existingContact = contactService.ReadById(id);

            if (existingContact != null)
            {
                var loggerMessage = $"Contact (Id = {id}) does not exist!";
                var errorMessage = "Contact does not exist!";
                result = new ApiErrorResult(ApiResultStatus.NotFound, errorMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var contactDto = contactMapperService.MapContactDto(existingContact);
                result = new ApiOkResult(ApiResultStatus.Ok, "Success", contactDto);
            }

            return result;
        }

        public async Task<IApiResult> GetContactByIdAsync(object id)
        {
            return await Task.FromResult(GetContactById(id));
        }

        public IApiResult GetContactsByCondition(Func<ContactDto, bool> condition)
        {
            var contacts = contactService.Read();
            var contactDtos = contactMapperService.MapContactDtos(contacts);
            var contactsByCondition = contactDtos.Where(condition);
            var result = default(IApiResult);
            var message = default(string);

            if (contactsByCondition != null && contactsByCondition.Any())
            {
                message = "Success";
                result = new ApiOkResult(ApiResultStatus.Ok, message, contactsByCondition);
            }
            else
            {
                message = "There are no contacts by a given condition";
                result = new ApiErrorResult(ApiResultStatus.NotFound, message, message, new string[] { message });
            }

            return result;
        }

        public async Task<IApiResult> GetContactsByConditionAsync(Func<ContactDto, bool> condition)
        {
            return await Task.FromResult(GetContactsByCondition(condition));
        }

        public IApiResult GetContactsFromFile(IFormFile file)
        {
            var result = default(IApiResult);
            var errorMessage = "Could not get contacts from file";

            var contactDtosFromFile = csvService.ReadFromStream(file.OpenReadStream());

            if (contactDtosFromFile == null || !contactDtosFromFile.Any())
            {
                result = new ApiErrorResult(ApiResultStatus.BadRequest, errorMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var addContactResult = default(IApiResult);
                var skippedContacts = 0;

                foreach (var contactToAdd in contactDtosFromFile)
                {
                    addContactResult = AddContact(contactToAdd);

                    // One or more contacts may already be in the DB, just skip them
                    if (addContactResult is IApiErrorResult && (addContactResult as IApiErrorResult).ApiResultStatus == ApiResultStatus.Conflict)
                        skippedContacts++;
                }

                // All contacts from CSV are already in the DB
                if (skippedContacts == contactDtosFromFile.Count())
                {
                    result = new ApiErrorResult(ApiResultStatus.Conflict, errorMessage, errorMessage, new string[] { errorMessage });
                }
                else
                {
                    result = new ApiOkResult(ApiResultStatus.Empty, "Successfully got contacts from file");
                }
            }

            return result;
        }

        public async Task<IApiResult> GetContactsFromFileAsync(IFormFile file)
        {
            return await Task.FromResult(GetContactsFromFile(file));
        }

        public IApiResult GetPortion(int skip, int take)
        {
            var result = default(IApiResult);

            var contacts = contactService.ReadPortionEnumerable(skip, take);

            if (contacts == null || !contacts.Any())
            {
                var loggerMessage = $"Could not get portion (skip = {skip}, take = {take}) of contacts";
                var errorMessage = "Could not get portion of contacts from file";
                result = new ApiErrorResult(ApiResultStatus.BadRequest, loggerMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var contactDtos = contactMapperService.MapContactDtos(contacts);
                result = new ApiOkResult(ApiResultStatus.Ok, "Success", contactDtos);
            }

            return result;
        }

        public async Task<IApiResult> GetPortionAsync(int skip, int take)
        {
            return await Task.FromResult(GetPortion(skip, take));
        }

        public IApiResult ListContacts()
        {
            var result = default(IApiResult);

            var contacts = contactService.Read();

            if (contacts == null || !contacts.Any())
            {
                var errorMessage = $"Could not get contacts";
                result = new ApiErrorResult(ApiResultStatus.Empty, errorMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var contactDtos = contactMapperService.MapContactDtos(contacts);
                result = new ApiOkResult(ApiResultStatus.Ok, "Success", contactDtos);
            }

            return result;
        }

        public async Task<IApiResult> ListContactsAsync()
        {
            return await Task.FromResult((ListContacts()));
        }

        public IApiResult UpdateContact(ContactDto contactDto)
        {
            var result = default(IApiResult);

            var existingContact = contactService.ReadById(contactDto.Id);

            if (existingContact == null)
            {
                var loggerMessage = $"Contact (Id = {contactDto.Id}) does not exist!";
                var errorMessage = "Contact does not exits!";
                result = new ApiErrorResult(ApiResultStatus.NotFound, loggerMessage, errorMessage, new string[] { errorMessage });
            }
            else
            {
                var newContactData = contactMapperService.MapContact(contactDto);
                var updateResult = contactService.Update(newContactData);

                if (!updateResult)
                {
                    var loggerMessage = $"An error occurred while updating contact (Id = {contactDto.Id})";
                    var errorMessage = "An error occurred while updating contact";
                    result = new ApiErrorResult(ApiResultStatus.BadRequest, loggerMessage, errorMessage, new string[] { errorMessage });
                }
                else
                {
                    result = new ApiOkResult(ApiResultStatus.Ok, "Success");
                }
            }

            return result;
        }

        public async Task<IApiResult> UpdateContactAsync(ContactDto contactDto)
        {
            return await Task.FromResult(UpdateContact(contactDto));
        }
    }
}
