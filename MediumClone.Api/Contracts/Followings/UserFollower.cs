namespace MediumClone.Api.Contracts.Followings;

public record UserFollower(
    string Id,
    string Image,
    string Email,
    string FirstName,
    string LastName
);