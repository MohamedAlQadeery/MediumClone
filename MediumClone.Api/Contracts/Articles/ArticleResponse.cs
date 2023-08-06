using MediumClone.Application.Common;

namespace MediumClone.Api.Contracts.Articles;

public record AuthorResponse(string Id, string FullName, string Image);
public record ArticleResponse(string Title, string Body, string Slug,
 AuthorResponse Author, List<string> TagNames, DateTime CreatedDateTime);

// public record GetAllArticlesResponse(PaginatedList<ArticleResponse> Articles);
