namespace MediumClone.Api.Contracts.Tags;
public record CreateTagRequest(string Name);
public record UpdateTagRequest(int Id, string Name);
