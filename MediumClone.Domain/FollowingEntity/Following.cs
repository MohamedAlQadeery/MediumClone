using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.FollowingEntity;

public class Following : BaseEntity<int>
{
    public string FollowingUserId { get; private set; }
    public string FollowedUserId { get; private set; }

    public AppUser FollowingUser { get; private set; }
    public AppUser FollowedUser { get; private set; }


    private Following(string followingUserId, string followedUserId)
    {
        FollowingUserId = followingUserId;
        FollowedUserId = followedUserId;
    }

    public static Following Create(string followingUserId, string followedUserId)
    {
        return new Following(followingUserId, followedUserId);
    }

}