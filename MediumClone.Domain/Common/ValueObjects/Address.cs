using MediumClone.Domain.Common.Models;

namespace MediumClone.Domain.Common.ValueObjects;
public sealed class Address : ValueObject
{
    public string Street { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string ZipCode { get; private set; }


    private Address(string street, string city, string country, string zipCode)
    {
        Street = street;
        City = city;
        Country = country;
        ZipCode = zipCode;
    }

    //for Ef
    private Address()
    {

    }
    public static Address Create(string street, string city, string country, string zipCode)
    {
        //Todo enforce invariant

        return new Address(street, city, country, zipCode);

    }


    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Street;
        yield return City;
        yield return Country;
        yield return ZipCode;
    }
}