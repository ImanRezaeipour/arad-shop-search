using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Common.Domain.Model.Common;
using Framework.Persistance.EF;
using LuceneSearch.Core;
using SearchEngine.Application.Contracts;

namespace SearchEngine.Application
{
    public class SearchService : ISearchService
    {
        private readonly IUnitOfWork _uow;
        private readonly AutoComplete _autoComplete;
        private readonly CreateDbIndex _createDbIndex;

        #region =================== Public Constructor ====================
        public SearchService(IUnitOfWork uow, AutoComplete autoComplete, CreateDbIndex createDbIndex)
        {
            _uow = uow;
            _autoComplete = autoComplete;
            _createDbIndex = createDbIndex;
        }
        static string _indexPath = HttpRuntime.AppDomainAppPath + @"App_Data\idx";

        public ShopperSearchModel Search(string text, int skip, int count)
        {
            var result = _autoComplete.GetTermsScored(_indexPath, text);

            var data = new ShopperSearchModel();
            if (result != null)
                data.Suggests = result.Suggests;
            if (result != null && result.Results != null && result.Results.Count > 0)
            {
                data.count = result.Results.Count;

                var ids = result.Results.Select(a => a.Id).ToList();
                //var idsStr = result.Results.Select(a => a.Title.ToString()).ToList();
                var list = _uow.Set<ShopperProduct>()
                    .Where(a => ids.Contains(a.Id))
                    .Include(a => a.Brand)
                    .Include(a => a.Category)
                    .Include(a => a.Discount)
                    .ToList()
                    .OrderBy(l => ids.IndexOf(l.Id))
                    .Skip(skip)
                    .Take(count)
                    .ToList();
                //var q = (from npostID in idsStr.AsQueryable()
                //            join np in _uow.Set<ShopperProduct>()
                //            on npostID equals np.Id.ToString()
                //            select np).AsQueryable();
                //var list=q
                //    .Include(a => a.Brand)
                //    .Include(a => a.Category)
                //    .Include(a => a.Discount)
                //    .Skip(skip)
                //    .Take(count)
                //    .ToList();
                data.Result = list;
            }



            return data;
        }

        public void Update()
        {
            _createDbIndex.Start(_indexPath, true);
            _autoComplete.RenewSearcherObject(_indexPath);

        }


        #endregion =================== Public Constructor ====================


    }
}