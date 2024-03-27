using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhoneBook.Database;
using PhoneBook.Database.Model;
using PhoneBook.Dto;
using PhoneBook.Middileware;
using PhoneBook.Service;


namespace PhoneBook.Controllers
{
    [Route("api/[controller]/contact")]
    [ApiController]
    //[ServiceFilter(typeof(ApiKeyAuthFilter))]
    public class PhoneBookController : ControllerBase
    {
        private readonly IContactService contactService;
        private readonly ILogger<PhoneBookController> logger;
        public PhoneBookController(IContactService contactService, ILogger<PhoneBookController> logger)
        {
            this.contactService = contactService;
            this.logger = logger;
        }
        // GET: api/<PhoneBookController>
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery]int offset, [FromQuery]int numRecords)
        {
            logger.LogDebug("Start implementation : Get");
            var contacts=await contactService.GetContacts(offset, numRecords);
            logger.LogDebug("End implementation : Get");
            if (contacts != null)
                return Ok(contacts);                                                                                       
            else
                return new StatusCodeResult(500);
        }

        // GET api/<PhoneBookController>/b8442229-8797-4843-b920-0450d3196114
        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            logger.LogDebug("Start implementation : Get/id");
            var contact =await contactService.GetContactById(id);
            logger.LogDebug("End implementation : Get/id");
            if (contact != null)  
                return Ok(contact);
            else 
                return new StatusCodeResult(404);
        }

        // POST api/<PhoneBookController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddContactRequestDto contact) 
        {
            logger.LogDebug("Start implementation : Post");
            var Id=await contactService.AddContact(contact);
            logger.LogDebug("End implementation : Post");
            if(Id!=null)
                return Ok(Id);
            else
                return new StatusCodeResult(500);
        }

        // PUT api/<PhoneBookController>/b8442229-8797-4843-b920-0450d3196114
        [HttpPut()]
        public async Task<IActionResult> Put(ContactDto contact)
        {
            logger.LogDebug("Start implementation : Put");
            var updatedContact =await contactService.UpdateContact(contact);
            logger.LogDebug("End implementation : Put");
            if(updatedContact != null)
                return Ok(updatedContact);
            else
                return new StatusCodeResult(500);
        }

        // DELETE api/<PhoneBookController>/b8442229-8797-4843-b920-0450d3196114
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            logger.LogDebug("Start implementation : Delete");
            var status=await contactService.DeleteContactById(id);
            logger.LogDebug("End implementation : Delete");
            if(status)
                return Ok();
            else
                return new StatusCodeResult(500);
        }
    }
}
