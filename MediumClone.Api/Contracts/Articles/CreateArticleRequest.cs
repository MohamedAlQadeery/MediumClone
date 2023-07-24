namespace MediumClone.Api.Contracts.Articles;
public record CreateArticleRequest(string Title, string Body, string AuthorId, List<int> TagsId);
