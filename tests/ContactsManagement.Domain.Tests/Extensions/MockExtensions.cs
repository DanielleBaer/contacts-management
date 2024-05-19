using Microsoft.Extensions.Logging;
using Moq;

namespace ContactsManagement.Domain.Tests.Extensions;

internal static class MockExtensions
{
    internal static Mock<ILogger<T>> SetupLogging<T>(
        this Mock<ILogger<T>> logger,
        string expectedMessage,
        LogLevel expectedLogLevel,
        Exception? ex = null)
    {
        logger
            .Setup(x => x.Log(
                It.Is<LogLevel>(l => l == expectedLogLevel),
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((v, _) => v.ToString() == expectedMessage),
                ex ?? It.IsAny<Exception>(),
                (Func<It.IsAnyType, Exception?, string>)It.IsAny<object>()));

        return logger;
    }
}
