namespace MediumClone.Api.Contracts.Articles;
public record CreateArticleRequest(string Title, string Body, List<int> TagsId);
