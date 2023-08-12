using MediumClone.Api.Contracts.Followings;

namespace MediumClone.Contracts.Authentication;

public record AuthenticationResponse(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Role,
    string Token,
    string Bio,
    string Image,
    List<string> FollowingsIds,
    List<string> FollowersIds
    );