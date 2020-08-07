using System.Collections.Generic;

namespace SearchEngine.TransferObject
{
    public class SearchInputModelDTO
    {
        #region =================== Public Property =======================
        public string Term { get; set; }
        public int skip { get; set; }
        public int count { get; set; }
        #endregion =================== Public Property =======================
    }
}
