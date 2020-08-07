using Castle.Core.Logging;
using Framework.Core;
using SearchEngine.Interface.Facade.Contracts;
using SearchEngine.TransferObject;
using System.Collections.Generic;
using System.Web.Http;

namespace SearchEngine.WebAPI.Controllers
{
    public class SearchEngineController : ApiController, IGateway
    {
        #region =================== Private Variable ======================
        private ISearchFacade _Facade;

        #endregion

        #region =================== Public Property ======================= 
        public ILogger Logger { get; set; }
        #endregion

        #region =================== Public Constructor ==================== 
        public SearchEngineController(ISearchFacade facade)
        {
            _Facade = facade;
            Logger = NullLogger.Instance;
        }
        #endregion

        #region =================== Private Methods ======================= 
        #endregion

        #region =================== Public Methods ======================== 

        [HttpGet]
        [Route("api/ProductFilterByName")]
        public SearchModelDTO Get(string text,int skip,int count)
        {
            var data= _Facade.Search(text,skip,count);

 

            return data;
        }

        [HttpGet]
        [Route("api/ProductFilterByNameWithSuggest")]
        public SearchModelDTO GetWithSuggest(string text,int skip,int count)
        {
            return _Facade.Search(text,skip,count);
            

        }


        [Route("api/ProductFilterByName")]
        public SearchModelDTO Post(SearchInputModelDTO model)
        {
            var data = _Facade.Search(model.Term,model.skip,model.count);
            return data;

        }

        [Route("api/SearchEngine/Update")]
        [HttpGet]
        public bool UpdateIndx()
        {
            _Facade.Update();
            return true;
        }



        #endregion
    }
}