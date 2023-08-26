using ErrorOr;
using FluentValidation;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.AppUserEntity;
using MediumClone.Domain.ArticleEntity;
using MediumClone.Domain.ArticleTagEntity;
using Microsoft.AspNetCore.Identity;

namespace MediumClone.Application.Articles.Commands;

public record UpdateArticleCommand(int Id, string Title, string Body, List<int> TagsId) : IRequest<ErrorOr<Article>>;

public class UpdateArticleCommandValidator : AbstractValidator<UpdateArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<AppUser> _userManager;



    public UpdateArticleCommandValidator(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;


        RuleFor(x => x).MustAsync(BeUniqueName);
        RuleFor(x => x.Id).MustAsync(ArticleBeExist).WithMessage("The article does not exist");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Body).NotEmpty().MaximumLength(500);
        RuleFor(x => x.TagsId).NotEmpty().MustAsync(BeExist).WithMessage("All tags must be exist");
    }



    //validate authorId exisdt in database
    public async Task<bool> AuthorBeExist(string authorId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(authorId);
        return user is not null;

    }


    public async Task<bool> ArticleBeExist(int id, CancellationToken cancellationToken)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(id);
        return article is not null;

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


    public async Task<bool> BeUniqueName(UpdateArticleCommand request, CancellationToken cancellationToken)
    {
        var tag = await _unitOfWork.Articles.FindAsync(a => a.Title == request.Title && a.Id != request.Id);
        return tag == null;

    }

}


public class UpdateArticleCommandHandler : IRequestHandler<UpdateArticleCommand, ErrorOr<Article>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateArticleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Article>> Handle(UpdateArticleCommand request, CancellationToken cancellationToken)
    {



        var article = await _unitOfWork.Articles.GetByIdAsync(request.Id);
        var tagsToAdd = await _unitOfWork.Tags.FindAllAsync(t => request.TagsId.Contains(t.Id));

        var articleTags = tagsToAdd.Select(t => ArticleTag.Create(article.Id, t.Id)).ToList();

        article.Update(request.Title, request.Body, articleTags);

        await _unitOfWork.SaveChangesAsync();
        return article;


    }
}
