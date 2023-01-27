using System.Collections.Generic;
using MovieSystem.Domain.SeedWork;

namespace MovieSystem.Domain.AggregatesModel.UserAggregate;
public class UserDetails : ValueObject
{
    public string Address { get; private set; }
    public string PersonalNumber { get; private set; }
    public string HomeNumber { get; private set; }
    public string? ImageUrl { get; private set; }

    public UserDetails(string address, string personalNumber, string homeNumber, string? imageUrl)
    {
        Address = address;
        PersonalNumber = personalNumber;
        HomeNumber = homeNumber;
        ImageUrl = imageUrl;
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Address;
        yield return PersonalNumber;
        yield return HomeNumber;
        yield return ImageUrl;
    }
}
