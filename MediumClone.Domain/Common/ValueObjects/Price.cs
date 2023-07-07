
using MediumClone.Domain.Common.Models;

namespace MediumClone.Domain.Common.ValueObjects;

public sealed class Price : ValueObject
{
    public decimal Value { get; set; }
    public string Currency { get; set; }

    private Price(decimal value, string currency)
    {
        Value = value;
        Currency = currency;
    }


    public static Price Create(decimal value, string currency)
    {
        return new Price(value, currency);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return Currency;
    }



#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.

    private Price() { }

#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
}