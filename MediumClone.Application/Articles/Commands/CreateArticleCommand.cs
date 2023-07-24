using ErrorOr;
using FluentValidation;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;
using MediumClone.Domain.Common.DomainErrors;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Articles.Commands;

public record CreateArticleCommand(string Title, string Body, string AuthorId, List<int> TagsId) : IRequest<ErrorOr<Article>>;

public class CreateArticleCommandValidator : AbstractValidator<CreateArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;



    public CreateArticleCommandValidator(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;


        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Body).NotEmpty().MaximumLength(500);
        RuleFor(x => x.AuthorId).NotEmpty().MustAsync(AuthorBeExist).WithMessage("The author does not exist");
        RuleFor(x => x.TagsId).NotEmpty().MustAsync(BeExist).WithMessage("All tags must be exist");
    }



    //validate authorId exisdt in database
    public async Task<bool> AuthorBeExist(string authorId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(authorId);
        return user is not null;

    }
    //validate all tags are exist in database
    public async Task<bool> BeExist(List<int> tagsIds, CancellationToken cancellationToken)
    {
        foreach (var tagId in tagsIds)
        {
            var tag = await _unitOfWork.Tags.GetByIdAsync(tagId);
            if (tag is null) { return false; }

        }

        return true;

    }


}


public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, ErrorOr<Article>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateArticleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Article>> Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {


        var article = Article.Create(request.Title, request.Body, request.AuthorId);
        await _unitOfWork.Articles.AddAsync(article);
        await _unitOfWork.SaveChangesAsync();

        //add tags
        var articleTags = request.TagsId.Select(tagId => ArticleTag.Create(article.Id, tagId)).ToList();
        article.AddTags(articleTags);
        _unitOfWork.Articles.Update(article);
        if (await _unitOfWork.SaveChangesAsync() <= 0)
        {
            return Errors.Articles.TagsCantBeAssinged;
        }

        return article;


    }
}
