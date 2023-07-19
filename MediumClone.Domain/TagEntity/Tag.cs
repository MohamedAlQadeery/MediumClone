using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.TagEntity;

public sealed class Tag : BaseEntity<int>
{

    public string Name { get; private set; } = null!;




    private Tag()
    {
    }

    private Tag(string name)
    {
        Name = name;

    }

    public static Tag Create(string name)
    {
        return new Tag(name);
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedDateTime = DateTime.Now;
    }









}