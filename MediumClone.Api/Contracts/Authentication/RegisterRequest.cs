namespace MediumClone.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LastName,
    string Email,
    string Password,
    AddressRequest Address

    );


public record AddressRequest(
string Street,
string City,
string State,
string ZipCode
);