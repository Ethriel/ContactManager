using ContactManager.Server.Extensions;
using ContactManager.Services.Abstraction;
using ContactManager.Services.Model.DTO;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly IContactManagerService contactManager;

        public ContactsController(ILogger<ContactsController> logger, IContactManagerService contactManager)
        {
            _logger = logger;
            this.contactManager = contactManager;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var result = await contactManager.ListContactsAsync();

            return this.ActionResultByApiResult(result, _logger);
        }

        [HttpPost("upload-csv")]
        public async Task<IActionResult> UploadScv([FromForm(Name = "csv")] IFormFile file)
        {
            var apiResult = await contactManager.CreateContactsFromFileAsync(file);

            return this.ActionResultByApiResult(apiResult, _logger);
        }

        [HttpPost("remove-contact")]
        public async Task<IActionResult> RemoveContact([FromBody] int id)
        {
            var apiResult = await contactManager.DeleteContactAsync(id);

            return this.ActionResultByApiResult(apiResult, _logger);
        }

        [HttpPost("update-contact")]
        public async Task<IActionResult> UpdateContact([FromBody] ContactDto contactDto)
        {
            var apiResult = await contactManager.UpdateContactAsync(contactDto);

            return this.ActionResultByApiResult(apiResult, _logger);
        }
    }
}
