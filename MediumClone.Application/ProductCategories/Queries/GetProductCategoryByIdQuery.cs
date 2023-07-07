using ErrorOr;
using MediatR;
using MediumClone.Application.Abstractions.Repositories;
using MediumClone.Domain.Common.DomainErrors;

using MediumClone.Domain.ProductCategoryEntity;

namespace MediumClone.Application.ProductCategories.Queries;
public record GetProductCategoryByIdQuery(int Id) : IRequest<ErrorOr<ProductCategory>>;


public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, ErrorOr<ProductCategory>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetProductCategoryByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<ProductCategory>> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var productCategory = await _unitOfWork.ProductCategories
        .FindAsync(productCategory => productCategory.Id == request.Id, new string[] { "Products" });

        if (productCategory is null) { return Errors.Common.NotFound; }
        return productCategory;
    }
}
