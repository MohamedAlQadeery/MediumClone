namespace MediumClone.Api.Contracts.Followings;
public record FollowingInfoResponse(string Id, string FirstName, string LastName, string Image);
public record FollowerInfoResponse(string Id, string FirstName, string LastName, string Image);