using Microsoft.AspNetCore.Mvc;

namespace SampleEmailAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost]
        public IActionResult SendMail(EmailModel emailModel)
        {
            _emailService.SendEmail(emailModel);

            return Ok();
        }
    }
}
