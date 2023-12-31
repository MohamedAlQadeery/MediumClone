using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.ArticleTagEntity;
using MediumClone.Domain.Common.Entities;

namespace MediumClone.Domain.ArticleEntity;

public class Article : BaseEntity<int>
{

    private readonly List<ArticleTag> _articleTags = new();
    public string Title { get; private set; } = null!;
    public string Slug { get; private set; }
    public string Body { get; private set; } = null!;

    //many to one relation to AppUser
    public string AuthorId { get; private set; }
    public AppUser Author { get; private set; } = null!;

    //many to many relation to Tag 
    public IReadOnlyList<ArticleTag> ArticleTags => _articleTags.AsReadOnly();

    private Article()
    {

    }

    private Article(string title, string body, string authorId)
    {
        Title = title;
        Slug = title.Replace(" ", "-").ToLower();
        Body = body;
        AuthorId = authorId;
    }

    public static Article Create(string title, string body, string authorId)
    {
        return new Article(title, body, authorId);
    }

    public void Update(string title, string body)
    {
        Title = title;
        Slug = title.Replace(" ", "-").ToLower();
        Body = body;

    }
    public void Update(string title, string body, List<ArticleTag> articleTags)
    {
        Title = title;
        Slug = title.Replace(" ", "-").ToLower();
        Body = body;
        _articleTags.Clear();
        _articleTags.AddRange(articleTags);

    }




    //add tags
    public void AddTags(List<ArticleTag> articleTags)
    {
        _articleTags.Clear();
        _articleTags.AddRange(articleTags);
    }

    //remove tags
    public void RemoveTags(List<ArticleTag> articleTags)
    {
        _articleTags.RemoveAll(x => articleTags.Contains(x));
    }





}