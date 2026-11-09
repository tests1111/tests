namespace Notify.Api.Test;

using Infrastructure.EmailService;
using Microsoft.Extensions.Logging.Abstractions;
using Notify.Api.Options;

public class NotificationServiceTests
{
    [Theory]
    [InlineData(PersonType.Individual, true)]
    [InlineData(PersonType.Company, true)]
    [InlineData(PersonType.Individual, false)]
    [InlineData(PersonType.Company, false)]
    public async Task SendNotifySuccessTest(PersonType personType, bool throwFirstSending)
    {
        var profiles = new Dictionary<PersonType, NotificationProfile>
        {
            {
                PersonType.Default,
                new NotificationProfile
                {
                    To = "Default@email.com",
                    Subject = "DefaultSubject",
                    Body = "DefaultBody"
                }
            },
            {
                PersonType.Individual,
                new NotificationProfile
                {
                    To = "Individual@email.com",
                    Subject = "IndividualSubject",
                    Body = "IndividualBody"
                }
            },
            {
                PersonType.Company,
                new NotificationProfile
                {
                    To = "Company@email.com",
                    Subject = "CompanySubject",
                    Body = "CompanyBody"
                }
            }
        };

        var emailService = new Mock<IEmailService>();
        emailService.Setup(x => x.Send(
                It.Is<EmailMessage>(x => x.To == "Default@email.com" && throwFirstSending),
                It.IsAny<CancellationToken>()))
            .Throws(new Exception("Send email exception"));

        var options = new Mock<IOptions<NotificationOption>>();
        options.Setup(x => x.Value).Returns(new NotificationOption { Profiles = profiles });

        var logger = new NullLogger<NotificationService>();

        INotificationService notificationService = new NotificationService(
            emailService.Object,
            options.Object,
            logger);

        // Act
        var success = false;
        var exception = await Record.ExceptionAsync(async () =>
        {
            success = await notificationService.SendNotify(new NotificationItemModel
            {
                Name = "Ivanov Ivan",
                Phone = "123456789",
                Email = "Ivanv@email.com",
                PersonType = personType,
            });
        });

        // Assert
        Assert.Null(exception);
        Assert.True(success);
    }
}