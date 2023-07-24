using Microsoft.AspNetCore.Identity;
using MediumClone.Domain.AppUserEntity.Enums;
using MediumClone.Domain.Common.ValueObjects;
using MediumClone.Domain.ArticleEntity;

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


    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }





}