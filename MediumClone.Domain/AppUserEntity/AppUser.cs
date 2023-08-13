using Microsoft.AspNetCore.Identity;
using MediumClone.Domain.AppUserEntity.Enums;
using MediumClone.Domain.Common.ValueObjects;
using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.FollowingEntity;
using ErrorOr;
using MediumClone.Domain.Common.DomainErrors;

namespace MediumClone.Domain.AppUserEntity;

public class AppUser : IdentityUser
{
    public AppUserRole AppUserRole { get; set; } = AppUserRole.User;
    public Address Address { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;


    public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
    public DateTime? UpdatedDateTime { get; set; }
    public bool IsActive { get; private set; } = true;

    public string? Bio { get; set; }
    public string? Image { get; set; } = null!;

    public List<Article> Articles { get; private set; } = new();

    public List<Following> Followers { get; private set; } = new();
    public List<Following> Followings { get; private set; } = new();


    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }


    public Following FollowUser(string userIdToFollow)
    {
        var newFollowing = Following.Create(Id, userIdToFollow);
        Followings.Add(newFollowing);
        return newFollowing;

    }










}