using MediumClone.Domain.Common.Models;

namespace MediumClone.Domain.Common.Entities;

public abstract class BaseEntity<TId>
{

    public TId Id { get; private set; } = default!;
    public DateTime CreatedDateTime { get; private set; } = DateTime.Now;
    public DateTime? UpdatedDateTime { get; protected set; }
    public bool IsActive { get; private set; } = true;


    public void ToggleStatus()
    {
        IsActive = !IsActive;
    }
}