using BlazorSozluk.Common;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Infrastructure;
using BlazorSozluk.Projections.UserService.Services;

namespace BlazorSozluk.Projections.UserService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly Services.UserService userService;
        private readonly EmailService emailService;

        public Worker(ILogger<Worker> logger, Services.UserService userService, EmailService emailService)
        {
            _logger = logger;
            this.userService = userService;
            this.emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
                QueueFactory.CreateBasiConsumer()
               .EnsurExchange(SozlukConstants.UserExchangeName)
               .EnsureQueue(SozlukConstants.UserEmailChangedQueuName, SozlukConstants.UserExchangeName)
               .Receive<UserEmailChangedEvent>(user =>
               {
                   // Db Ýnsert
                   var confirmationId = userService.CreateEmailConfirmation(user).GetAwaiter().GetResult();  

                  // Generate Link
                  var link = emailService.GenerateConfirmationLink(confirmationId);

                   // Send Email
                   emailService.SendEmail(user.NewEmailAddress,link).GetAwaiter().GetResult();
               })
               .StartConsuming(SozlukConstants.UserEmailChangedQueuName);


        }
    }
}
