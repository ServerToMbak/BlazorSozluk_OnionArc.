namespace BlazorSozluk.Projections.UserService.Services;

public class EmailService
{
    private readonly IConfiguration configuration;
    private readonly ILogger<EmailService> logger;

    public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
    {
        this.configuration = configuration;
        this.logger = logger;
    }

    public string GenerateConfirmationLink(Guid confirmationId)
    {
        var baseUrl = configuration["ConfirmationLinkBase"] + confirmationId;

        return baseUrl; 
    }


    public  Task  SendEmail(string toEmailAddress,string content)
    {
        // Send Email SERVİCE
        logger.LogInformation($"Email Sent to {toEmailAddress} with Content of {content}");


        return Task.CompletedTask;
    }
}
