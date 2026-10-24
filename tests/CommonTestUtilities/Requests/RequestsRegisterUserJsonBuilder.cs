using AdoPet.Communication.Requests;
using Bogus;

namespace CommonTestUtilities.Requests;

public class RequestsRegisterUserJsonBuilder
{
    public static RequestsRegisterUserJson Build()
    {
        return new Faker<RequestsRegisterUserJson>()
            .RuleFor(request => request.UserName, faker => faker.Person.UserName)
            .RuleFor(request => request.Email, (faker, x) => faker.Internet.Email(x.UserName))
            .RuleFor(request => request.Password, faker => faker.Internet.Password());
    }
}
