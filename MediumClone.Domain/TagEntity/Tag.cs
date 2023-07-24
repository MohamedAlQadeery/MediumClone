using MediumClone.Domain.ArticleTagEntity;
using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.TagEntity;

public sealed class Tag : BaseEntity<int>
{
    private readonly List<ArticleTag> _articleTags = new();

    public string Name { get; private set; } = null!;
    public IReadOnlyList<ArticleTag> ArticleTags => _articleTags.AsReadOnly();





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