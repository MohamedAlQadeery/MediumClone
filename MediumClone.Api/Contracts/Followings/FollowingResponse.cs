namespace MediumClone.Api.Contracts.Followings;
public record FollowingResponse(int Id, string FollowingUserId,
string FollowedUserId, DateTime CreatedDateTime);