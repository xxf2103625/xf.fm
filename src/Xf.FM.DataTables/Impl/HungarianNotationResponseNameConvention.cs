

using Xf.FM.DataTables.NameConvention;

namespace Xf.FM.DataTables.Impl.NameConvention
{
    /// <summary>
    /// Represents HungarianNotation response naming convention for Xf.FM.DataTables.Impl.
    /// </summary>
    public class HungarianNotationResponseNameConvention : IResponseNameConvention
    {
        public string Draw { get { return "sEcho"; } }
        public string TotalRecords { get { return "iTotalRecords"; } }
        public string TotalRecordsFiltered { get { return "iTotalDisplayRecords"; } }
        public string Data { get { return "aaData"; } }
        public string Error { get { return string.Empty; } }
    }
}
