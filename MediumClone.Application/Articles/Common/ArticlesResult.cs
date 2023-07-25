using MediumClone.Domain.ArticleEntity;

namespace MediumClone.Application.Articles.Common;

public record ArticlesResult(IReadOnlyList<Article> Articles, int Count);