namespace MediumClone.Api.Contracts.Articles;

public record ArticleResponse(string Title, string Body, string AuthorId, List<int> TagsId);
