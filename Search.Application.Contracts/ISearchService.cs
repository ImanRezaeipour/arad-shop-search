using System.Collections.Generic;
using System.Linq;
using Common.Domain.Model.Common;
using Framework.Core;

namespace SearchEngine.Application.Contracts
{
    public interface ISearchService : IService
    {
        ShopperSearchModel Search(string text, int skip, int count);
        void Update();
    }
}