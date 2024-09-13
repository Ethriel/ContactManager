using ContactManager.Server.Extensions;
using ContactManager.Services.Abstraction;
using ContactManager.Services.Model.DTO;
using ContactManager.Services.Model.Utility.ApiResult.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = Random.Shared.Next(-20, 55),
            //    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            //})
            //.ToArray();
            //var testDto = new ContactDto { Id = 1, Name = "test", Married = false, Phone = "0987654321", Salary = 100, DateOfBirth = DateTime.Now.ToShortDateString() };
            //var arr = new ContactDto[] { testDto };
            //var apiResult = new ApiOkResult(Services.Model.Utility.ApiResult.Abstraction.ApiResultStatus.Ok, "Success", arr);

            var result = await contactManager.ListContactsAsync();

            return this.ActionResultByApiResult(result, _logger);
        }

        [HttpPost("uploadCsv")]
        public async Task<IActionResult> UploadScv([FromForm(Name = "csv")] IFormFile file)
        {
            var apiResult = await contactManager.GetContactsFromFileAsync(file);

            return this.ActionResultByApiResult(apiResult, _logger);
        }
    }
}
