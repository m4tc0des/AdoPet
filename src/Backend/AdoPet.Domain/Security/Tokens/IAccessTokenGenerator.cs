using AdoPet.Domain.Entities;

namespace AdoPet.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string Generate(User user);
}
