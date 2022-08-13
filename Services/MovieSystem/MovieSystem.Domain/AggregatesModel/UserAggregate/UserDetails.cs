using System.Collections.Generic;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate;
public class UserDetails : ValueObject
{
    public string Address { get; private set; }
    public string PersonalNumber { get; private set; }
    public string HomeNumber { get; private set; }
#pragma warning disable CA1056 // URI-like properties should not be strings
    public string? ImageUrl { get; private set; }
#pragma warning restore CA1056 // URI-like properties should not be strings

#pragma warning disable CA1054 // URI-like parameters should not be strings
    public UserDetails(string address, string personalNumber, string homeNumber, string? imageUrl)
#pragma warning restore CA1054 // URI-like parameters should not be strings
    {
        Address = address;
        PersonalNumber = personalNumber;
        HomeNumber = homeNumber;
        ImageUrl = imageUrl;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Address;
        yield return PersonalNumber;
        yield return HomeNumber;
        if (ImageUrl is not null)
        {
            yield return ImageUrl;
        }
    }
}
