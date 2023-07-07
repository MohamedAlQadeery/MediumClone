using Ardalis.SmartEnum;

namespace MediumClone.Domain.AppUserEntity.Enums
{

    public sealed class AppUserRole : SmartEnum<AppUserRole>
    {
        public AppUserRole(string name, int value) : base(name, value)
        {
        }
        public static readonly AppUserRole Admin = new(nameof(Admin), 1);
        public static readonly AppUserRole User = new(nameof(User), 2);

    }
}