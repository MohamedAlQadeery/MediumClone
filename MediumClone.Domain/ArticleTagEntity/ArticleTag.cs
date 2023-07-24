using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.TagEntity;

namespace MediumClone.Domain.ArticleTagEntity;

public class ArticleTag
{
    public int ArticleId { get; private set; }
    public Article Article { get; private set; } = null!;
    public int TagId { get; private set; }
    public Tag Tag { get; private set; } = null!;

    private ArticleTag()
    {

    }

    private ArticleTag(int articleId, int tagId)
    {
        ArticleId = articleId;
        TagId = tagId;
    }

    public static ArticleTag Create(int articleId, int tagId)
    {
        return new ArticleTag(articleId, tagId);
    }


}