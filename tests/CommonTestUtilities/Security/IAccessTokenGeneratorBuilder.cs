using AdoPet.Domain.Entities;
using AdoPet.Domain.Security.Tokens;
using Bogus;
using Moq;

namespace CommonTestUtilities.Security;

public class IAccessTokenGeneratorBuilder
{
    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        var fakeToken = new Faker().Random.String2(32, "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789");

        mock.Setup(options => options.Generate(It.IsAny<User>())).Returns(fakeToken);

        return mock.Object;
    }
}
