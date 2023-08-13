namespace MediumClone.Api.Contracts.Followings;

public sealed record FollowUserRequest(string UserId);

public sealed record UnFollowUserRequest(string UserId);