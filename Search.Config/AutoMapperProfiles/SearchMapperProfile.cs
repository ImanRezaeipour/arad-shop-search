using AutoMapper;
using Common.Domain.Model.Common;
using SearchEngine.TransferObject;

namespace SearchEngine.Config.AutoMapperProfiles
{
    public class SearchMapperProfile : Profile
    {
        #region =================== Public Constructor ====================

        public SearchMapperProfile()
        {
            CreateMap<SearchResult, SearchResult>();
            CreateMap<SearchResult, SearchDTO>();
            CreateMap<ShopperProductDiscount, ShopperProductDiscountDTO>();
            CreateMap<ShopperSearchModel, SearchModelDTO>();
            CreateMap<ShopperBrand, ShopperBrandDTO>();
            CreateMap<ShopperProduct, ShopperProductDTO>()
                .ForMember(des => des.discount, op => op.MapFrom(src => new ShopperProductDiscountDTO { from = src.Discount_From, to = src.Discount_To }));


        }

        #endregion =================== Public Constructor ====================
    }
}