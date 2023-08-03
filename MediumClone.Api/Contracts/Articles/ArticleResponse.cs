namespace MediumClone.Api.Contracts.Articles;

public record AuthorResponse(string Id, string FullName, string Image);
public record ArticleResponse(string Title, string Body, AuthorResponse Author, List<string> TagNames);

public record GetAllArticlesResponse(IReadOnlyList<ArticleResponse> Articles, int Count);
