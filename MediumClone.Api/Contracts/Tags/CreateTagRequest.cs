namespace MediumClone.Api.Contracts.Tags;
public record CreateTagRequest(string Name);
public record UpdateTagRequest(string Name);

public record TagResponse(int Id, string Name);
public record PopularTagResponse(int Id, string Name, int ArticlesCount);
