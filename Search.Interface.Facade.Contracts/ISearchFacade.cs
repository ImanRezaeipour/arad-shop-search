using System.Collections.Generic;
using Framework.Core;
using SearchEngine.TransferObject;

namespace SearchEngine.Interface.Facade.Contracts
{
    public interface ISearchFacade : IFacadeService
    {
        #region =================== Private Variable ======================

        #endregion

        #region =================== Public Property ======================= 		
        #endregion

        #region =================== Public Constructor ==================== 
        #endregion

        #region =================== Private Methods ======================= 
        SearchModelDTO Search(string text, int skip, int count);
        void Update();


        #endregion

        #region =================== Public Methods ======================== 
        #endregion

    }
}