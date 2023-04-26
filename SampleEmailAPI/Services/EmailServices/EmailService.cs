using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace SampleEmailAPI.Services.EmailServices
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public void SendEmail(EmailModel request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = "New Candidate has been added!";

            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $""" 
                        <h2>New Candidate</h2>
                        <table style="width:30%">
                                <tr>
                                    <td >Name</td>
                                    <td>:<td>
                                    <td>{request.Name}</td>
                                </tr>                                
                                <tr>
                                    <td>Email</td>
                                    <td>:<td>
                                    <td>{request.Email}</td>
                                </tr>                                
                                <tr>
                                    <td>Mobile No</td>
                                    <td>:<td>
                                    <td>{request.MobileNo}</td>
                                </tr>
                                <tr>
                                    <td>URL</td>
                                    <td>:<td>
                                    <td>#</td>
                                </tr>
                        </table>
                        """
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                smtp.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
                smtp.Send(email);
                smtp.Dispose();
            }
        }
    }
}
