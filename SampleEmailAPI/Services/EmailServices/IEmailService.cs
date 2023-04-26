namespace SampleEmailAPI.Services.EmailServices
{
    public interface IEmailService
    {
        void SendEmail(EmailModel request);
    }
}
