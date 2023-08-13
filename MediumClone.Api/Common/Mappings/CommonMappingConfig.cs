using Mapster;
using MediumClone.Api.Contracts.Followings;
using MediumClone.Application.Common;
using MediumClone.Domain.FollowingEntity;

namespace MediumClone.Api.Common.Mappings;
public class CommonMappingConfig : IRegister
{
    private const int _defaultPageNumber = 1;
    private const int _defaultPageSize = 10;
    private const string _defaultSortColumn = "id";
    private const string _defaultSortOrder = "desc";

    public void Register(TypeAdapterConfig config)
    {

        config.NewConfig<QueryParamters, CommonQueryParams>()
            .Map(dest => dest.PageNumber, src => src.PageNumber ?? _defaultPageNumber)
            .Map(dest => dest.PageSize, src => src.PageSize ?? _defaultPageSize)
            .Map(dest => dest.Search, src => src.Search ?? string.Empty)
            .Map(dest => dest.SortColumn, src => src.SortColumn ?? _defaultSortColumn)
            .Map(dest => dest.SortOrder, src => src.SortOrder ?? _defaultSortOrder);


        config.NewConfig<Following, FollowingInfoResponse>()
                .Map(dest => dest.Id, src => src.FollowedUser.Id)
                .Map(dest => dest.FirstName, src => src.FollowedUser.FirstName)
                .Map(dest => dest.LastName, src => src.FollowedUser.LastName)
                .Map(dest => dest.Image, src => src.FollowedUser.Image);

        config.NewConfig<Following, FollowerInfoResponse>()
                .Map(dest => dest.Id, src => src.FollowingUser.Id)
                .Map(dest => dest.FirstName, src => src.FollowingUser.FirstName)
                .Map(dest => dest.LastName, src => src.FollowingUser.LastName)
                .Map(dest => dest.Image, src => src.FollowingUser.Image);












        //config.NewConfig<ProductCategory, ProductCategoryResponse>()
        //    .Map(dest => dest.ProductsIds, src => src.Products.Select(x => x.Id).ToList());

    }
}