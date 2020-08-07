using System.Collections.Generic;
using System.Linq;
using Common.Domain.Model.Common;
using Common.Infrastructure.Persistance.EF;
using Framework.Persistance.EF;
using LuceneSearch.Core.Model;

namespace LuceneSearch.Core
{
    public class CreateDbIndex
    {        private readonly CreateIndex _createIndex;

        public CreateDbIndex( CreateIndex createIndex)
        {
            _createIndex = createIndex;
        }
        public void Start(string path,bool isUpdate=false)
        {
            var _dbContext = new CommonDbContext();
            var list = _dbContext.Database.SqlQuery<ViewProduct>("SELECT Id,[Name] FROM V_Brand_Product").Select(a => new SearchResult
            {
                Id = a.Id,
                Title = a.Name
            }).ToList();
            if(!isUpdate)
                _createIndex.CreateFullTextIndex(list, path);
            else
                _createIndex.UppdateFullTextIndex(list, path);
        }
    }
}
