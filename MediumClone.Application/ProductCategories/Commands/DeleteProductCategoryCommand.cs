using ErrorOr;
using MediatR;
using MediumClone.Domain.Common.DomainErrors;
using MediumClone.Application.Abstractions.Repositories;

namespace MediumClone.Application.ProductCategories.Commands.DeleteProductCategory;
public record DeleteProductCategoryCommand(int Id) : IRequest<ErrorOr<Unit>>;


public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, ErrorOr<Unit>>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeleteProductCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<Unit>> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
    {
        var productTodelete = await _unitOfWork.ProductCategories.GetByIdAsync(request.Id);
        if (productTodelete is null) { return Errors.Common.NotFound; }

        _unitOfWork.ProductCategories.Delete(productTodelete);

        await _unitOfWork.SaveChangesAsync();
        return Unit.Value;


    }
}
