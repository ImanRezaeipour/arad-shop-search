using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Domain.Model;
using Common.Domain.Model.Common;
using Framework.Persistance.EF;
using SearchEngine.Application.Contracts;
using SearchEngine.Interface.Facade.Contracts;
using SearchEngine.TransferObject;

namespace SearchEngine.Interface.Facade
{
    public class SearchFacade : ISearchFacade
    {
        #region =================== Private Variable ======================
        private ISearchService _searchService;
        private IUnitOfWork _UOW;
        #endregion

        #region =================== Public Property ======================= 		
        #endregion

        #region =================== Public Constructor ==================== 
        public SearchFacade(ISearchService SearchService, IUnitOfWork uow)
        {
            _searchService = SearchService;
            _UOW = uow;
        }
        #endregion

        #region =================== Private Methods ======================= 
        #endregion

        #region =================== Public Methods ======================== 
        public SearchModelDTO Search(string text, int skip, int count)
        {
            var result = _searchService.Search(text, skip, count);

            //var data = new SearchModelDTO();
            //data.result = result.Result.Select(a => new ShopperProductDTO
            //{
            //    brand = a.Brand == null ? new ShopperBrandDTO() : new ShopperBrandDTO {
            //        id = a.Brand.Id,
            //        isActive = a.Brand.IsActive,
            //        latinName = a.Brand.LatinName,
            //        name = a.Brand.Name
            //    },
            //    name = a.Name,
            //    id = a.Id,
            //    brand_Id = a.Brand_Id,
            //    categoryId = a.Category_Id,
            //    categoryName = a.Category?.Name,
            //    description = a.Description,
            //    discount = a.Discount == null ? new ShopperProductDiscountDTO() : new ShopperProductDiscountDTO {
            //        from = a.Discount_From,
            //        to = a.Discount_To
            //    },
            //    isactive = a.IsActive,
            //    price = a.Price,
            //    productDiscount_Id = a.ProductDiscount_Id,
            //    shortDescription = a.ShortDescription,
            //    mainImage = a.MainImage
            //}).ToList();
            //data.suggests = result.Suggests;
            //if (result != null && result.Result != null && result.Result.Count > 0)
            //    result.Result.ForEach(a =>
            //    {
            //        if (a.Price > 0)
            //            a.Price /= 10;
            //    }
            //    );
            return Mapper.Map<SearchModelDTO>(result);
        }

        public void Update()
        {
            _searchService.Update();
        }



        #endregion
    }
}