using AdoPet.Domain.Security.PasswordHashing;
using Moq;

namespace CommonTestUtilities.Security;

public class IPasswordHasherBuilder
{
    private readonly Mock<IPasswordHasher> _mock;

    public IPasswordHasherBuilder()
    {
        _mock = new Mock<IPasswordHasher>();

        _mock.Setup(passwordHash => passwordHash.HashPassword(It.IsAny<string>())).Returns("hashed_password");
    }

    public void VerifyPassword(string password)
    {
        _mock.Setup(hasher => hasher.VerifyPassword(password, It.IsAny<string>())).Returns(true);
    }

    public IPasswordHasher Build()
    {
        return _mock.Object;
    }
}
